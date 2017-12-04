using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    //Claus
    //Containing the variables consumed from account og ledgertag api's
    //from here variables are persisted to the Db.
    public class LedgerTags
    {
        // Data containing ledger tag 
        public LedgerTags(JObject jObject)
        {
            ledgerTagId = int.Parse(jObject["Id"].ToString());
            shortDescribtion = (jObject["ShortDescription"].ToString());
            longDescribtion = (jObject["LongDescription"].ToString());
        }
        public int ledgerTagId { get; set; }
        public string shortDescribtion { get; set; }
        public string longDescribtion { get; set; }
    }

    // Data containing ledger group
    public class LedgerAccounts
    {
        public LedgerAccounts(JObject jObject)
        {
            ledgerAccountId = (jObject["Value"].ToString());
            accountName = (jObject["Text"].ToString());
        }
        public string ledgerAccountId { get; set; }
        public string accountName { get; set; }
    }
}