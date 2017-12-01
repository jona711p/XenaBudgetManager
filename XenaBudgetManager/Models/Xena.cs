using System;
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
        public HttpClient CallXena()
        {
            AuthenticationHeaderValue authValue = new AuthenticationHeaderValue("Bearer", access_token);

            HttpClient httpClient = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue },
                BaseAddress = new Uri("https://my.xena.biz/Api/Fiscal/")
            };

            return httpClient;
        }


        /// <summary>
        /// 
        /// Written by Jonas
        ///
        /// This is what should be used in the rest of the code, to utilize the Helper Method seen above.
        /// !!!   Don't assume that its like it should be everywhere, you will need to change the URL string "98437/FiscalPeriod" so that you call the right API Endpoint !!!
        /// !!! Right now, we just recive a JSON string. This needs to be processed into what ever you might need and change the return type. !!!
        /// </summary>
        private void GetDataFromXena(Xena xena)
        {
            using (HttpClient httpClient = xena.CallXena())
            {
                var result = httpClient.GetStringAsync("98437/FiscalPeriod").Result;

                //return result;
            }
        }
    }
}