using System.Collections.Generic;

namespace XenaBudgetManager.Models
{
    public class AccountGroupListViewModel
    {
        public AccountGroupListViewModel()
        {
            groupList = new List<AccountGroup>();
        }
        public List<AccountGroup> groupList { get; set; }
    }
}