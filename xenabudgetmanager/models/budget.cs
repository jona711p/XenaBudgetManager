using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XenaBudgetManager.Models
{
    public class Budget
    {
        public Budget()
        {
            //CUNT
        }
        [DisplayName("Budget ID")]
        public int budgetID { get; set; }

        [RegularExpression(@"^[^<>{}\[\]]+$")]
        [DisplayName("Budgetnavn")]
        public string budgetName { get; set; }

        [DisplayName("Xena Finans ID")]
        public int XenaFiscalID { get; set; }

        [Range(1970,9999)]
        [DisplayName("Budgetår")]
        public int budgetYear { get; set; }

        //[DisplayName("Brugerliste")]
        //public List<Fiscal> userList { get; set; }

        [DisplayName("Gruppeliste")]
        public AccountGroupListViewModel groupList { get; set; }
    }
}