using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Classes
{
    public class XenaLogic
    {
        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// You will now request a Access Token from Xena's Authorization Server.
        /// You build a encoded URL and send it to Xena's Authorization Server.
        /// Xena's Authorization Server sends a long string back, this is your Access Token!
        /// You assign it to your varible, and it is now ready to be shipped with any Xena's API calls ;)
        /// </summary>
        public static string AccessToken(Xena xena)
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
                return xena.access_token;
            }
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// Here we generate a uniqe string og letters & numbers to be used in the authorization process.
        /// </summary>
        public static string RandomString(int length)
        {
            Random rnd = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static SelectList GetFiscalSelectList(List<JToken> jTokenList)
        {
            List<Fiscal> fiscalList = new List<Fiscal>();

            foreach (JToken jToken in jTokenList)
            {
                fiscalList.Add(new Fiscal(jToken));
            }

            SelectList fiscalSelectList = new SelectList(fiscalList, "ResourceName", "FiscalSetupId");

            return fiscalSelectList;
        }

        /// <summary>
        /// 
        /// Written by Jonas
        /// 
        /// This is a "HELPER METHOD" that is used everytime you want to call Xena's API. So that it minimize the code use in the rest of the code ;)
        /// </summary>
        public static List<JToken> CallXena(string token, string path)
        {
            List<JToken> jTokenList = new List<JToken>();

            AuthenticationHeaderValue authValue = new AuthenticationHeaderValue("Bearer", token);

            using (HttpClient httpClient = new HttpClient { DefaultRequestHeaders = { Authorization = authValue }, BaseAddress = new Uri("https://my.xena.biz/Api/") })
            {
                JObject jObject = JObject.Parse(httpClient.GetStringAsync(path).Result);
                jTokenList = jObject["Entities"].Children().ToList();
            }

            return jTokenList;
        }


        // Written by Jonas
        //
        // This is what should be used in the rest of the code, to utilize the Helper Method seen above, COPY AND UNCOMMENT.
        // !!! Don't assume that its like it should be everywhere, you will need to change the URL string "Fiscal/98437/FiscalPeriod" so that you call the right API Endpoint !!!
        // !!! We'll recive a List with JSON Tokens.                                                                                                                          !!!
        // !!! The list of Tokens and now be procressed with INDEX [ ] or a FOREACH, I'll include examples on each for you                                                    !!!

        //private void GetDataFromXena()
        //{
        //    List<JToken> jTokenList = Xena.CallXena(Session["access_token"].ToString(), // "Session["access_token"].ToString()" Can only be used in a controller, change it to a parameter if used in a class and send it along from the controller
        //        "Fiscal/98437/FiscalPeriod");

        //    // INDEX [ ]
        //    string indexString = jTokenList[0]["ResourceName"].ToString();

        //    // FOREACH
        //    foreach (JToken jToken in jTokenList)
        //    {
        //        string jTokenString1 = jToken["ResourceName1"].ToString();
        //        string jTokenString2 = jToken["ResourceName3"].ToString();
        //        string jTokenString3 = jToken["ResourceName3"].ToString();
        //    }
        //}
    }
}