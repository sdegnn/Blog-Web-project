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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");
            builder.HasData(new AppUserRole
            {
                UserId=Guid.Parse("A0672378-C7F9-4C88-96A2-B33796483CCB"), //usermapteki ıd 
                RoleId=Guid.Parse("66DF9FCD-641E-4264-BF83-4E78DCEFA345"), //rolemapten ıd

            },
            new AppUserRole
            {
                UserId=Guid.Parse("656752FD-788E-4922-AD50-E7CD13A70BA5"),
                RoleId=Guid.Parse("8A5D6A0D-7F62-49A6-AAA1-DF5CE8D60A8A")

            });
        }
    }
}
