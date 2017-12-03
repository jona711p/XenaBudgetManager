using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    public class AccountingModel
    {
        // Month
        [DisplayName("Month")]
        public string Month { get; set; }

        [DisplayName("MonthResult")]
        public int MonthResult { get; set; }

        // Finance Groups
        [DisplayName("Netto Omsætning")]
        public int Revenue { get; set; }

        [DisplayName("Vareforbrug")]
        public int ProductConsumption { get; set; }

        [DisplayName("Personale Omkostninger")]
        public int StaffCost { get; set; }

        [DisplayName("Lokale Omkostninger")]
        public int LocalCost { get; set; }

        [DisplayName("Årets Resultat")]
        public int TheResultOfTheYear { get; set; }



        // finance Accounts
        [DisplayName("--Hunde Mad")]
        public int Revenue_DogFood { get; set; }

        [DisplayName("--Katte Mad")]
        public int Revenue_CatFood  { get; set; }

        [DisplayName("--Hunde Mad")]
        public int ProductConsumption_DogFood { get; set; }

        [DisplayName("--Katte Mad")]
        public int ProductConsumption_CatFood { get; set; }

        [DisplayName("--Løn")]
        public int StaffCost_Pay { get; set; }

        [DisplayName("--Leje")]
        public int LocalCost_Rent { get; set; }

        [DisplayName("--Lys/Varme")]
        public int LocalCost_Light_Heat { get; set; }

    }
}