using EFCoreSqlLite.Model;
using EFCoreSqlLite.Model.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EFCoreSqlLite.Infrastructure
{
    public class BookContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publishing> Publishing { get; set; }
        public DbSet<AuthorBook> AuthorBook { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BookDB.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Book>()
            .HasMany(c => c.Authors)
            .WithMany(s => s.Books)
            .UsingEntity<AuthorBook>(
               j => j
                .HasOne(pt => pt.Author)
                .WithMany(t => t.AuthorBook)
                .HasForeignKey(pt => pt.AuthorId),
            j => j
                .HasOne(pt => pt.Book)
                .WithMany(p => p.AuthorBook)
                .HasForeignKey(pt => pt.BookId),
            j =>
            {
                j.HasKey(pt => new { pt.AuthorId, pt.BookId});
                j.ToTable("AuthorBook");
            });

        }
    }
}
