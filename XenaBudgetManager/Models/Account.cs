using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace XenaBudgetManager.Models
{
    public class Account
    {
        /// <summary>
        /// Written by Claus, Jonas, Mikael and Thomas
        /// </summary>
        public Account()
        {
        }

        public Account(DataRow row)
        {
            accountName = row["AccountName"].ToString();
            January = int.Parse(row["January"].ToString());
            February = int.Parse(row["February"].ToString());
            March = int.Parse(row["March"].ToString());
            April = int.Parse(row["April"].ToString());
            May = int.Parse(row["May"].ToString());
            June = int.Parse(row["June"].ToString());
            July = int.Parse(row["July"].ToString());
            August = int.Parse(row["August"].ToString());
            September = int.Parse(row["September"].ToString());
            October = int.Parse(row["October"].ToString());
            November = int.Parse(row["November"].ToString());
            December = int.Parse(row["December"].ToString());
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