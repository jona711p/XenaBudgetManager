using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XenaBudgetManager.Models
{
    public class Xena
    {
        public string access_token { get; set; }
        public string id_code { get; set; }

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
        //    List<JToken> jTokenList = Xena.CallXena(Session["access_token"].ToString(),
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