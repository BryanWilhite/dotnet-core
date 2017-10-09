using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using InMemoryOne.Models;


namespace InMemoryOne.Repository
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(string connectionString, DbContextOptions<BloggingContext> options) : base(options)
        {
            this._connectionString = connectionString;
        }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._connectionString);
            }
        }

        readonly string _connectionString;
    }
}