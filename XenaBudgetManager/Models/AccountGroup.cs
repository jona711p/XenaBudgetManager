using System.Collections.Generic;
using System.ComponentModel;

namespace XenaBudgetManager.Models
{
    public class AccountGroup
    {
        [DisplayName("Kontogruppe ID")]
        public int accountGroupID { get; set; }

        [DisplayName("Kontogruppenavn")]
        public string accountGroupName { get; set; }

        [DisplayName("Kontoliste")]
        public List<Account> accountList { get; set; }
    }
}