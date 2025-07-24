using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
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
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LName).HasMaxLength(50).IsRequired();
            
          
            
            builder.Property(x => x.Age).IsRequired();
           
        

            builder.Property(x => x.Gender).HasConversion<string>().IsRequired();
            builder.HasOne(p=>p.User).WithOne(u=>u.Patient)
                .HasForeignKey<Patient>(p=>p.UserId).OnDelete(DeleteBehavior.Cascade);

         builder.HasData(AppSeedData.GetPatients());
        }
    }
}
