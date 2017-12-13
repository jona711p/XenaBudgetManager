using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using XenaBudgetManager.Models;

namespace XenaBudgetManager.Classes
{
    public class DB
    {
        /// <summary>
        ///     Written by Jonas
        ///     Establishes a connection to the DB
        /// </summary>
        public static SqlConnection ConnectToDB(SqlConnection connection)
        {
            if (connection == null)
                connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["XenaBudgetManager"].ConnectionString);
            connection.Open();


            return connection;
        }

        /// <summary>
        ///     Written by Jonas
        ///     Closes connection to the DB
        /// </summary>
        public static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
            }

            catch (Exception)
            {
            }

            return connection;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry to the ValueInterval table in DB with the postings of a given account
        /// </summary>
        public static void WriteValueInterval(Account inputData) //BRUGE´S IKKE?
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand(
                string.Format(
                    @"INSERT INTO ValueInterval(January, February, March, April, May, June, July, August, September, October, November, December, [total]) 
                        VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});",
                    inputData.January, inputData.February, inputData.March,
                    inputData.April, inputData.May, inputData.June, inputData.July,
                    inputData.August, inputData.September, inputData.October,
                    inputData.November, inputData.December, inputData.Total),
                connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry in  DB 'LedgerAccount' with related data
        /// </summary>
        public static List<LedgerAccounts>
            WriteNewLedgerAccount(List<LedgerAccounts> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (var i = 0; i < inputList.Count; i++)
            {
                var cmd = new SqlCommand(
                    @"INSERT INTO LedgerAccount(LedgerAccountXena, AccountName) 
                    VALUES(@LedgerAccountXena, @AccountName) SELECT SCOPE_IDENTITY()", connection);

                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                cmd.Parameters.AddWithValue("@AccountName", inputList[i].accountName);

                inputList[i].ledgerAccountId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();
            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry in  DB 'LedgerTag' with related data
        /// </summary>
        public static List<LedgerTags> WriteNewLedgerTag(List<LedgerTags> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);


            for (var i = 1; i < inputList.Count; i++)
            {
                var cmd = new SqlCommand(
                    @"INSERT INTO LedgerTag(ShortDescription, LongDescription) 
               VALUES (@ShortDescription, @LongDescription) SELECT SCOPE_IDENTITY()", connection);

                cmd.Parameters.AddWithValue("@ShortDescription", inputList[i].shortDescription);
                cmd.Parameters.AddWithValue("@LongDescription", inputList[i].longDescription);

                inputList[i].ledgerTagId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();
            }
            connection = DisconnectFromDB(connection);
            return inputList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry in  DB 'Rel_AccountPlan' with related data
        /// </summary>
        public static void WriteNewRel_AccountPlan(List<LedgerTags> inputTagList, List<LedgerAccounts> inputAccountList,
            int budgetID) //rettet efter XenaDataModel
        {
            for (var i = 0; i < inputTagList.Count; i++)
            for (var j = 0; j < inputAccountList.Count; j++)
                if (inputTagList[i].ledgerAccountXena == inputAccountList[j].ledgerAccountXena)
                    try
                    {
                        SqlConnection connection = null;
                        connection = ConnectToDB(connection);

                        var command = new SqlCommand(
                            string.Format(
                                @"INSERT INTO Rel_AccountPlan(FK_BudgetID, FK_LedgerAccountID, FK_LedgerTagID) VALUES ({0},{1},{2});",
                                budgetID, inputAccountList[j].ledgerAccountId, inputTagList[i].ledgerTagId),
                            connection);

                        command.ExecuteNonQuery();
                        connection = DisconnectFromDB(connection);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error at " + i + ". " + ex);
                    }
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry in  DB 'XenaFiscal' with related data
        /// </summary>
        public static void WriteNewFiscal(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand(
                string.Format(@"INSERT INTO XenaFiscal(XenaFiscal) 
                        VALUES ({0});", inputData.XenaFiscalID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        } //ikke i brug

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry in  DB 'Budget' with related data
        /// </summary>
        public static int WriteNewBudget(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand(
                string.Format(
                    @"INSERT INTO Budget(BudgetName, Year, XenaFiscalID) VALUES ('{0}', {1}, {2}) SELECT SCOPE_IDENTITY();",
                    inputData.budgetName, inputData.budgetYear, inputData.XenaFiscalID), connection);

            var tempData = command.ExecuteScalar();

            connection = DisconnectFromDB(connection);

            return int.Parse(tempData.ToString());
        }

        /// <summary>
        ///     Written Claus
        ///     Gets List of Budgets. Is used to populate selectedList i view where you pick a budget to compare.
        /// </summary>
        public static List<Budget> GetBudgetId()
        {
            var budgetList = new List<Budget>();
            var dt = new DataTable();

            SqlConnection conn = null;
            conn = ConnectToDB(conn);
            var cmd = new SqlCommand(
                @"select * FROM Budget", conn);

            dt.Load(cmd.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                var budget = new Budget();
                budget.budgetID = int.Parse(row["BudgetID"].ToString());
                budget.budgetName = row["BudgetName"].ToString();
                budgetList.Add(budget);
            }

            conn = DisconnectFromDB(conn);
            return budgetList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Establishes a connection to the DB
        /// </summary>
        public static List<LedgerAccounts> GetAccountIDs(List<LedgerAccounts> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (var i = 0; i < inputList.Count; i++)
            {
                var cmd = new SqlCommand(
                    @"select LedgerAccountID from LedgerAccount WHERE LedgerAccountXena =  @LedgerAccountXena",
                    connection);
                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                var data = cmd.ExecuteScalar();
                if (data != null)
                {
                    inputList[i].ledgerAccountId = int.Parse(data.ToString());
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.Parameters.Clear();

                    var cmd1 = new SqlCommand(
                        @"INSERT INTO LedgerAccount(LedgerAccountXena, AccountName) 
                    VALUES(@LedgerAccountXena, @AccountName); SELECT SCOPE_IDENTITY()", connection);

                    cmd1.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                    cmd1.Parameters.AddWithValue("@AccountName", inputList[i].accountName);

                    inputList[i].ledgerAccountId = int.Parse(cmd1.ExecuteScalar().ToString());
                    cmd1.Parameters.Clear();
                }
            }
            connection = DisconnectFromDB(connection);


            return inputList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static List<LedgerTags> GetTagIDs(List<LedgerTags> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (var i = 0; i < inputList.Count; i++)
            {
                var cmd = new SqlCommand(
                    @"select LedgerTagID from LedgerTag WHERE longDescription =  @longDescription", connection);
                cmd.Parameters.AddWithValue("@longDescription", inputList[i].longDescription);

                var data = cmd.ExecuteScalar();
                if (data != null)
                {
                    inputList[i].ledgerTagId = int.Parse(data.ToString());
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.Parameters.Clear();

                    var cmd1 = new SqlCommand(
                        @"INSERT INTO LedgerTag(ShortDescription, longDescription) 
                    VALUES(@ShortDescription, @longDescription); SELECT SCOPE_IDENTITY()", connection);

                    cmd1.Parameters.AddWithValue("@ShortDescription", inputList[i].shortDescription);
                    cmd1.Parameters.AddWithValue("@longDescription", inputList[i].longDescription);

                    inputList[i].ledgerTagId = int.Parse(cmd1.ExecuteScalar().ToString());
                    cmd1.Parameters.Clear();
                }
            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static List<KeyValuePair<int, int>> ReadRel_AccountPlan(Budget inputBudget)
        {
            var inputList = new List<KeyValuePair<int, int>>();
            inputList.Clear();

            var dt = new DataTable();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command =
                new SqlCommand(
                    "SELECT FK_LedgerAccountID, FK_LedgerTagID from rel_accountplan where FK_budgetid = @BudgetID",
                    connection);
            command.Parameters.AddWithValue("@BudgetID", inputBudget.budgetID);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                var tempAccount = int.Parse(row["FK_LedgerAccountID"].ToString());
                var tempTag = int.Parse(row["FK_LedgerTagID"].ToString());
                inputList.Add(new KeyValuePair<int, int>(tempAccount, tempTag));
            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static List<string> DupeCheckListTag()
        {
            var dt = new DataTable();
            var dupeCheckList = new List<string>();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand("SELECT LongDescription From LedgerTag WHERE LongDescription IS NOT Null",
                connection);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
                dupeCheckList.Add(row[2].ToString());

            connection = DisconnectFromDB(connection);

            return dupeCheckList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static List<string> DupeCheckListAccount()
        {
            var dt = new DataTable();
            var dupeCheckList = new List<string>();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command =
                new SqlCommand("SELECT LedgerAccountXena From LedgerAccount WHERE LedgerAccountXena IS NOT Null",
                    connection);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
                dupeCheckList.Add(row[0].ToString());

            connection = DisconnectFromDB(connection);

            return dupeCheckList;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static void WriteBudgetValues(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (var i = 0; i < inputData.groupList.groupList.Count; i++)
            for (var j = 0; j < inputData.groupList.groupList[i].accountList.accountList.Count; j++)
            {
                var command = new SqlCommand(
                    @"INSERT INTO ValueInterval (January,February,March,April, May, June,July,August,September,October,November,December) 
                                    VALUES(@January, @February, @March, @April, @May, @June , @July , @August, @September, @October, @November, @December) SELECT SCOPE_IDENTITY();",
                    connection);
                command.Parameters.AddWithValue("@January",
                    inputData.groupList.groupList[i].accountList.accountList[j].January);
                command.Parameters.AddWithValue("@February",
                    inputData.groupList.groupList[i].accountList.accountList[j].February);
                command.Parameters.AddWithValue("@March",
                    inputData.groupList.groupList[i].accountList.accountList[j].March);
                command.Parameters.AddWithValue("@April",
                    inputData.groupList.groupList[i].accountList.accountList[j].April);
                command.Parameters.AddWithValue("@May",
                    inputData.groupList.groupList[i].accountList.accountList[j].May);
                command.Parameters.AddWithValue("@June",
                    inputData.groupList.groupList[i].accountList.accountList[j].June);
                command.Parameters.AddWithValue("@July",
                    inputData.groupList.groupList[i].accountList.accountList[j].July);
                command.Parameters.AddWithValue("@August",
                    inputData.groupList.groupList[i].accountList.accountList[j].August);
                command.Parameters.AddWithValue("@September",
                    inputData.groupList.groupList[i].accountList.accountList[j].September);
                command.Parameters.AddWithValue("@October",
                    inputData.groupList.groupList[i].accountList.accountList[j].October);
                command.Parameters.AddWithValue("@November",
                    inputData.groupList.groupList[i].accountList.accountList[j].November);
                command.Parameters.AddWithValue("@December",
                    inputData.groupList.groupList[i].accountList.accountList[j].December);
                var tempData = command.ExecuteScalar();

                var accountID = inputData.groupList.groupList[i].accountList.accountList[j].accountID;
                var BudgetID = inputData.budgetID;
                var ValueID = int.Parse(tempData.ToString());
                WriteValueIntervalToAccountPlan(ValueID, BudgetID, accountID);
            }
            connection = DisconnectFromDB(connection);
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        /// </summary>
        public static void WriteValueIntervalToAccountPlan(int ValueID, int budgetID, int accountID)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand(
                string.Format(
                    @"UPDATE Rel_AccountPlan SET FK_ValueIntervalID = ({0}) WHERE FK_BudgetID = {1} AND FK_LedgerTagID = {2}",
                    ValueID, budgetID, accountID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        /// <summary>
        ///     Written by Jonas
        ///     Get Accounts from the DB, and process the onces selected in the DatePicker (With full months)
        /// </summary>
        public static List<Account> GetAccounts(LedgerGroupData ledgerGroupData, int budgetID, int fromMonth,
            int toMonth)
        {
            var accountList = new List<Account>();

            var dt = new DataTable();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command =
                new SqlCommand(
                    "SELECT * FROM view_BudgetAccount WHERE FK_BudgetID = @BudgetID AND AccountName = @AccountName ORDER BY ValueIntervalID",
                    connection);
            command.Parameters.AddWithValue("@BudgetID", budgetID);
            command.Parameters.AddWithValue("@AccountName", ledgerGroupData.TranslatedGroup);

            dt.Load(command.ExecuteReader());

            var months = new string[12];

            for (var i = fromMonth; i <= toMonth; i++)
                months[i - 1] = FindMonth(i);

            foreach (DataRow row in dt.Rows)
                accountList.Add(new Account(row, months));

            connection = DisconnectFromDB(connection);

            return accountList;
        }

        /// <summary>
        ///     Written by Jonas
        /// </summary>
        private static string FindMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }

            return null;
        }

        public static Budget GetBudget(int BudgetID)
        {
            var tempBudget = new Budget();
            tempBudget.budgetID = BudgetID;


            var _groupList = new List<AccountGroup>();

            var dt = new DataTable();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command =
                new SqlCommand("SELECT * FROM view_readbudget WHERE budgetID = @BudgetID  ORDER BY LedgerAccountID ",
                    connection);
            command.Parameters.AddWithValue("@BudgetID", tempBudget.budgetID);

            dt.Load(command.ExecuteReader());

            //!int nygruppeID = ny gruppeID row
            //    hvis gruppeID row != ny gruppeID row
            //        ny gruppeID row = gruppeID row
            ;


            var groupCount = -1;
            var currentGroupID = -1;
            foreach (DataRow row in dt.Rows)
            {
                if (currentGroupID != int.Parse(row["LedgerAccountID"].ToString())
                ) //hvis der refereres til en ny gruppe
                {
                    currentGroupID = int.Parse(row["LedgerAccountID"].ToString());
                    if (currentGroupID == 57)
                    {
                        var bla = 1;
                    }
                    groupCount++;

                    _groupList.Add(new AccountGroup(row)); //tilføjer en ny gruppe til budgetet med id og navn
                }
                _groupList[groupCount].accountList.accountList.Add(new Account(row));
            }

            connection = DisconnectFromDB(connection);
            //_groupList[groupCount].accountList.accountList[acc] = _accountList;
            tempBudget.groupList.groupList = _groupList;
            return tempBudget;
        }

        /// <summary>
        ///     Written by Thomas and Mikael
        ///     Inserts a new entry to the ValueInterval table in DB with the postings of a given account
        /// </summary>
        public static void UpdateValueInterval(Account inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            var command = new SqlCommand(
                string.Format(@"UPDATE ValueInterval SET January = {0}, February = {1}, March = {2},
                                April = {3}, May ={4}, June = {5}, July = {6}, August = {7},
                                September= {8}, October = {9}, November = {10}, December = {11} WHERE ValueIntervalID = {12};",
                    inputData.January, inputData.February, inputData.March,
                    inputData.April, inputData.May, inputData.June, inputData.July,
                    inputData.August, inputData.September, inputData.October,
                    inputData.November, inputData.December, inputData.ValueIntervalID),
                connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }
    }
}