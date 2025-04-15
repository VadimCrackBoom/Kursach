using AutoServiceApi.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;

namespace AutoServiceApi.Data
{
    public class AutoServiceContext : DbContext
    {
        public AutoServiceContext(DbContextOptions<AutoServiceContext> options) 
            : base(options) { }

        public DbSet<ServiceOffer> ServicesOffers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AutoService> AutoServices { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        
        public IQueryable<ServiceOffer> ServiceOffer => ServiceOffer.AsQueryable();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Начальные данные
            modelBuilder.Entity<AutoService>().HasData(
                new AutoService { Id = 1, Name = "Автосервис", Address = "ул. Примерная, 1", Phone = "+79990001122" }
            );

            modelBuilder.Entity<ServiceOffer>().HasData(
                new ServiceOffer() { Id = 1, Name = "Замена масла", Price = 2000 },
                new ServiceOffer() { Id = 2, Name = "Диагностика", Price = 1500 }
            );
        }
    }
}
