using System.ComponentModel;

namespace XenaBudgetManager.Models
{
    public class Budget
    {
        public Budget()
        {
            
        }
        [DisplayName("Budget ID")]
        public int budgetID { get; set; }

        [DisplayName("Budgetnavn")]
        public string budgetName { get; set; }

        [DisplayName("Xena Finans ID")]
        public int XenaFiscalID { get; set; }

        [DisplayName("Budgetår")]
        public int budgetYear { get; set; }

        //[DisplayName("Brugerliste")]
        //public List<Fiscal> userList { get; set; }

        [DisplayName("Gruppeliste")]
        public AccountGroupListViewModel groupList { get; set; }
    }
}