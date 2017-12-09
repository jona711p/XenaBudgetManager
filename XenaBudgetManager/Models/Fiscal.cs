using Newtonsoft.Json.Linq;

namespace XenaBudgetManager.Models
{
    public class Fiscal
    {
        public int FiscalID { get; set; }
        public string FiscalSetupName { get; set; }
        public int UserID { get; set; }

        public Fiscal()
        {
        }

        public Fiscal(JToken jToken)
        {
            FiscalID = int.Parse(jToken["FiscalSetupId"].ToString());
            FiscalSetupName = jToken["FiscalSetupName"].ToString();
            UserID = int.Parse(jToken["UserId"].ToString());
        }
    }
}