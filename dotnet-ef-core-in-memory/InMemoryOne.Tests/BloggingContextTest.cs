using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using InMemoryOne.Repository;

namespace InMemoryOne.Tests
{
    [TestClass]
    public class BloggingContextTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty("connectionString", @"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;")]
        [TestProperty("databaseName", "blogDatabase")]
        public void ShouldConnectToDatabase()
        {
            var connectionString = this.TestContext.Properties["connectionString"].ToString();
            var databaseName = this.TestContext.Properties["databaseName"].ToString();

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
        }
    }
}