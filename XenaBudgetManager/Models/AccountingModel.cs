using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    public class AccountingModel
    {
        // Months
        [DisplayName("Januar")]
        public string January { get; set; }

        [DisplayName("Februar")]
        public string February { get; set; }

        [DisplayName("Marts")]
        public string March { get; set; }

        [DisplayName("April")]
        public string April { get; set; }

        [DisplayName("May")]
        public string May { get; set; }

        [DisplayName("Juni")]
        public string June { get; set; }

        [DisplayName("July")]
        public string July { get; set; }

        [DisplayName("August")]
        public string August { get; set; }

        [DisplayName("September")]
        public string September { get; set; }

        [DisplayName("Oktober")]
        public string October { get; set; }

        [DisplayName("November")]
        public string November { get; set; }

        [DisplayName("December")]
        public string December { get; set; }



        // Finance Groups
        [DisplayName("Netto Omsætning")]
        public string Revenue { get; set; }

        [DisplayName("Vareforbrug")]
        public string ProductConsumption { get; set; }

        [DisplayName("Personale Omkostninger")]
        public string StaffCost { get; set; }

        [DisplayName("Lokale Omkostninger")]
        public string LocalCost { get; set; }

        [DisplayName("Årets Resultat")]
        public string TheResultOfTheYear { get; set; }



        // finance Accounts
        [DisplayName("--Hunde Mad")]
        public string Revenue_DogFood { get; set; }

        [DisplayName("--Katte Mad")]
        public string Revenue_CatFood { get; set; }

        [DisplayName("--Hunde Mad")]
        public string ProductConsumption_DogFood { get; set; }

        [DisplayName("--Katte Mad")]
        public string ProductConsumption_CatFood { get; set; }

        [DisplayName("--Løn")]
        public string StaffCost_Pay { get; set; }

        [DisplayName("--Leje")]
        public string LocalCost_Rent { get; set; }

        [DisplayName("--Lys/Varme")]
        public string LocalCost_Light_Heat { get; set; }
    }
}