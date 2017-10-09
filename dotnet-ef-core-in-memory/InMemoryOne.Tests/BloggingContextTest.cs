using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InMemoryOne.Tests
{
    [TestClass]
    public class BloggingContextTest
    {
        [TestMethod]
        [TestProperty("connectionString", @"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;")]
        public void ShouldConnectToDatabase()
        {
        }
    }
}
