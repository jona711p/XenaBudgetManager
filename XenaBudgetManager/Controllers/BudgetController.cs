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
            int budgetID;
            List<LedgerTags> tempLedgerTag = GetXenaData.LedgerTag(Session["access_token"].ToString());
            List<LedgerAccounts> tempLedgerAccount = GetXenaData.LedgerAccount(Session["access_token"].ToString());

            budgetID = DB.WriteNewBudget(budget);

            List<LedgerTags> dupecheckledgertag = tempLedgerTag.Where(x => !GetXenaData.DupeCheckListTag().Contains(x.ledgerTagId)).ToList();
            List<LedgerAccounts> dupecheckledgerAccount = tempLedgerAccount.Where(x => !GetXenaData.DupeCheckListAccount().Contains(x.ledgerAccountId)).ToList();

            DB.WriteNewLedgerAccount(dupecheckledgerAccount);
            DB.WriteNewLedgerTag(dupecheckledgertag);
            DB.WriteNewRel_AccountPlan(dupecheckledgertag, dupecheckledgerAccount, budgetID);
            return View();
            
        }
        
    }
}