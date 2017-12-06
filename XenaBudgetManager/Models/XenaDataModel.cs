using Newtonsoft.Json.Linq;

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
        public string accountID { get; set; }

        public LedgerTags()
        {
        }

        public LedgerTags(JToken jToken)
        {
            ledgerTagId = int.Parse(jToken["Id"].ToString());
            shortDescription = jToken["ShortDescription"].ToString();
            longDescription = jToken["LongDescription"].ToString();
            accountID = jToken["LedgerAccount"].ToString();
        }
    }

    // Containing ledger accounts
    public class LedgerAccounts
    {
        public string ledgerAccountId { get; set; }
        public string accountName { get; set; }

        public LedgerAccounts()
        {
        }

        public LedgerAccounts(JToken jToken)
        {
            ledgerAccountId = jToken["Value"].ToString();
            accountName = jToken["Text"].ToString();
        }
    }

    public class LedgerGroupData
    {
        public int AmountMonth { get; set; }
        public int? AmountMonthDebit { get; set; }
        public int? AmountMonthCredit { get; set; }
        public string Group { get; set; }
        public int AmountYearToDate { get; set; }
        public int? AmountYearToDateDebit { get; set; }
        public int? AmountYearToDateCredit { get; set; }
        public string TranslatedGroup { get; set; }

        public LedgerGroupData()
        {
        }

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
    }

    public class LedgerGroupDetailData
    {
        public string Controller { get; set; }
        public string ControllerAction { get; set; }
        public string Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public int AmountMonth { get; set; }
        public int? AmountMonthDebit { get; set; }    //obj ti int ?
        public int AmountMonthCredit { get; set; }
        public int AmountYearToDate { get; set; }
        public int? AmountYearToDateDebit { get; set; }//obj to int ?
        public int AmountYearToDateCredit { get; set; }
        public string LedgerAccount { get; set; }
        public string Group { get; set; }
        public int GroupIndex { get; set; }

        public LedgerGroupDetailData()
        {
        }

        public LedgerGroupDetailData(JToken jToken)
        {
            Controller = jToken["Controller"].ToString();
            ControllerAction = jToken["ControllerAction"].ToString();
            Id = jToken["Id"].ToString();
            AccountNumber = int.Parse(jToken["AccountNumber"].ToString());
            AccountDescription = jToken["AccountDescription"].ToString();
            Description = jToken["Description"].ToString();
            AmountMonth = int.Parse(jToken["AmountMonth"].ToString());
            AmountMonthDebit = jToken["AmountMonthDebit"].Type == JTokenType.Null ? null : AmountMonthDebit = int.Parse(jToken["AmountMonthDebit"].ToString());
            AmountMonthCredit = int.Parse(jToken["AmountMonthCredit"].ToString());
            AmountYearToDate = int.Parse(jToken["AmountYearToDate"].ToString());
            AmountYearToDateDebit = jToken["AmountYearToDateDebit"].Type == JTokenType.Null ? null : AmountYearToDateDebit = int.Parse(jToken["AmountYearToDateDebit"].ToString());
            AmountYearToDateCredit = int.Parse(jToken["AmountYearToDateCredit"].ToString());
            LedgerAccount = jToken["LedgerAccount"].ToString();
            Group = jToken["Group"].ToString();
            GroupIndex = int.Parse(jToken["GroupIndex"].ToString());
        }
    }
}