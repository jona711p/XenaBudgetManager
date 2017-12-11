using Newtonsoft.Json.Linq;

namespace XenaBudgetManager.Models
{

    /// <summary>
    /// Written by Jonas
    /// </summary>
    public class Fiscal
    {
        public int FiscalID { get; set; }
        public string FiscalSetupName { get; set; }

        public Fiscal()
        {
        }

        public Fiscal(JToken jToken)
        {
            FiscalID = int.Parse(jToken["FiscalSetupId"].ToString());
            FiscalSetupName = jToken["FiscalSetupName"].ToString();
        }
    }
}