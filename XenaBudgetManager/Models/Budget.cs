using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace XenaBudgetManager.Models
{
    public class Budget
    {
        public Budget()
        {
        }

        public Budget(DataRow row)
        {
            budgetID = int.Parse(row["BudgetID"].ToString());
            budgetName = row["BudgetName"].ToString();
            budgetYear = int.Parse(row["Year"].ToString());
            XenaFiscalID = int.Parse(row["XenaFiscalID"].ToString());

            AccountList.Add(new Account(row));


        }

        [DisplayName("Budget ID")]
        public int budgetID { get; set; }

        [RegularExpression(@"^[^<>{}\[\]]+$")]
        [DisplayName("Budgetnavn")]
        public string budgetName { get; set; }

        [DisplayName("Xena Finans ID")]
        public int XenaFiscalID { get; set; }

        [Range(1970, 9999)]
        [DisplayName("Budgetår")]
        public int budgetYear { get; set; }

        [DisplayName("Gruppeliste")]
        public AccountGroupListViewModel groupList { get; set; }

        public List<Account> AccountList { get; set; }

        public List<LedgerGroupData> LedgerGroupDataList { get; set; }

        public List<LedgerGroupDetailData> LedgerGroupDetailDataList { get; set; }
    }
}