using LibraryApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
            }

            modelBuilder.Entity<Author>().HasData(
                new 
                {
                    Id = Guid.Parse("67cf18b2-4ce2-489b-99c4-a24ab3eb6d16"),
                    Name = "Sample Author",
                    Email = "sampleAuthor@gmail.com",
                    Address = "Sample Address",
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new
                {
                    Id = Guid.Parse("7e121dc2-6a56-4324-9f02-d5570f210824"),
                    Name = "Sample Author",
                    Publisher = "Sample Publisher",
                    AuthorId = Guid.Parse("67cf18b2-4ce2-489b-99c4-a24ab3eb6d16"),
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property("LastModified").CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = timestamp;
                }
            }

            return base.SaveChanges();
        }
    }
}
