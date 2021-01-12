using Microsoft.EntityFrameworkCore;
using InMemoryOne.Models;


namespace InMemoryOne.Repository
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}