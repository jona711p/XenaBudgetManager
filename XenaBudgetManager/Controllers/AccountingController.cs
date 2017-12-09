using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XenaBudgetManager.Classes;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class AccountingController : Controller
    {
        public ActionResult Accounting()
        {
            ViewBag.datePicked = false;
            return View();
        }

        [HttpPost]
        public ActionResult Accounting(DateTime fromDate, DateTime toDate)
        {
            ViewBag.datePicked = true;

            List<LedgerGroupData> ledgerGroupDataList =
                GetXenaData.LedgerGroupData(Session["access_token"].ToString(), int.Parse(Session["fiscalID"].ToString()), fromDate, toDate);

            return View(ledgerGroupDataList);
        }
    }
}