using Microsoft.EntityFrameworkCore;
using InMemoryOne.Models;


namespace InMemoryOne.Repository;

public class BloggingContext(DbContextOptions<BloggingContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
}
