using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XenaBudgetManager.Controllers
{
    public class GetXenaDataController : Controller
    {
        // GET: GetXenaData
        public ActionResult Index(string token)
        {
            return View();
        }

        // GET: GetXenaData/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GetXenaData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GetXenaData/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GetXenaData/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GetXenaData/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GetXenaData/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GetXenaData/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
