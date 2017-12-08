using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using XenaBudgetManager.Classes;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// When the "Login" Link is pushed on the page.
        /// Makes a QueryString with the parameters that is needed and a "Callback URL", then it sends it to Xena's Authorization Server.
        /// Then you get redirect to Xena's own Login Page, here you enter your data and press Login.
        /// </summary>
        public void Login()
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["response_type"] = "code id_token";
            queryString["client_id"] = "2e64617f-dc5d-4983-ba27-7dcdb2ed5510.apps.xena.biz";
            queryString["redirect_uri"] = "http://xenabudgetmanager.azurewebsites.net/";
            queryString["scope"] = "openid testapi";
            queryString["nonce"] = XenaLogic.RandomString(32);
            queryString["response_mode"] = "form_post";
            queryString["json"] = "true";

            Response.Redirect("https://login.xena.biz/connect/authorize?" + queryString);
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// After you have pressed Login on Xena's own Login Page, it sends you back to the "Callback URL" that was send in the first place.
        /// You have been givin' a "code". This is your approval from the Xena's Authorization Server.
        /// </summary>
        [HttpPost]
        public ActionResult Index(Xena xena)
        {
            xena.id_code = Request["code"];
            xena.access_token = XenaLogic.AccessToken(xena);

            Session["access_token"] = xena.access_token;
            Session["loggedIn"] = true;

            List<JToken> jTokenList = XenaLogic.CallXena(Session["access_token"].ToString(),
                "User/XenaUserMembership?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false");

            Session["ficalIDList"] = XenaLogic.GetFiscalIDList(jTokenList);
            Session["userName"] = jTokenList[0]["ResourceName"].ToString();

            ViewBag.Token = xena.access_token; // Debug

            return View(xena);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Debug
        /// </summary>
        public ActionResult Debug()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Debug(string token)
        {
            Session["loggedIn"] = true;
            Session["access_token"] = token;

            List<JToken> jTokenList = XenaLogic.CallXena(Session["access_token"].ToString(),
                "User/XenaUserMembership?listOptions.showDeactivated=true&listOptions.forceNoPaging=true");

            Session["ficalIDList"] = XenaLogic.GetFiscalIDList(jTokenList);
            Session["userName"] = jTokenList[0]["ResourceName"].ToString();

            return RedirectToAction("Index");
        }
    }
}