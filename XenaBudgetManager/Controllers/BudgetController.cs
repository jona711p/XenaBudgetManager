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
                string query = "Select ValueIntervalID, AccountName,ShortDescription,LongDescription FROM Budget Inner JOIN ValueInterval ON BudgetID = BudgetID INNER JOIN LedgerAccount ON LedgerAccountID = LedgerAccountID INNER JOIN LedgerTag ON LedgerTagID = LedgerTagID INNER JOIN XenaFiscal ON XenaFiscalID = XenaFiscalID";
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
    }
}