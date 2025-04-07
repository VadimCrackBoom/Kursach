using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;

namespace WebApplication1.Data
{
    public class AutoServiceContext : DbContext
    {
        public AutoServiceContext(DbContextOptions<AutoServiceContext> options) : base(options)
        {
        }

        public DbSet<Autoservice> Autoservices { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.History)
                .WithOne(h => h.Appointment)
                .HasForeignKey<AppointmentHistory>(h => h.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Добавим начальные данные
            modelBuilder.Entity<Autoservice>().HasData(
                new Autoservice { Id = 1, Name = "Автосервис Косованова", Address = "ул. Примерная, 123", Phone = "+7 (123) 456-78-90" }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Замена масла", Description = "Полная замена моторного масла и фильтра", Price = 2000 },
                new Service { Id = 2, Name = "Диагностика", Description = "Компьютерная диагностика автомобиля", Price = 1500 }
            );
        }
    }
}
