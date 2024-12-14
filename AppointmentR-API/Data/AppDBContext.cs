using AppointmentR_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentR_API.Data
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        //Add DBSets
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<EmployeeAvailability> EmployeeAvailability { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure model relationships
            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId);

            modelBuilder.Entity<AppointmentModel>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<EmployeeAvailability>()
                .HasOne(ea => ea.Employee)
                .WithMany(e => e.Availability)
                .HasForeignKey(ea => ea.EmployeeId);
        }

    }
}
