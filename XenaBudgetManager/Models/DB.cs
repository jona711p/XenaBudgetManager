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
        public static SqlConnection ConnectToDB(SqlConnection connection)
        {
           
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["XenaBudgetManager"].ConnectionString);
                connection.Open();
           

            return connection;
        }

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
    }
}