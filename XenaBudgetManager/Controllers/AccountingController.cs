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
        /// /// Written by Jonas and Mikael
        ///</summary>
        public ActionResult Accounting()
        {
            ViewBag.datePicked = false;
            ViewBag.datePickedCorrect = true;

            return View();
        }

        ///<summary>
        /// /// Written by Jonas
        ///</summary>
        [HttpPost]
        public ActionResult Accounting(DateTime fromDate, DateTime toDate)
        {
            ViewBag.datePicked = true;
            ViewBag.datePickedCorrect = DatePickedCorrect(fromDate, toDate);
            
            List<LedgerGroupData> ledgerGroupDataList =
                GetXenaData.LedgerGroupData(Session["access_token"].ToString(), int.Parse(Session["fiscalID"].ToString()), fromDate, toDate);

            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            //DB.GetFullBudgetList(123, fromMonth, toMonth);

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