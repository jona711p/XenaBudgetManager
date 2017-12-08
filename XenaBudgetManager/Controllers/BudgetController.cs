using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using XenaBudgetManager.Classes;

namespace XenaBudgetManager.Models
{
    
    public class BudgetController : Controller
    {
        List<LedgerTags> tempLedgerTag = new List<LedgerTags>();
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
            //evt lav et tjek om budgetyear allerede er i db

            //hent alle grupper i en liste
            List<LedgerAccounts> tempLedgerAccount = GetXenaData.LedgerAccount(Session["access_token"].ToString()); //trækker grupper ud fra xena og gemmer dem i en liste af typen LedgerAccounts

            //hent alle konti fra xena
            List<LedgerTags> tempLedgerTag = GetXenaData.LedgerTag(Session["access_token"].ToString()); //trækker kontoer ud fra xena og gemmer dem i en liste af typen LedgerTags
            List<ExtraLedgerTag> tempExtraProductTag = GetXenaData.GetProductTag(Session["access_token"].ToString());
            List<ExtraLedgerTag> tempExtraRevenueTag = GetXenaData.GetRevenueTag(Session["access_token"].ToString()); //laver listen forfra

            //konverter og tilføj konti for varegrupper og nettoomsætning og tilføj dem til kontolisten
            for (int i = 0; i < tempExtraProductTag.Count; i++)
            {
                tempLedgerTag.Add(new LedgerTags(tempExtraProductTag[i]));
            }
            for (int i = 0; i < tempExtraRevenueTag.Count; i++)
            {
                tempLedgerTag.Add(new LedgerTags(tempExtraRevenueTag[i]));
            }

            //extratagID8 er ikke unikt brug kontoid istedet?
            budget.budgetID = DB.WriteNewBudget(budget); //opretter nyt budget og returnere Id fra DB

            tempLedgerAccount = DB.GetAccountIDs(tempLedgerAccount);
            tempLedgerTag = DB.GetTagIDs(tempLedgerTag);
            DB.WriteNewRel_AccountPlan(tempLedgerTag, tempLedgerAccount, budget.budgetID);


            ViewBag.list = tempLedgerTag;
            return View("EditBudget",ViewBag.list);
            
        }



        [HttpPost]
        public ActionResult EditBudget(List<LedgerTags> list)
        {
            return View();
        }
    }

}

