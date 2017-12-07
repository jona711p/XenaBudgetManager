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
            DateTime? fromDate = null;
            DateTime? toDate = null;

            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;

            return View();
        }

        [HttpPost]
        public ActionResult Accounting(DateTime fromDate, DateTime toDate)
        {
            List<LedgerGroupData> ledgerGroupDataList =
                GetXenaData.LedgerGroupData(Session["access_token"].ToString(), fromDate, toDate);
            List<LedgerGroupDetailData> ledgerGroupDetailDataList =
                GetXenaData.LedgerGroupDetailData(Session["access_token"].ToString(), fromDate, toDate);

            return View();
        }

        public ActionResult LedgerTag()
        {
            GetXenaData.LedgerTag(Session["access_token"].ToString());

            return RedirectToAction("Accounting");
        }

        public ActionResult LedgerGroupData()
        {
            return View(GetXenaData.LedgerGroupData(Session["access_token"].ToString()));
        }

        public ActionResult LedgerGroupDetailData()
        {
            return View(GetXenaData.LedgerGroupDetailData(Session["access_token"].ToString()));
        }
    }
}