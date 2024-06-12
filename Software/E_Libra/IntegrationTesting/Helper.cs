using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IntegrationTesting
{
    public static class Helper
    {
        private static SqlConnection _connection;

        private static void ExecuteSqlCommand(string sqlCommand)
        {
            using (var command = new SqlCommand(sqlCommand, _connection))
            {
                command.ExecuteNonQuery();
            }
        }
        
        private static void Connect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseModelConfig"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        private static void Disconnect()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        public static void ResetDatabase()
        {
            Connect();

            DeleteFromAllTables();

            Disconnect();
        }

        public static void DeleteFromAllTables()
        {
            string sqlCommand =
                "DELETE FROM [dbo].[WishList]; " +
                "DELETE FROM [dbo].[Reservation]; " +
                "DELETE FROM [dbo].[Review]; " +
                "DELETE FROM [dbo].[Archive]; " +
                "DELETE FROM [dbo].[Borrow]; " +
                "DELETE FROM [dbo].[NotificationRead]; " +
                "DELETE FROM [dbo].[Notification]; " +
                "DELETE FROM [dbo].[Member]; " +
                "DELETE FROM [dbo].[Book_Author]; " +
                "DELETE FROM [dbo].[Author]; " +
                "DELETE FROM [dbo].[Book]; " +
                "DELETE FROM [dbo].[Genre]; " +
                "DELETE FROM [dbo].[Employee]; " +
                "DELETE FROM [dbo].[Library]; " +
                "DELETE FROM [dbo].[Administrator];";

            ExecuteSqlCommand(sqlCommand);
            
        }
    }
}
