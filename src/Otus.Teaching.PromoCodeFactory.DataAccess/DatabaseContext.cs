using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Клиенты
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Предпочтения
        /// </summary>
        public DbSet<Preference> Preferences { get; set; }

        /// <summary>
        /// Промокоды
        /// </summary>
        public DbSet<PromoCode> PromoCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(20);
                entity.Property(e => e.LastName).HasMaxLength(30);
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.HasOne(e => e.Role)
                      .WithMany();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PromoCode>(entity =>
            {
                entity.Property(pc => pc.Code).HasMaxLength(20);
                entity.Property(pc => pc.ServiceInfo).HasMaxLength(500);
                entity.Property(pc => pc.PartnerName).HasMaxLength(50);

                entity.HasOne(pc => pc.PartnerManager).WithMany().HasForeignKey(pc => pc.PartnerId);
                entity.HasOne(pc => pc.Preference).WithMany().HasForeignKey(pc => pc.PreferenceId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(20);
                entity.Property(e => e.LastName).HasMaxLength(30);
                entity.Property(e => e.Email).HasMaxLength(50);
            });

            modelBuilder.Entity<CustomerPreference>(entity =>
            {
                entity.HasKey(bc => new { bc.CustomerId, bc.PreferenceId });

                entity.HasOne(bc => bc.Customer)
                      .WithMany(b => b.Preferences)
                      .HasForeignKey(bc => bc.CustomerId);

                entity.HasOne(bc => bc.Preference)
                      .WithMany()
                      .HasForeignKey(bc => bc.PreferenceId);
            });
        }
    }
}
