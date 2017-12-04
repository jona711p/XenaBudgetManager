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
        //GET:  list of ledgeraccount objects - the group names
        public static List<LedgerAccounts> LedgerAccount(Xena xena) //Takes the helper object xena to easier connect to xena
        {
            List<LedgerAccounts> LedgerAccount = new List<LedgerAccounts>();

            using (HttpClient httpClient = xena.CallXena()) //instantite httpclient using the cll xena helper
            {
              
                JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/LedgerTagGroup/LedgerAccount").Result);

                foreach (JObject jObject in jArray)
                {
                    LedgerAccount.Add(new LedgerAccounts(jObject));
                }
            }
            return LedgerAccount;         
        }
        //GET:  list of ledgertag objects - the account names
        public static List<LedgerTags> LedgerTag(Xena xena) //Takes the helper object xena to easier connect to xena
        {
            List<LedgerTags> LedgerTag = new List<LedgerTags>();

            using (HttpClient httpClient = xena.CallXena()) //instantite httpclient using the cll xena helper
            {

                JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/LedgerTag").Result);

                foreach (JObject jObject in jArray)
                {
                    LedgerTag.Add(new LedgerTags(jObject));
                }
            }
            return LedgerTag;
        }


        //// GET: GetXenaData - fiscal period data
        public ActionResult LedgerGroupData(Xena xena)
        {
            List<LedgerGroupData> LedgerGroupData = new List<LedgerGroupData>();

            using (HttpClient httpClient = xena.CallXena()) //instantite httpclient using the cll xena helper
            {
                
              JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/Transaction/LedgerGroupData?fiscalPeriodId=169626878&FiscalDateFrom=17167&FiscalDateTo=17530").Result);

                foreach (JObject jObject in jArray)
                {
                    LedgerGroupData.Add(new LedgerGroupData(jObject));
                }
            }
            return View(LedgerGroupData);
        }
        //// GET: GetXenaData - detailed period data
        public ActionResult LedgerGroupDetailData(Xena xena)
        {
            List<LedgerGroupDetailData> LedgerGroupDetailData = new List<LedgerGroupDetailData>();

            using (HttpClient httpClient = xena.CallXena()) //instantite httpclient using the cll xena helper
            {

                JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/Transaction/LedgerGroupDataDetail?fiscalPeriodId=169626878&FiscalDateFrom=17197&FiscalDateTo=17535&ledgerAccount=Xena_Domain_Income_Accounts_Net_Turn_Over&_=1512035981799").Result);

                foreach (JObject jObject in jArray)
                {
                    LedgerGroupDetailData.Add(new LedgerGroupDetailData(jObject));
                }
            }
            return View(LedgerGroupDetailData);
        }



    }
}
