using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XenaBudgetManager.Classes;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class AccountingController : Controller
    {
        ///<summary>
        /// Written by Claus, Jonas and Mikael
        ///</summary>
        public ActionResult Accounting()
        {
            ViewBag.datePicked = false;
            ViewBag.datePickedCorrect = true;

            SelectList budgetList = new SelectList(DB.GetBudgetId(), "budgetID", "budgetName");
            ViewBag.budgetList = budgetList;

            return View();
        }

        ///<summary>
        /// /// Written by Claus and Jonas
        ///</summary>
        [HttpPost]
        public ActionResult Accounting(DateTime fromDate, DateTime toDate, int budgetID)
        {
            ViewBag.datePicked = true;
            ViewBag.datePickedCorrect = DatePickedCorrect(fromDate, toDate);
            
            List<LedgerGroupData> ledgerGroupDataList =
                GetXenaData.LedgerGroupData(Session["access_token"].ToString(), int.Parse(Session["fiscalID"].ToString()), fromDate, toDate);

            SelectList budgetList = new SelectList(DB.GetBudgetId(), "budgetID", "budgetName");
            ViewBag.budgetList = budgetList;

            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            foreach (LedgerGroupData ledgerGroupData in ledgerGroupDataList)
            {
                ledgerGroupData.AccountList = DB.GetAccounts(ledgerGroupData, budgetID, fromMonth, toMonth);
            }

            //cn attempt to build list og budget numbers
            //List<LedgerGroupData> AccountList = new List<LedgerGroupData>();
            //List<ComparedData> ComparedDataList = new List<ComparedData>();

            //for (int i = 0; i < ledgerGroupDataList.Count; i++)
            //{
            //    ComparedDataList = ledgerGroupDataList[i] - AccountList[i];
            //}

            return View(ledgerGroupDataList);
        }

        ///<summary>
        /// /// Written by Jonas
        ///</summary>
        private bool DatePickedCorrect(DateTime fromDate, DateTime toDate)
        {
            int fromYear = fromDate.Year;
            int toYear = toDate.Year;

            long fromXnixTimeSeconds = ((DateTimeOffset)fromDate).ToUnixTimeSeconds();
            long toUnixTimeSeconds = ((DateTimeOffset)toDate).ToUnixTimeSeconds();

            if (fromYear == toYear && fromXnixTimeSeconds <= toUnixTimeSeconds)
            {
                return true;
            }

            return false;
        }
    }
}