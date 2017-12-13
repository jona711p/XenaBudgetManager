using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

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
            accountID = int.Parse(row["LedgerTagID"].ToString());
            accountName = row["LongDescription"].ToString();

            January = double.Parse(row["January"].ToString());
            February = double.Parse(row["February"].ToString());
            March = double.Parse(row["March"].ToString());
            April = double.Parse(row["April"].ToString());
            May = double.Parse(row["May"].ToString());
            June = double.Parse(row["June"].ToString());
            July = double.Parse(row["July"].ToString());
            August = double.Parse(row["August"].ToString());
            September = double.Parse(row["September"].ToString());
            October = double.Parse(row["October"].ToString());
            November = double.Parse(row["November"].ToString());
            December = double.Parse(row["December"].ToString());
        }
        public Account(DataRow row, string[] months)
        {
            accountName = row["AccountName"].ToString();

            if (months.Contains("January"))
            {
                January = double.Parse(row["January"].ToString());
                Total = Total + January;
            }

            if (months.Contains("February"))
            {
                February = double.Parse(row["February"].ToString());
                Total = Total + February;
            }

            if (months.Contains("March"))
            {
                March = double.Parse(row["March"].ToString());
                Total = Total + March;
            }

            if (months.Contains("May"))
            {
                May = double.Parse(row["May"].ToString());
                Total = Total + May;
            }

            if (months.Contains("June"))
            {
                June = double.Parse(row["June"].ToString());
                Total = Total + June;
            }

            if (months.Contains("July"))
            {
                July = double.Parse(row["July"].ToString());
                Total = Total + July;
            }

            if (months.Contains("August"))
            {
                August = double.Parse(row["August"].ToString());
                Total = Total + August;
            }

            if (months.Contains("September"))
            {
                September = double.Parse(row["September"].ToString());
                Total = Total + September;
            }

            if (months.Contains("October"))
            {
                October = double.Parse(row["October"].ToString());
                Total = Total + October;
            }

            if (months.Contains("November"))
            {
                November = double.Parse(row["November"].ToString());
                Total = Total + November;
            }

            if (months.Contains("December"))
            {
                December = double.Parse(row["December"].ToString());
                Total = Total + December;
            }
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