using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Config
{
    public class AvailabilityConfig : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            builder.ToTable("availabilities");
            builder.HasKey(x => x.Id);
          
            builder.Property(x => x.Day).HasConversion<string>().IsRequired();

            builder.Property(x => x.StartTime).IsRequired();

            builder.Property(x => x.EndTime).IsRequired();

            builder.HasOne(a => a.Doctor).WithMany(d => d.Availabilities)
              .HasForeignKey(a => a.DoctorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(AppSeedData.GetAvailabilities());


        }
    }
}
