﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    public class Account
    {
        [DisplayName("Konto ID")]
        public int accountID { get; set; }

        [DisplayName("Kontonavn")]
        public string accountName { get; set; }

        [DisplayName("Januar")]
        public double January { get; set; }

        [DisplayName("Februar")]
        public double February { get; set; }


        [DisplayName("Marts")]
        public double March { get; set; }


        [DisplayName("April")]
        public double April { get; set; }


        [DisplayName("Maj")]
        public double May { get; set; }


        [DisplayName("Juni")]
        public double June { get; set; }


        [DisplayName("Juli")]
        public double July { get; set; }


        [DisplayName("August")]
        public double August { get; set; }


        [DisplayName("September")]
        public double September { get; set; }


        [DisplayName("Oktober")]
        public double October { get; set; }


        [DisplayName("November")]
        public double November { get; set; }


        [DisplayName("December")]
        public double December { get; set; }


        [DisplayName("Total")]
        public double Total { get; set; }


    }
}