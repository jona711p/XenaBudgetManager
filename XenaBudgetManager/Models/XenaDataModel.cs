using Newtonsoft.Json.Linq;

namespace XenaBudgetManager.Models
{
    //Claus
    //Containing the variables consumed from account og ledgertag api's
    //from here variables are persisted to the Db.

    // Containing ledger tag  which is the actual account numbers
    public class LedgerTags
    {
        public LedgerTags(JObject jObject)
        {
            ledgerTagId = int.Parse(jObject["Id"].ToString());
            shortDescription = jObject["ShortDescription"].ToString();
            longDescription = jObject["LongDescription"].ToString();
        }

        public int ledgerTagId { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
    }

    // Containing ledger accounts
    public class LedgerAccounts
    {
        public LedgerAccounts(JObject jObject)
        {
            //ledgerAccountId = jToken["Value"].ToString();
            accountName = jObject["Text"].ToString();
        }
        public string ledgerAccountId { get; set; }
        public string accountName { get; set; }
    }

    public class LedgerGroupData
    {
        public LedgerGroupData(JToken jToken) // Changed from JObject to JToken, and added "if null" to int?'s
        {
            AmountMonth = int.Parse(jToken["AmountMonth"].ToString());
            AmountMonthDebit = jToken["AmountMonthDebit"].Type == JTokenType.Null ? null : AmountMonthDebit = int.Parse(jToken["AmountMonthDebit"].ToString());
            AmountMonthCredit = jToken["AmountMonthCredit"].Type == JTokenType.Null ? null : AmountMonthDebit = int.Parse(jToken["AmountMonthCredit"].ToString());
            Group = jToken["Group"].ToString();
            AmountYearToDate = int.Parse(jToken["AmountYearToDate"].ToString());
            AmountYearToDateDebit = jToken["AmountYearToDateDebit"].Type == JTokenType.Null ? null : AmountMonthDebit = int.Parse(jToken["AmountYearToDateDebit"].ToString());
            AmountYearToDateCredit = jToken["AmountYearToDateCredit"].Type == JTokenType.Null ? null : AmountMonthDebit = int.Parse(jToken["AmountYearToDateCredit"].ToString());
            TranslatedGroup = jToken["TranslatedGroup"].ToString();
        }

        public LedgerGroupData()
        {
        }

        public int AmountMonth { get; set; }
        public int? AmountMonthDebit { get; set; }
        public int? AmountMonthCredit { get; set; }
        public string Group { get; set; }
        public int AmountYearToDate { get; set; }
        public int? AmountYearToDateDebit { get; set; }
        public int? AmountYearToDateCredit { get; set; }
        public string TranslatedGroup { get; set; }
    }

    public class LedgerGroupDetailData
    {
        public LedgerGroupDetailData(JObject jObject)
        {
            Controller = jObject["Controller"].ToString();
            ControllerAction = jObject["ControllerAction"].ToString();
            Id = jObject["Id"].ToString();
            AccountNumber = int.Parse(jObject["AccountNumber"].ToString());
            AccountDescription = jObject["AccountDescription"].ToString();
            Description = jObject["Description"].ToString();
            AmountMonth = int.Parse(jObject["AmountMonth"].ToString());
            AmountMonthDebit = int.Parse(jObject["AmountMonthDebit"].ToString());
            AmountMonthCredit = int.Parse(jObject["AmountMonthCredit"].ToString());
            AmountYearToDate = int.Parse(jObject["AmountYearToDate"].ToString());
            AmountYearToDateDebit = int.Parse(jObject["AmountYearToDateDebit"].ToString());
            AmountYearToDateCredit = int.Parse(jObject["AmountYearToDateCredit"].ToString());
            LedgerAccount = jObject["LedgerAccount"].ToString();
            Group = jObject["Group"].ToString();
            GroupIndex = int.Parse(jObject["GroupIndex"].ToString());
        }

        public string Controller { get; set; }
        public string ControllerAction { get; set; }
        public string Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public int AmountMonth { get; set; }
        public object AmountMonthDebit { get; set; }
        public int AmountMonthCredit { get; set; }
        public int AmountYearToDate { get; set; }
        public object AmountYearToDateDebit { get; set; }
        public int AmountYearToDateCredit { get; set; }
        public string LedgerAccount { get; set; }
        public string Group { get; set; }
        public int GroupIndex { get; set; }
    }
}