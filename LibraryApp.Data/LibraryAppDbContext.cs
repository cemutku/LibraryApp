using LibraryApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryApp.Data
{
    public class LibraryAppDbContext : DbContext
    {
        public LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Author>().ToTable("Authors");
            modelBuilder.Entity<Book>().ToTable("Books");

            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("67cf18b2-4ce2-489b-99c4-a24ab3eb6d16"),
                    Name = "Sample Author",
                    Email = "sampleAuthor@gmail.com",
                    Address = "Sample Address"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = Guid.Parse("7e121dc2-6a56-4324-9f02-d5570f210824"),
                    Name = "Sample Author",
                    Publisher = "Sample Publisher",
                    AuthorId = Guid.Parse("67cf18b2-4ce2-489b-99c4-a24ab3eb6d16")
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
