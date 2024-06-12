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

        public static void ExecuteSqlCommand(string sqlCommand)
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

        public static void ExecuteCustomSql(string sql)
        {
            Connect();

            ExecuteSqlCommand(sql);

            Disconnect();
        }

        public static void DeleteFromAllTables()
        {
            string sqlCommand =
                "DELETE FROM [dbo].[WishList]; " +
                "DELETE FROM [dbo].[Reservation]; DBCC CHECKIDENT ('[dbo].[Reservation]', RESEED, 0);" +
                "DELETE FROM [dbo].[Review]; " +
                "DELETE FROM [dbo].[Archive]; " +
                "DELETE FROM [dbo].[Borrow]; DBCC CHECKIDENT ('[dbo].[Borrow]', RESEED, 0);" +
                "DELETE FROM [dbo].[NotificationRead]; " +
                "DELETE FROM [dbo].[Notification]; DBCC CHECKIDENT ('[dbo].[Notification]', RESEED, 0);" +
                "DELETE FROM [dbo].[Member]; DBCC CHECKIDENT ('[dbo].[Member]', RESEED, 0);" +
                "DELETE FROM [dbo].[Book_Author]; " +
                "DELETE FROM [dbo].[Author]; " +
                "DELETE FROM [dbo].[Book]; DBCC CHECKIDENT ('[dbo].[Book]', RESEED, 0);" +
                "DELETE FROM [dbo].[Genre]; DBCC CHECKIDENT ('[dbo].[Genre]', RESEED, 0);" +
                "DELETE FROM [dbo].[Employee]; DBCC CHECKIDENT ('[dbo].[Employee]', RESEED, 0);" +
                "DELETE FROM [dbo].[Library]; "+
                "DELETE FROM [dbo].[Administrator]; DBCC CHECKIDENT ('[dbo].[Administrator]', RESEED, 0);";

            ExecuteSqlCommand(sqlCommand);

            //DBCC CHECKIDENT ('[dbo].[Library]', RESEED, 0);" 
            //DBCC CHECKIDENT ('[dbo].[Author]', RESEED, 0);"
        }

    }
}
