using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
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
            queryString["nonce"] = RandomString(32);
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
            AccessToken(xena);

            Session["loggedIn"] = true;

            List<JToken> jTokenList = Xena.CallXena(Session["access_token"].ToString(),
                "User/XenaUserMembership?ForceNoPaging=true&Page=0&PageSize=10&ShowDeactivated=false");

            Session["userName"] = jTokenList[0]["ResourceName"].ToString();

            ViewBag.Token = xena.access_token; // Debug

            return View(xena);
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// You will now request a Access Token from Xena's Authorization Server.
        /// You build a encoded URL and send it to Xena's Authorization Server.
        /// Xena's Authorization Server sends a long string back, this is your Access Token!
        /// You assign it to your varible, and it is now ready to be shipped with any Xena's API calls ;)
        /// </summary>
        private void AccessToken(Xena xena)
        {
            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("code", xena.id_code),
                new KeyValuePair<string, string>("client_id", "2e64617f-dc5d-4983-ba27-7dcdb2ed5510.apps.xena.biz"),
                new KeyValuePair<string, string>("redirect_uri", "http://xenabudgetmanager.azurewebsites.net/"),
                new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["client_secret"]),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("response_mode", "form_post"),
                new KeyValuePair<string, string>("json", "true")
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(keyValuePairs);

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync("https://login.xena.biz/connect/token?", content).Result;

                string result = response.Content.ReadAsStringAsync().Result;

                JObject jObject = JObject.Parse(result);

                xena.access_token = jObject["access_token"].ToString();
                Session["access_token"] = xena.access_token;
            }
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// Here we generate a uniqe string og letters & numbers to be used in the authorization process.
        /// </summary>
        private string RandomString(int length)
        {
            Random rnd = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
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

            List<JToken> jTokenList = Xena.CallXena(Session["access_token"].ToString(),
                "User/XenaUserMembership?listOptions.showDeactivated=true&listOptions.forceNoPaging=true");

            Session["userName"] = jTokenList[0]["ResourceName"].ToString();

            return RedirectToAction("Index");
        }

        public ActionResult DBTEST()
        {
            Account demo = new Account();
            Random rnd = new Random();

            demo.January = rnd.Next(0, 99999);
            demo.February = rnd.Next(0, 99999);
            demo.March = rnd.Next(0, 99999);
            demo.April = rnd.Next(0, 99999);
            demo.May = rnd.Next(0, 99999);
            demo.June = rnd.Next(0, 99999);
            demo.July = rnd.Next(0, 99999);
            demo.August = rnd.Next(0, 99999);
            demo.September = rnd.Next(0, 99999);
            demo.October = rnd.Next(0, 99999);
            demo.November = rnd.Next(0, 99999);
            demo.December = rnd.Next(0, 99999);
            demo.Total = rnd.Next(0, 99999);

            DB.WriteValueInterval(demo);

            return View("Index");
        }
    }
}