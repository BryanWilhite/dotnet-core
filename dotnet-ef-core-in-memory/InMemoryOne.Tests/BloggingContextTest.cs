using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using InMemoryOne.Models;
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
        [TestProperty("blogLocation", "http://sample.com")]
        public void ShouldAddBlog()
        {
            var connectionString = this.TestContext.Properties["connectionString"].ToString();
            var databaseName = this.TestContext.Properties["databaseName"].ToString();
            var blogLocation = this.TestContext.Properties["blogLocation"].ToString();

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            
            using(var context = new BloggingContext(connectionString, options))
            {
                var blog = new Blog { Url = blogLocation };
                context.Blogs.Add(blog);
                context.SaveChanges();
            }
        }

        [TestMethod]
        [TestProperty("connectionString", @"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;")]
        [TestProperty("databaseName", "blogDatabase")]
        [TestProperty("expectedMessage", "Relational-specific methods can only be used when the context is using a relational database provider.")]
        public void ShouldNotGetDatabaseConnection()
        {
            var connectionString = this.TestContext.Properties["connectionString"].ToString();
            var databaseName = this.TestContext.Properties["databaseName"].ToString();
            var expectedMessage = this.TestContext.Properties["expectedMessage"].ToString();

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            
            using(var context = new BloggingContext(connectionString, options))
            {
                var test = false;
                try
                {
                    var connection = context.Database.GetDbConnection();
                }
                catch (InvalidOperationException ex)
                {
                    test = true;
                    Assert.IsTrue(ex.Message.Contains(expectedMessage));
                }
                Assert.IsTrue(test, "The expected exception was not caught.");
            }
        }
    }
}