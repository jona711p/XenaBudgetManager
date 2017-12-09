using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Classes
{
    public class GetXenaData
    {
        //GET:  list of ledgeraccount objects - the group names
        public static List<LedgerAccounts> LedgerAccount(string token) //Takes the helper object xena to easier connect to xena
        {
            List<LedgerAccounts> LedgerAccountList = new List<LedgerAccounts>();

            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = XenaLogic.CallXena(token,
                "Fiscal/98437/LedgerTagGroup/LedgerAccount");
            //take each token in the token list and add them to the ledgeraccount list
            foreach (JToken jToken in jTokenList)
            {
                LedgerAccountList.Add(new LedgerAccounts(jToken)); // Adds each Entity to a Ledger account list
            }

            return LedgerAccountList;

            //TODO: code that takes the list and persists it to the db
        }

        //GET:  list of ledgertag objects - the account names
        public static List<LedgerTags> LedgerTag(string token) //Takes the helper object xena to easier connect to xena
        {
            List<LedgerTags> LedgerTagList = new List<LedgerTags>();

            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = XenaLogic.CallXena(token,
                "Fiscal/98437/LedgerTag");
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                if (jToken["Id"].Type != JTokenType.Null && int.Parse(jToken["Id"].ToString()) != 0)
                {
                    if (int.Parse(jToken["Number"].ToString()) != 8900 && int.Parse(jToken["Number"].ToString()) != 8920)
                    {
                        LedgerTagList.Add(new LedgerTags(jToken)); // Adds each Entity to a LedgerTag list
                    }
                }
            }

            //TODO: code that takes the list and persists it to the db
            //DB.WriteNewLedgerTag(LedgerTagList);

            return LedgerTagList;
        }

        // GET: GetXenaData - fiscal period data
        public static List<LedgerGroupData> LedgerGroupData(string token, DateTime fromData, DateTime toData)
        {
            //create an instanse of a ledgergroupdata
            List<LedgerGroupData> ledgerGroupDataList = new List<LedgerGroupData>(); // A new empty list of LedgerGroupData
                                                                                     //
                                                                                     //create a list of the tokens received from xena - tokens here are key/value pairs
                                                                                     //Next we call xena, pass in the accesstoken, to retrieve our data from the api

            long fromXenaEpoch = TimeInEpoch(fromData);
            long toXenaEpoch = TimeInEpoch(toData);

            string url = "Fiscal/98437/Transaction/LedgerGroupData?fiscalPeriodId=169626878&" +
                          "FiscalDateFrom=" + fromXenaEpoch + "&" +
                          "FiscalDateTo=" + toXenaEpoch;

            List<JToken> jTokenList = XenaLogic.CallXena(token, url); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ledgerGroupDataList.Add(new LedgerGroupData(jToken)); // Adds each Entity to a LedgerGroupData
            }

            foreach (LedgerGroupData ledgerGroupData in ledgerGroupDataList)
            {
                ledgerGroupData.LedgerGroupDetailDataList = LedgerGroupDetailData(token, ledgerGroupData.Group, fromXenaEpoch, toXenaEpoch);
            }

            return ledgerGroupDataList;
        }

        public static List<LedgerGroupDetailData> LedgerGroupDetailData(string token, string group, long fromXenaEpoch, long toXenaEpoch)
        {
            //create an instanse of a ledgergroupdata
            List<LedgerGroupDetailData> ledgerGroupDetailDataList = new List<LedgerGroupDetailData>();

            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api

            string url = "Fiscal/98437/Transaction/LedgerGroupDataDetail?fiscalPeriodId=169626878&" +
                         "FiscalDateFrom=" + fromXenaEpoch + "&" +
                         "FiscalDateTo=" + toXenaEpoch + "&" +
                         "ledgerAccount=" + group +
                         "&_=1512035981799";

            List<JToken> jTokenList = XenaLogic.CallXena(token, url); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ledgerGroupDetailDataList.Add(new LedgerGroupDetailData(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return ledgerGroupDetailDataList;
        }

        private static long TimeInEpoch(DateTime dateTime)
        {
            long unixTimeSeconds = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
            return unixTimeSeconds / 60 / 60 / 24;
        }

        public static List<ExtraLedgerTag> GetRevenueTag(string token)
        {
            List<ExtraLedgerTag> ExtraLedgerTagList = new List<ExtraLedgerTag>();
            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = XenaLogic.CallXena(token,
                "Fiscal/98437/Transaction/LedgerGroupDataDetail?fiscalPeriodId=169626878&FiscalDateFrom=17197&FiscalDateTo=17535&ledgerAccount=Xena_Domain_Income_Accounts_Net_Turn_Over&_=1512035981799"); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ExtraLedgerTagList.Add(new ExtraLedgerTag(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return ExtraLedgerTagList;
        }

        public static List<ExtraLedgerTag> GetProductTag(string token)
        {
            List<ExtraLedgerTag> ExtraLedgerTagList = new List<ExtraLedgerTag>();
            //create a list of the tokens received from xena - tokens here are key/value pairs
            //Next we call xena, pass in the accesstoken, to retrieve our data from the api
            List<JToken> jTokenList = XenaLogic.CallXena(token,
                "Fiscal/98437/Transaction/LedgerGroupDataDetail?fiscalPeriodId=169626878&FiscalDateFrom=17197&FiscalDateTo=17535&ledgerAccount=Xena_Domain_Income_Accounts_Product_Consumption&_=1512035981799"); // List with JTokens from Xena's Array
            //take each token in the token list and add them to the ledgergroup list
            foreach (JToken jToken in jTokenList)
            {
                ExtraLedgerTagList.Add(new ExtraLedgerTag(jToken)); // Adds each Entity to a LedgerGroupData
            }

            return ExtraLedgerTagList;
        }
    }
}