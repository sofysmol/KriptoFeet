using System;
using System.Linq;
using KriptoFeet.News.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using KriptoFeet.Categories.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KriptoFeet.DB
{
    public class DomainModelMySqlContext : IdentityDbContext<Account>
    {
        public DomainModelMySqlContext(DbContextOptions<DomainModelMySqlContext> options) :base(options)
        { }
        
        public DbSet<CommentDB> Comments { get; set; }
        
        public DbSet<CategoryDB> Categories { get; set; }
        
        public DbSet<NewsDB> News {get; set;}
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDB>().HasKey(m => m.Id);
            builder.Entity<NewsDB>().HasKey(m => m.Id);
            builder.Entity<CommentDB>().HasKey(m => m.Id);
            builder.Entity<CategoryDB>().HasKey(m => m.Id);
            builder.Entity<SignInData>().HasKey(m => m.Password);
 
            base.OnModelCreating(builder);
        }
 
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }
 
        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
 
            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}