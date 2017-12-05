﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Controllers
{
    public class GetXenaDataController : Controller
    {
        //GET:  list of ledgeraccount objects - the group names
        public List<LedgerAccounts> LedgerAccount() //Takes the helper object xena to easier connect to xena
        {
            List<LedgerAccounts> LedgerAccount = new List<LedgerAccounts>();

            //using (HttpClient httpClient = Xena.CallXena(Request.Cookies["access_token"].Value)) //instantite httpclient using the call xena helper
            //{

            //    JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/LedgerTagGroup/LedgerAccount").Result);

            //    foreach (JObject jObject in jArray)
            //    {
            //        LedgerAccount.Add(new LedgerAccounts(jObject));
            //    }
            //}

            return LedgerAccount;
        }

        //GET:  list of ledgertag objects - the account names
        public List<LedgerTags> LedgerTag() //Takes the helper object xena to easier connect to xena
        {
            List<LedgerTags> LedgerTag = new List<LedgerTags>();

            //using (HttpClient httpClient = Xena.CallXena(Request.Cookies["access_token"].Value)) //instantite httpclient using the call xena helper
            //{

            //    JArray jArray = JArray.Parse(httpClient.GetStringAsync("Fiscal/98437/LedgerTag").Result);

            //    foreach (JObject jObject in jArray)
            //    {
            //        LedgerTag.Add(new LedgerTags(jObject));
            //    }
            //}

            return LedgerTag;
        }

        //// GET: GetXenaData - fiscal period data
        public ActionResult LedgerGroupData()
        {
            List<LedgerGroupData> ledgerGroupDataList = new List<LedgerGroupData>(); // A new empty list of LedgerGroupData

            List<JToken> jTokenList = Xena.CallXena(Request.Cookies["access_token"].Value,
                "Fiscal/98437/Transaction/LedgerGroupData?fiscalPeriodId=169626878&FiscalDateFrom=17167&FiscalDateTo=17530"); // List with JTokens from Xena's Array

            foreach (JToken jToken in jTokenList)
            {
                ledgerGroupDataList.Add(new LedgerGroupData(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return View(ledgerGroupDataList);
        }
    }
}