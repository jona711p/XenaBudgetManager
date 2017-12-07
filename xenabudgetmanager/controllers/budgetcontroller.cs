using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Models
{
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult Budget()
        {
            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["XenaBudgetManager"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM view_FullBudget WHERE Budgetnavn = 'Kaffebudget'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }

            return View(ds);
        }
        

        public ActionResult CreateBudget()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBudget(Budget budget)
        {
            List<LedgerTags> tempLedgerTag = GetXenaData.LedgerTag(Session["access_token"].ToString()); //trækker kontoer ud fra xena og gemmer dem i en liste af typen LedgerTags
            List<LedgerAccounts> tempLedgerAccount = GetXenaData.LedgerAccount(Session["access_token"].ToString()); //trækker grupper ud fra xena og gemmer dem i en liste af typen LedgerAccounts

            budget.budgetID = DB.WriteNewBudget(budget); //opretter nyt budget og returnere Id fra DB

            List<LedgerTags> dupecheckledgertag = tempLedgerTag.Where(x => !GetXenaData.DupeCheckListTag().Contains(x.ledgerTagId)).ToList();
            List<LedgerAccounts> dupecheckledgerAccount = tempLedgerAccount.Where(x => !GetXenaData.DupeCheckListAccount().Contains(x.ledgerAccountId.ToString())).ToList();//CN Id added tostring

            //if ((tempLedgerTag.Count > 0 || tempLedgerAccount.Count > 0 && dupecheckledgertag.Count == 0) ||
            //    (tempLedgerTag.Count > 0 || tempLedgerAccount.Count > 0 && dupecheckledgerAccount.Count == 0))
            //{
            //    //tjek efter budgetår?
            //    tempLedgerAccount = DB.GetAccountIDs(tempLedgerAccount); //hent ids fra allerede oprettede db grupper
            //    DB.WriteNewRel_AccountPlan(dupecheckledgertag, tempLedgerAccount, budget.budgetID); //sætter budget grupper og kontoer i relation til hinanden

            //}
            tempLedgerAccount = DB.WriteNewLedgerAccount(dupecheckledgerAccount); //skriver unikke  grupper i DB og gemmer grupperID fra db i en liste af grupper
            DB.WriteNewLedgerTag(dupecheckledgertag); //skriver unikke  kontoer i DB
            DB.WriteNewRel_AccountPlan(dupecheckledgertag, tempLedgerAccount, budget.budgetID); //sætter budget grupper og kontoer i relation til hinanden
            return View("EditBudget", tempLedgerTag);
            
            /*
             kører fint første gang
             anden gang er 
                dupecheckledgertag.count = 1 
                dupecheckledgerAccount.count = 10
            men disse grupper og kontoer hører ikke sammen? så der bliver ikke lavet en relationstabel for budget nr 2
            henter vi ikke den fulde liste med grupper & konto fra xena med vores kald?
             */
        }
        public ActionResult EditBudget(List<LedgerTags> list)
        {
            return View();
        }

    }
}