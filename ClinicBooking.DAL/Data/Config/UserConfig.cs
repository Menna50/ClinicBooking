using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Role).HasConversion<string>().IsRequired();
           
       


        }
    }
}
