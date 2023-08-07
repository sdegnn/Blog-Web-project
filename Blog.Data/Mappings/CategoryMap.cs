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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                
                    Id = Guid.Parse("FCBD8E88-4987-45F1-BB25-C299C45C2F48"),
                    Name = "Asp.Net Core",
                    CreatedBy = "Admin Test",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false

            },
            new Category
            {
                Id = Guid.Parse("83B17459-554C-4DD3-841E-325D81938418"),
                Name = "Visual Stuido 2023",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false

            }
            );
        }
    }
}
