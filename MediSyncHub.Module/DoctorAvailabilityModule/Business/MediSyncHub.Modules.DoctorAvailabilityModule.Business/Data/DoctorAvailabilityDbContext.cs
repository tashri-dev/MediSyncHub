using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Data
{
    internal class DoctorAvailabilityDbContext(DbContextOptions<DoctorAvailabilityDbContext> options)
        : DbContext(options)
    {
        public DbSet<Slot> Slots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slot>(builder =>
            {
                builder.ToTable("Slots", "DoctorAvailability");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Time).IsRequired();
                builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
            });
        }
    }
}