using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using XenaBudgetManager.Classes;

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
            //evt lav et tjek om budgetyear allerede er i db?

            //bind det valgte fiscalID til budget.fiscalId 
            budget.XenaFiscalID = int.Parse(Session["fiscalID"].ToString());

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

            budget.budgetID = DB.WriteNewBudget(budget); //opretter nyt budget og returnere Id fra DB

            tempLedgerAccount = DB.GetAccountIDs(tempLedgerAccount);
            tempLedgerTag = DB.GetTagIDs(tempLedgerTag);
            DB.WriteNewRel_AccountPlan(tempLedgerTag, tempLedgerAccount, budget.budgetID);

            #region Konverter til budget

            

            //gemmer rel_accountplan for det givene budgetID i et keyvaluepair
            List<KeyValuePair<int, int>> AccountPlan = DB.ReadRel_AccountPlan(budget);

            //konvertere tags til accounts
            List<Account> tempAccountList = Account.ConvertLedgerTagsToAccountList(tempLedgerTag);

            //konvertere ledgerAccounts til accountgroups
            List<AccountGroup> tempGroupList = AccountGroup.ConvertLedgerAccountToAccountGroupList(tempLedgerAccount, tempAccountList, AccountPlan);


            budget.groupList = new AccountGroupListViewModel() {groupList = tempGroupList};

            //tjek hvilke group.id der hører sammen med account.id via accountplan
            for (int i = 0; i < tempGroupList.Count; i++)
            {
                tempGroupList[i].accountList = new AccountListViewModel()
                {
                    accountList = new List<Account>()
                };
                for (int j = 0; j < tempAccountList.Count; j++)
                {
                    for (int k = 0; k < AccountPlan.Count; k++)
                    {
                        if (tempGroupList[i].accountGroupID == AccountPlan[k].Key && tempAccountList[j].accountID == AccountPlan[k].Value)
                        {
                            tempGroupList[i].accountList.accountList.Add(tempAccountList[j]);
                        }
                    }
                }
            }
            for (int i = 0; i < budget.groupList.groupList.Count; i++)
            {
                if (budget.groupList.groupList[i].accountList.accountList.Count == 0)
                {
                    budget.groupList.groupList.RemoveAt(i);
                    --i;
                }
            }
            #endregion

            ViewBag.list = budget;
            return View("EditBudget",ViewBag.list);
        }

        [HttpPost]
        public ActionResult EditBudget(Budget compeleteBudget)
        {
            // !lav Dropdownliste med alle måneder i view 
            //!efter måned er valgt kan værdier for denne indtastes for hver konto
            //!derefter vælger man et nyt måned og gør det samme
            //!hvis man f.eks. har indtastes op til marts skal man stadig kunne gå tilbage og ændre f.eks. januar
            //-!når alle måneder/konti er udfyldt trykkes på en knap "gem budget"
            //-!budgettet kan ikke redigeres efter dette
            //bagefter skal vi lave et view til visningen af budgettet a la 'vis regnskab'


            return RedirectToAction("Index","Home");
        }
    }

}

