using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XenaBudgetManager.Models
{
    public class User
    {
        [DisplayName("User ID")]
        public int userID { get; set; }
    }
}