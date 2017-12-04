using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
        public static void WriteNewLedgerAccount(LedgerAccounts inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                string.Format(@"INSERT INTO LedgerAccount(LedgerAccountID, AccountName) 
                        VALUES ({0},{1});", inputData.ledgerAccountId, inputData.ledgerAccountId), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        /// <summary>
        /// Written by Thomas
        /// Inserts a new entry in  DB 'LedgerTag' with related data 
        /// </summary>
        public static void WriteNewLedgerTag(LedgerTags inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                string.Format(@"INSERT INTO LedgerTag(LedgerTagID, ShortDescription, LongDescription) 
                        VALUES ({0},{1},{2});", inputData.ledgerTagId, inputData.shortDescription, inputData.longDescription), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
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
        public static void WriteNewBudget(Budget inputData)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                string.Format(@"INSERT INTO Budget(BudgetID, BudgetName, Year) 
                        VALUES ({0}, {1}, {2});", inputData.budgetID, inputData.budgetName, inputData.budgetYear), connection);

            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }
    }
}