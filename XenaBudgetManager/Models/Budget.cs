using System.Collections.Generic;
using System.ComponentModel;

namespace XenaBudgetManager.Models
{
    public class Budget
    {
        [DisplayName("Budget ID")]
        public int budgetID { get; set; }

        [DisplayName("Budgetnavn")]
        public string budgetName { get; set; }

        [DisplayName("Xena Finans ID")]
        public int XenaFiscalID { get; set; }

        [DisplayName("Budgetår")]
        public int budgetYear { get; set; }

        [DisplayName("Brugerliste")]
        public List<User> userList { get; set; }

        [DisplayName("Gruppeliste")]
        public List<AccountGroup> groupList { get; set; }
    }
}