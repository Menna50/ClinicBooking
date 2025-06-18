using ClinicBooking.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Config
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Specialty).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired(false);

            builder.HasOne(d=>d.User).WithOne(u=>u.Doctor)
                .HasForeignKey<Doctor>(d=>d.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired(); 
        }
    }
}
