using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
        }
        public void Dispose()
        {
        }
        public void ResetDatabase()
        {
            Helper.ResetDatabase();
        }
    }
}
