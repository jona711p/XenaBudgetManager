using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    public class XenaDataModel
    {
        // Months
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July  { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }

        // Finance Groups
        public string Revenue { get; set; }
        public string ProductConsumption { get; set; }
        public string StaffCost { get; set; }
        public string LocalCost { get; set; }
        public string TheResultOfTheYear { get; set; }



        // finance Accounts
        public string Revenue_DogFood { get; set; }
        public string Revenue_CatFood { get; set; }
        public string ProductConsumption_DogFood { get; set; }
        public string ProductConsumption_CatFood { get; set; }
        public string StaffCost_Pay { get; set; }
        public string LocalCost_Rent { get; set; }
        public string LocalCost_Light_Heat { get; set; }
    }
}