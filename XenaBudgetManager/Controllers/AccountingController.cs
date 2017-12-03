using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class AccountingController : Controller
    {
        [HttpGet]
        public ActionResult Accounting(AccountingModel data)
        {
            return View("Accounting",data);
        }

    }
}