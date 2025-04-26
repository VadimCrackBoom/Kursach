using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data;

public class AutoServiceContext: DbContext
{
    public AutoServiceContext(DbContextOptions<AutoServiceContext> options): base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<AutoService> AutoServices { get; set; }
    public DbSet<ServiceOffer> ServiceOffers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentHistory> AppointmentHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.ServiceOffer)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.ServiceOfferId);
        
        modelBuilder.Entity<AppointmentHistory>()
            .HasOne(ah => ah.Appointment)
            .WithOne(a => a.AppointmentHistory)
            .HasForeignKey<AppointmentHistory>(ah => ah.AppointmentId);
        
        modelBuilder.Entity<ServiceOffer>()
            .HasOne(s => s.AutoService)
            .WithMany(a => a.ServiceOffers)
            .HasForeignKey(s => s.AutoServiceId);
    }
}