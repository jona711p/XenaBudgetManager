using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace XenaBudgetManager.Models
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
        public static void WriteValueInterval(Account inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                        string.Format(@"INSERT INTO ValueInterval(January, February, March, April, May, June, July, August, September, October, November, December, [total]) 
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
                    string.Format(@"INSERT INTO LedgerAccount(LedgerAccountXena, AccountName) 
                    VALUES(@LedgerAccountXena, @AccountName) SELECT SCOPE_IDENTITY()"), connection);

                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);
                cmd.Parameters.AddWithValue("@AccountName", inputList[i].accountName);

                inputList[i].ledgerAccountId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();

            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'LedgerTag' with related data 
        /// </summary>
        public static void WriteNewLedgerTag(List<LedgerTags> inputList) //rettet efter XenaDataModel
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            
            for (int i = 1; i < inputList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
               string.Format(@"INSERT INTO LedgerTag(LedgerTagID, ShortDescription, LongDescription) 
               VALUES (@LedgerTagID, @ShortDescription, @LongDescription)"), connection);

                cmd.Parameters.AddWithValue("@LedgerTagID", inputList[i].ledgerTagId).ToString();
                cmd.Parameters.AddWithValue("@ShortDescription", inputList[i].shortDescription);
                cmd.Parameters.AddWithValue("@LongDescription", inputList[i].longDescription);
                
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

            }
            connection = DisconnectFromDB(connection);
        }
      public static GetLedgerTag()
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            return;
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
                                string.Format(@"INSERT INTO Rel_AccountPlan(FK_BudgetID, FK_LedgerAccountID, FK_LedgerTagID) VALUES ({0},{1},{2});", budgetID, inputAccountList[j].ledgerAccountId, inputTagList[i].ledgerTagId), connection);

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
        /// Inserts a new entry in  DB 'XenaUser' with related data 
        /// </summary>
        public static void WriteNewUser(User inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                string.Format(@"INSERT INTO XenaUser(UserID) 
                        VALUES ({0});", inputData.userID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
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
                string.Format(@"INSERT INTO XenaFiscal(XenaFiscal) 
                        VALUES ({0});", inputData.XenaFiscalID), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'Budget' with related data 
        /// </summary>
        public static int WriteNewBudget(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                string.Format(@"INSERT INTO Budget(BudgetName, Year) VALUES ('{0}', {1}) SELECT SCOPE_IDENTITY();", inputData.budgetName, inputData.budgetYear), connection);

            var tempData = command.ExecuteScalar();

            connection = DisconnectFromDB(connection);

            return int.Parse(tempData.ToString());
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
                    string.Format(@"select LedgerAccountID from LedgerAccount WHERE LedgerAccountXena =  @LedgerAccountXena) SELECT SCOPE_IDENTITY()"), connection);
                cmd.Parameters.AddWithValue("@LedgerAccountXena", inputList[i].ledgerAccountXena);

                inputList[i].ledgerAccountId = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Parameters.Clear();

            }

            connection = DisconnectFromDB(connection);

            return inputList;
        }


    }
}