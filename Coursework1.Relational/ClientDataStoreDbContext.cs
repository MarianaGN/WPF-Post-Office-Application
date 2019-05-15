using Coursework1.Core;
using Microsoft.EntityFrameworkCore;

namespace Coursework1.Relational
{
    public class ClientDataStoreDbContext : DbContext
    {
        #region DbSets

        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        #endregion

        #region Constructor

        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options)
        {
            
        }

        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Fluent API

            // Configure LoginCredentials
            // --------------------------
            //
            // Set Id as primary key
            modelBuilder.Entity<LoginCredentialsDataModel>().HasKey(a => a.Id);

            // TODO: Set up limits
            //modelBuilder.Entity<LoginCredentialsDataModel>().Property(a => a.FirstName).HasMaxLength(50);
        }

        #endregion
    }
}