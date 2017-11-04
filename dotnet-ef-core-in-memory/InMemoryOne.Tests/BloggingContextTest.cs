using System;
using System.Linq;
using InMemoryOne.Models;
using InMemoryOne.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace InMemoryOne.Tests
{
    [TestClass]
    public class BloggingContextTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty("databaseName", "blogDatabase")]
        [TestProperty("blogLocation", "http://sample.com")]
        public void ShouldAddBlog()
        {
            var databaseName = this.TestContext.Properties["databaseName"].ToString();
            var blogLocation = this.TestContext.Properties["blogLocation"].ToString();

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName : databaseName)
                .Options;

            using(var context = new BloggingContext(options))
            {
                var blog = new Blog { Url = blogLocation };
                context.Blogs.Add(blog);
                context.SaveChanges();
            }

            using(var context = new BloggingContext(options))
            {
                var blog = context.Blogs.SingleOrDefault(i => i.Url == blogLocation);
                Assert.IsNotNull(blog);
            }
        }

        [TestMethod]
        [TestProperty("databaseName", "blogDatabase")]
        [TestProperty("expectedMessage", "Relational-specific methods can only be used when the context is using a relational database provider.")]
        public void ShouldNotGetDatabaseConnection()
        {
            var databaseName = this.TestContext.Properties["databaseName"].ToString();
            var expectedMessage = this.TestContext.Properties["expectedMessage"].ToString();

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName : databaseName)
                .Options;

            using(var context = new BloggingContext(options))
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