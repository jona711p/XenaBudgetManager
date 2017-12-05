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
            List<LedgerAccounts> LedgerAccountList = new List<LedgerAccounts>();
            
            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = Xena.CallXena(Request.Cookies["access_token"].Value,
                "Fiscal/98437/LedgerTagGroup/LedgerAccount");
            //take each token in the token list and add them to the ledgeraccount list
            foreach (JToken jToken in jTokenList)
            {
                LedgerAccountList.Add(new LedgerAccounts(jToken)); // Adds each Entity to a Ledger account list
            }

            return LedgerAccountList;
        }

        //GET:  list of ledgertag objects - the account names
        public List<LedgerTags> LedgerTag() //Takes the helper object xena to easier connect to xena
        {
            List<LedgerTags> LedgerTagList = new List<LedgerTags>();

            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = Xena.CallXena(Request.Cookies["access_token"].Value,
                "Fiscal/98437/LedgerTag");
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                LedgerTagList.Add(new LedgerTags(jToken)); // Adds each Entity to a LedgerTag list
            }

            return LedgerTagList;
        }

        //// GET: GetXenaData - fiscal period data
        public ActionResult LedgerGroupData()
        {   
            //create an instanse of a ledgergroupdata
            List<LedgerGroupData> ledgerGroupDataList = new List<LedgerGroupData>(); // A new empty list of LedgerGroupData
                                                                                     //
            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = Xena.CallXena(Request.Cookies["access_token"].Value,
                "Fiscal/98437/Transaction/LedgerGroupData?fiscalPeriodId=169626878&FiscalDateFrom=17167&FiscalDateTo=17530"); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ledgerGroupDataList.Add(new LedgerGroupData(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return View(ledgerGroupDataList);
        }

        public ActionResult LedgerGroupDetailData()
        {
            //create an instanse of a ledgergroupdata
            List<LedgerGroupDetailData> ledgerGroupDetailDataList = new List<LedgerGroupDetailData>(); 
            
            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = Xena.CallXena(Request.Cookies["access_token"].Value,
                "Fiscal/98437/Transaction/LedgerGroupDataDetail?fiscalPeriodId=169626878&FiscalDateFrom=17197&FiscalDateTo=17535&ledgerAccount=Xena_Domain_Income_Accounts_Net_Turn_Over&_=1512035981799"); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ledgerGroupDetailDataList.Add(new Models.LedgerGroupDetailData(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return View(ledgerGroupDetailDataList);
        }
    }
}