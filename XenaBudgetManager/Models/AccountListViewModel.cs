using System.Collections.Generic;

namespace XenaBudgetManager.Models
{
    /// <summary>
    /// Written by Thomas
    /// </summary>
    public class AccountListViewModel
    {
        public AccountListViewModel()
        {
            accountList = new List<Account>();
        }

        public List<Account> accountList { get; set; }
    }
}