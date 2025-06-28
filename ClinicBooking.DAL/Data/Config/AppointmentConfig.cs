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
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AppointmentDate).IsRequired();

            builder.Property(x => x.Status).HasConversion<string>().IsRequired();
            builder.HasOne(a=>a.Doctor).WithMany(a=>a.Appointments)
                .HasForeignKey(a=>a.DoctorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(AppSeedData.GetAppointments());
        }
    }
}
