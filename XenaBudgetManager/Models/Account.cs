using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XenaBudgetManager.Models
{
    public class Account
    {
        public Account()
        {
            
        }
        public Account(Account account)
        {
            
        }
        public Account(LedgerTags inputData)
        {
            accountID = inputData.ledgerTagId;
            accountName = inputData.longDescription;
        }
        public static List<Account> ConvertLedgerTagsToAccountList(List<LedgerTags> inputData)
        {
            List<Account> convertedList = new List<Account>();

            for (int i = 0; i < inputData.Count; i++)
            {
                convertedList.Add(new Account(inputData[i]));
            }

            return convertedList;
        }
        [DisplayName("Konto ID")]
        public int accountID { get; set; }

        [DisplayName("Kontonavn")]
        public string accountName { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Januar")]
        public double January { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Februar")]
        public double February { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Marts")]
        public double March { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("April")]
        public double April { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Maj")]
        public double May { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Juni")]
        public double June { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Juli")]
        public double July { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("August")]
        public double August { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("September")]
        public double September { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Oktober")]
        public double October { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("November")]
        public double November { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("December")]
        public double December { get; set; }

        [DisplayName("Total")]
        public double Total { get; set; }
    }
}