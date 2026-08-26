using Microsoft.EntityFrameworkCore;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using InMemoryOne.Models;
using InMemoryOne.Repository;

namespace InMemoryOne.Tests;

public class BloggingContextTests(ITestOutputHelper helper)
{
    [Theory]
    [InlineData("blogDatabase", "http://sample.com")]
    public void ShouldAddBlog(string databaseName, string blogLocation)
    {
        DbContextOptions<BloggingContext> options = new DbContextOptionsBuilder<BloggingContext>()
            .UseInMemoryDatabase(databaseName : databaseName)
            .Options;

        helper.WriteLine($"Generating a new {nameof(Blog)} entry...");
        using(var context = new BloggingContext(options))
        {
            Blog blog = new() { Permalink = blogLocation };
            context.Blogs.Add(blog);

            Assert.Equal(1, context.SaveChanges());
            helper.WriteLine($"The following entry was added:\n    {blog}");
        }

        helper.WriteLine($"Searching for new {nameof(Blog)} entry...");
        using(var context = new BloggingContext(options))
        {
            Blog? blog = context.Blogs.SingleOrDefault(i => i.Permalink == blogLocation);

            Assert.NotNull(blog);
            helper.WriteLine($"The following entry was found:\n    {blog}");
        }
    }

    [Theory]
    [InlineData("blogDatabase")]
    public void ShouldNotGetDatabaseConnection(string databaseName)
    {
        ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddProvider(_loggerProvider));
        ILogger logger = factory.CreateLogger(nameof(ShouldNotGetDatabaseConnection));

        DbContextOptions<BloggingContext> options = new DbContextOptionsBuilder<BloggingContext>()
            .UseLoggerFactory(factory)
            .UseInMemoryDatabase(databaseName : databaseName)
            .Options;

        using BloggingContext context = new BloggingContext(options);

        InvalidOperationException actual = Assert.Throws<InvalidOperationException>(() => context.Database.GetDbConnection());
        logger.LogError("message: `{Message}`", actual.Message);
    }

    private readonly XUnitLoggerProvider _loggerProvider = new(helper);
}
