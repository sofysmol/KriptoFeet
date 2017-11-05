using System;
using System.Linq;
using KriptoFeet.Models;
using KriptoFeet.News.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using KriptoFeet.Categories.Models;

namespace KriptoFeet.DB
{
    public class DomainModelMySqlContext : DbContext
    {
        public DomainModelMySqlContext(DbContextOptions<DomainModelMySqlContext> options) :base(options)
        { }
         
        public DbSet<DataEventRecord> DataEventRecords { get; set; }
 
        public DbSet<SourceInfo> SourceInfos { get; set; }

        public DbSet<User> Users {get; set;}
        
        public DbSet<CommentDB> Comments { get; set; }
        
        public DbSet<CategoryDB> Categories { get; set; }
        
        public DbSet<NewsDB> News {get; set;}
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);
            builder.Entity<User>().HasKey(m => m.Id);
            builder.Entity<NewsDB>().HasKey(m => m.Id);
 
            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
 
            base.OnModelCreating(builder);
        }
 
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
 
            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
 
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