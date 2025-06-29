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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.Role).HasConversion<string>().IsRequired();

            builder.HasData(AppSeedData.GetUsers());


        }
    }
}
