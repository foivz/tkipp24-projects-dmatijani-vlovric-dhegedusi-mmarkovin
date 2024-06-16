using System;
using System.Data.SqlClient;
using System.Configuration;

namespace IntegrationTesting
{
    public static class Helper
    {
        private static readonly object _dbLock = new object();
        private static string connectionString = ConfigurationManager.ConnectionStrings["DatabaseModelConfig"].ConnectionString;

        public static void ExecuteSqlCommand(string sqlCommand)
        {
            lock (_dbLock)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sqlCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void ResetDatabase()
        {
            lock (_dbLock)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    DeleteFromAllTables(connection);
                }
            }
        }

        public static void ExecuteCustomSql(string sql)
        {
            lock (_dbLock)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void DeleteFromAllTables(SqlConnection connection)
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
                "DELETE FROM [dbo].[Library]; ";

            using (var command = new SqlCommand(sqlCommand, connection))
            {
                command.ExecuteNonQuery();
            }

            //DBCC CHECKIDENT ('[dbo].[Library]', RESEED, 0);" 
            //DBCC CHECKIDENT ('[dbo].[Author]', RESEED, 0);"
        }
    }
}
