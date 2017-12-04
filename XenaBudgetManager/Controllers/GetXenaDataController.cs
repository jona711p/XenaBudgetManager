using System.Web.Mvc;
using System.Collections.Generic;
using XenaBudgetManager.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace XenaBudgetManager.Controllers
{
    public class GetXenaDataController : Controller
    {
        //create a list of xenadatamodel objects, to place the incoming data in to 
        public static List<LedgerAccounts> GetAccountPlan(Xena xena) //Takes the helper object xena to easier connect to xena
        {
            List<LedgerAccounts> LedgerAccountData = new List<LedgerAccounts>();

            using (HttpClient httpClient = xena.CallXena()) //instantite httpclient using the cll xena helper
            {
              
                JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/LedgerTagGroup/LedgerAccount").Result);

                foreach (JObject jObject in jArray)
                {
                    LedgerAccountData.Add(new LedgerAccounts(jObject));
                }
            }
            return LedgerAccountData;         
        }

        //// GET: GetXenaData
        //public ActionResult Index(string token)
        //{
        //    return View();
        //}

        //// GET: GetXenaData/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: GetXenaData/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: GetXenaData/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: GetXenaData/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: GetXenaData/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add e logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: GetXenaData/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: GetXenaData/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
