using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.HasData(new AppRole
            {
                Id=Guid.Parse("66DF9FCD-641E-4264-BF83-4E78DCEFA345"),
                Name="Superadmin",
                NormalizedName="SUPERADMIN",
                ConcurrencyStamp=Guid.NewGuid().ToString(), //aynı anda rol üzerinde işlem yapılırsa eğer bu işlem sonrası oluşturuluyor.Kim daha önce yaptıysa bunu değiştirir sonraki kişi bastığında bu değer değiştiği için bu işlemi gerçekleştirmiyor.


            },
            new AppRole
            {
                Id = Guid.Parse("8A5D6A0D-7F62-49A6-AAA1-DF5CE8D60A8A"),
                Name="Admın",
                NormalizedName="ADMIN",
                ConcurrencyStamp= Guid.NewGuid().ToString(),

            },
            new AppRole
            {
                Id=Guid.Parse("F586B48B-FAEA-4FD7-9849-DD74101C2CDF"),
                Name="User",
                NormalizedName = "USER",
                ConcurrencyStamp=Guid.NewGuid().ToString(),
            });
        }
    }
}
