using System;
using System.Linq;
using InMemoryOne.Models;
using InMemoryOne.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InMemoryOne.Tests
{
    public class BloggingContextTest
    {
        [Theory]
        [InlineData("blogDatabase", "http://sample.com")]
        public void ShouldAddBlog(string databaseName, string blogLocation)
        {
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
                Assert.NotNull(blog);
            }
        }

        [Theory]
        [InlineData("blogDatabase", "Relational-specific methods can only be used when the context is using a relational database provider.")]
        public void ShouldNotGetDatabaseConnection(string databaseName, string expectedMessage)
        {
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
                    Assert.Contains(expectedMessage, ex.Message);
                }
                Assert.True(test, "The expected exception was not caught.");
            }
        }
    }
}