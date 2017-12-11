﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace XenaBudgetManager.Models
{
    //Claus
    //Containing the variables consumed from account og ledgertag api's
    //from here variables are persisted to the Db.

    // Containing ledger tag  which is the actual account numbers
    public class LedgerTags
    {
        public int ledgerTagId { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string ledgerAccountXena { get; set; }


        public LedgerTags()
        {
        }

        public LedgerTags(JToken jToken)
        {
                //ledgerTagId = int.Parse(jToken["Id"].ToString());
                shortDescription = jToken["ShortDescription"].ToString();
                longDescription = jToken["LongDescription"].ToString();
                ledgerAccountXena = jToken["LedgerAccount"].ToString();
        }

        public LedgerTags(ExtraLedgerTag input)
        {
            ledgerTagId = input.ledgerTagId;
            shortDescription = input.shortDescription;
            longDescription = input.longDescription;
            ledgerAccountXena = input.ledgerAccountXena;
        }
    }

    // Containing ledger accounts
    public class LedgerAccounts
    {
        public int ledgerAccountId { get; set; }
        public string ledgerAccountXena { get; set; }
        public string accountName { get; set; }

        public LedgerAccounts()
        {
        }

        public LedgerAccounts(JToken jToken)
        {
            ledgerAccountXena = jToken["Value"].ToString();
            accountName = jToken["Text"].ToString();
        }
    }

    public class LedgerGroupData
    {
        [DisplayName("Periode")]
        public int AmountMonth { get; set; }
        public string Group { get; set; }
        [DisplayName("År til dato")]
        public int AmountYearToDate { get; set; }
        [DisplayName("Finanskonto")]
        public string TranslatedGroup { get; set; }
        public List<LedgerGroupDetailData> LedgerGroupDetailDataList { get; set; }
        public List<Account> AccountList { get; set; }

        public LedgerGroupData()
        {
        }

        public LedgerGroupData(JToken jToken) // Changed from JObject to JToken, and added "if null" to int?'s
        {
            AmountMonth = int.Parse(jToken["AmountMonth"].ToString());
            Group = jToken["Group"].ToString();
            AmountYearToDate = int.Parse(jToken["AmountYearToDate"].ToString());
            TranslatedGroup = jToken["TranslatedGroup"].ToString();
        }
    }

    public class LedgerGroupDetailData
    {
        public string AccountDescription { get; set; }
        public int AmountMonth { get; set; }
        public int AmountYearToDate { get; set; }
        public string Group { get; set; }    
        public LedgerGroupDetailData()
        {
        }

        public LedgerGroupDetailData(JToken jToken)
        {
            AccountDescription = jToken["AccountDescription"].ToString();
            AmountMonth = int.Parse(jToken["AmountMonth"].ToString());
            AmountYearToDate = int.Parse(jToken["AmountYearToDate"].ToString());
            Group = jToken["Group"].ToString();
        }

     
    }
    public class ExtraLedgerTag
    {
        public int ledgerTagId { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string ledgerAccountXena { get; set; }

        ExtraLedgerTag()
        {

        }
        public ExtraLedgerTag(JToken jToken)
        {
            //ledgerTagId = int.Parse(jToken["Id"].ToString().Substring(0,9)); 
            shortDescription = jToken["AccountNumber"].ToString();
            longDescription = jToken["Description"].ToString();
            ledgerAccountXena = jToken["LedgerAccount"].ToString();
        }
    }
}
