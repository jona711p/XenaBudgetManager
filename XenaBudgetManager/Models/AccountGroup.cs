using System.Collections.Generic;
using System.ComponentModel;

namespace XenaBudgetManager.Models
{
    /// <summary>
    /// Written by Claus, Mikael and Thomas
    /// </summary>
    public class AccountGroup
    {
        public AccountGroup()
        {
            
        }

        public AccountGroup(LedgerAccounts inputData)
        {
            accountGroupID = inputData.ledgerAccountId;
            accountGroupName = inputData.accountName;
        }

        public static List<AccountGroup> ConvertLedgerAccountToAccountGroupList(List<LedgerAccounts> inputGroups, List<Account> inputAccounts, List<KeyValuePair<int, int>> inputAccountPlan)
        {
            List<AccountGroup> convertedList = new List<AccountGroup>();

            for (int i = 0; i < inputGroups.Count; i++)
            {
                convertedList.Add(new AccountGroup(inputGroups[i]));
            }

            return convertedList;
        }

        [DisplayName("Kontogruppe ID")]
        public int accountGroupID { get; set; }

        [DisplayName("Kontogruppenavn")]
        public string accountGroupName { get; set; }

        [DisplayName("Kontoliste")]
        public AccountListViewModel accountList { get; set; }
    }
}