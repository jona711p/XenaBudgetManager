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
        ///<summary>
        ///Written by Thomas
        ///Establishes a connection to the DB 
        ///</summary>
        public static SqlConnection ConnectToDB(SqlConnection connection)
        {

            if (connection == null)
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["XenaBudgetManager"].ConnectionString);
            connection.Open();


            return connection;
        }

        /// <summary>
        /// Written by Thomas
        /// Closes connection to the DB
        /// </summary>
        public static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            catch (Exception)
            {
                //MessageBox.Show("Der Kunne ikke Oprettes Forbindelse til Databaseserveren!");
            }

            return connection;
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry to the ValueInterval table in DB with the postings of a given account
        /// </summary>
        public static void WriteValueInterval(Account inputData)//BRUGEE´S IKKE?
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                        String.Format(@"INSERT INTO ValueInterval(January, February, March, April, May, June, July, August, September, October, November, December, [total]) 
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
        /// Written by Thomas
        /// Inserts a new entry in  DB 'LedgerAccount' with related data 
        /// </summary>
        public static List<LedgerAccounts> WriteNewLedgerAccount(List<LedgerAccounts> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (int i = 0; i < inputList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
                    String.Format(@"INSERT INTO LedgerAccount(LedgerAccountXena, AccountName) 
                    VALUES(@LedgerAccountXena, @AccountName) SELECT SCOPE_IDENTITY()"), connection);

                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                cmd.Parameters.AddWithValue("@AccountName", inputList[i].accountName);

                inputList[i].ledgerAccountId = Int32.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();

            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'LedgerTag' with related data 
        /// </summary>
        public static List<LedgerTags> WriteNewLedgerTag(List<LedgerTags> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);


            for (int i = 1; i < inputList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
               String.Format(@"INSERT INTO LedgerTag(ShortDescription, LongDescription) 
               VALUES (@ShortDescription, @LongDescription) SELECT SCOPE_IDENTITY()"), connection);

                cmd.Parameters.AddWithValue("@ShortDescription", inputList[i].shortDescription);
                cmd.Parameters.AddWithValue("@LongDescription", inputList[i].longDescription);

                inputList[i].ledgerTagId = Int32.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();

            }
            connection = DisconnectFromDB(connection);
            return inputList;
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'Rel_AccountPlan' with related data 
        /// </summary>
        public static void WriteNewRel_AccountPlan(List<LedgerTags> inputTagList, List<LedgerAccounts> inputAccountList, int budgetID) //rettet efter XenaDataModel
        {


            for (int i = 0; i < inputTagList.Count; i++)
            {
                for (int j = 0; j < inputAccountList.Count; j++)
                {
                    if (inputTagList[i].ledgerAccountXena == inputAccountList[j].ledgerAccountXena)
                    {
                        try
                        {
                            SqlConnection connection = null;
                            connection = ConnectToDB(connection);

                            SqlCommand command = new SqlCommand(
                                String.Format(@"INSERT INTO Rel_AccountPlan(FK_BudgetID, FK_LedgerAccountID, FK_LedgerTagID) VALUES ({0},{1},{2});", budgetID, inputAccountList[j].ledgerAccountId, inputTagList[i].ledgerTagId), connection);

                            command.ExecuteNonQuery();
                            connection = DisconnectFromDB(connection);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error at " + i + ". " + ex.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'XenaFiscal' with related data 
        /// </summary>
        public static void WriteNewFiscal(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                String.Format(@"INSERT INTO XenaFiscal(XenaFiscal) 
                        VALUES ({0});", inputData.XenaFiscalID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        } //ikke i brug

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'Budget' with related data 
        /// </summary>
        public static int WriteNewBudget(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                String.Format(@"INSERT INTO Budget(BudgetName, Year, XenaFiscalID) VALUES ('{0}', {1}, {2}) SELECT SCOPE_IDENTITY();", inputData.budgetName, inputData.budgetYear, inputData.XenaFiscalID), connection);

            var tempData = command.ExecuteScalar();

            connection = DisconnectFromDB(connection);

            return Int32.Parse(tempData.ToString());
        }

        // Claus. Gets List of Budgets. Is used to populate selectedList i view where you pick a budget to compare.
        public static List<Budget> GetBudgetId()
        {
            List<Budget> budgetList = new List<Budget>();
            DataTable dt = new DataTable();

            SqlConnection conn = null;
            conn = ConnectToDB(conn);
            SqlCommand cmd = new SqlCommand(
            string.Format(@"select * FROM Budget"), conn);

            dt.Load(cmd.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                Budget budget = new Budget();
                budget.budgetID = int.Parse(row["BudgetID"].ToString());
                budget.budgetName = row["BudgetName"].ToString();
                budgetList.Add(budget);
            }

            conn = DisconnectFromDB(conn);
            return budgetList;
        }
        //få fat i gruppeidliste 
        //select * from LedgerAccount
        //tag templisten og bind xenaid til accountid
        //returner opdateret liste med grupper
        public static List<LedgerAccounts> GetAccountIDs(List<LedgerAccounts> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);
              
            for (int i = 0; i < inputList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
                    string.Format(@"select LedgerAccountID from LedgerAccount WHERE LedgerAccountXena =  @LedgerAccountXena"), connection);
                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                var data = cmd.ExecuteScalar();
                if (data != null)
                {
                    inputList[i].ledgerAccountId = Int32.Parse(data.ToString());
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.Parameters.Clear();

                    SqlCommand cmd1 = new SqlCommand(
                        String.Format(@"INSERT INTO LedgerAccount(LedgerAccountXena, AccountName) 
                    VALUES(@LedgerAccountXena, @AccountName); SELECT SCOPE_IDENTITY()"), connection);

                    cmd1.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                    cmd1.Parameters.AddWithValue("@AccountName", inputList[i].accountName);

                    inputList[i].ledgerAccountId = Int32.Parse(cmd1.ExecuteScalar().ToString());
                    cmd1.Parameters.Clear();

                }

            }
            connection = DisconnectFromDB(connection);


            return inputList;

        }

        public static List<LedgerTags> GetTagIDs(List<LedgerTags> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (int i = 0; i < inputList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
                    String.Format(@"select LedgerTagID from LedgerTag WHERE longDescription =  @longDescription"), connection);
                cmd.Parameters.AddWithValue("@longDescription", inputList[i].longDescription);

                var data = cmd.ExecuteScalar();
                if (data != null)
                {
                    inputList[i].ledgerTagId = Int32.Parse(data.ToString());
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.Parameters.Clear();

                    SqlCommand cmd1 = new SqlCommand(
                        String.Format(@"INSERT INTO LedgerTag(ShortDescription, longDescription) 
                    VALUES(@ShortDescription, @longDescription); SELECT SCOPE_IDENTITY()"), connection);

                    cmd1.Parameters.AddWithValue("@ShortDescription", inputList[i].shortDescription);
                    cmd1.Parameters.AddWithValue("@longDescription", inputList[i].longDescription);

                    inputList[i].ledgerTagId = Int32.Parse(cmd1.ExecuteScalar().ToString());
                    cmd1.Parameters.Clear();

                }

            }

            connection = DisconnectFromDB(connection);

            return inputList;

        }

        public static List<KeyValuePair<int, int>> ReadRel_AccountPlan(Budget inputBudget)
        {
            List<KeyValuePair<int, int>> inputList = new List<KeyValuePair<int, int>>();
            inputList.Clear();

            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT FK_LedgerAccountID, FK_LedgerTagID from rel_accountplan where FK_budgetid = @BudgetID", connection);
            command.Parameters.AddWithValue("@BudgetID", inputBudget.budgetID);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                int tempAccount = int.Parse(row["FK_LedgerAccountID"].ToString());
                int tempTag = int.Parse(row["FK_LedgerTagID"].ToString());
                inputList.Add(new KeyValuePair<int, int>(tempAccount, tempTag));
            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }


        public static List<string> DupeCheckListTag()
        {
            DataTable dt = new DataTable();
            List<string> dupeCheckList = new List<string>();

            SqlConnection connection = null;
            connection = DB.ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT LongDescription From LedgerTag WHERE LongDescription IS NOT Null", connection);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                dupeCheckList.Add(row[2].ToString());
            }

            connection = DB.DisconnectFromDB(connection);

            return dupeCheckList;
        }

        public static List<string> DupeCheckListAccount()
        {
            DataTable dt = new DataTable();
            List<string> dupeCheckList = new List<string>();

            SqlConnection connection = null;
            connection = DB.ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT LedgerAccountXena From LedgerAccount WHERE LedgerAccountXena IS NOT Null", connection);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                dupeCheckList.Add(row[0].ToString());
            }

            connection = DB.DisconnectFromDB(connection);

            return dupeCheckList;
        }
        public static void WriteBudgetValues(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            for (int i = 0; i < inputData.groupList.groupList.Count; i++)
            {
                for (int j = 0; j < inputData.groupList.groupList[i].accountList.accountList.Count; j++)
                {
                    SqlCommand command = new SqlCommand(
                    String.Format(@"INSERT INTO ValueInterval (January,February,March,April, May, June,July,August,September,October,November,December) 
                                    VALUES(@January, @February, @March, @April, @May, @June , @July , @August, @September, @October, @November, @December) SELECT SCOPE_IDENTITY();"), connection);
                    command.Parameters.AddWithValue("@January", inputData.groupList.groupList[i].accountList.accountList[j].January);
                    command.Parameters.AddWithValue("@February", inputData.groupList.groupList[i].accountList.accountList[j].February);
                    command.Parameters.AddWithValue("@March", inputData.groupList.groupList[i].accountList.accountList[j].March);
                    command.Parameters.AddWithValue("@April", inputData.groupList.groupList[i].accountList.accountList[j].April);
                    command.Parameters.AddWithValue("@May", inputData.groupList.groupList[i].accountList.accountList[j].May);
                    command.Parameters.AddWithValue("@June", inputData.groupList.groupList[i].accountList.accountList[j].June);
                    command.Parameters.AddWithValue("@July", inputData.groupList.groupList[i].accountList.accountList[j].July);
                    command.Parameters.AddWithValue("@August", inputData.groupList.groupList[i].accountList.accountList[j].August);
                    command.Parameters.AddWithValue("@September", inputData.groupList.groupList[i].accountList.accountList[j].September);
                    command.Parameters.AddWithValue("@October", inputData.groupList.groupList[i].accountList.accountList[j].October);
                    command.Parameters.AddWithValue("@November", inputData.groupList.groupList[i].accountList.accountList[j].November);
                    command.Parameters.AddWithValue("@December", inputData.groupList.groupList[i].accountList.accountList[j].December);
                    var tempData = command.ExecuteScalar();

                    int accountID = inputData.groupList.groupList[i].accountList.accountList[j].accountID;
                    int BudgetID = inputData.budgetID;
                    int ValueID = Int32.Parse(tempData.ToString());
                    DB.WriteValueIntervalToAccountPlan(ValueID, BudgetID, accountID);

                }

            }
            connection = DisconnectFromDB(connection);
            return;
        }
        public static void WriteValueIntervalToAccountPlan(int ValueID, int budgetID, int accountID)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                String.Format(@"UPDATE Rel_AccountPlan SET FK_ValueIntervalID = ({0}) WHERE FK_BudgetID = {1} AND FK_LedgerTagID = {2}", ValueID, budgetID, accountID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        public static List<Budget> GetFullBudgetList(int budgetID, int fromMonth, int toMonth)
        {
            List<Budget> budgetList = new List<Budget>();
            DataTable dt = new DataTable();

            string[] months = new string[2];

            for (int i = 0; i < months.Length; i++)
            {
                if (i == 0)
                {
                    months[i] = FindMonth(fromMonth);
                }

                if (i == 1)
                {
                    months[i] = FindMonth(toMonth);
                }
            }

            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT * FROM view_GetFullBudget WHERE BudgetID = @BudgetID", connection);
            command.Parameters.AddWithValue("@BudgetID", budgetID);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                budgetList.Add(new Budget(row));
            }

            connection = DisconnectFromDB(connection);

            return budgetList;
        }

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
    }
}