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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder) 
        {
            /*  builder.Property(x => x.Title).HasMaxLength(150);*/ //property title uzunluğu max 
            builder.HasData(new Article //data seedlemiş olduk
            {
                Id = Guid.NewGuid(),
                Title = "Asp.Net Core Makale Denemesi",
                Content = "ASP.NET Core is a free, open-source, cross-platform framework used for building web applications. It was first released by Microsoft in 2016 and has since gained popularity among developers due to its flexibility, scalability, and performance.\r\n\r\nOne of the key benefits of using ASP.NET Core is its ability to run on multiple platforms, including Windows, macOS, and Linux. This makes it easier for developers to build and deploy web applications on their platform of choice, without worrying about compatibility issues.\r\n\r\nAnother advantage of ASP.NET Core is its modular architecture, which allows developers to include only the components they need in their applications. This results in smaller and faster applications, with improved performance and reduced memory footprint.",
                ViewCount = 15,
                IsDeleted = false,
                CategoryId= Guid.Parse("FCBD8E88-4987-45F1-BB25-C299C45C2F48"),
                ImageId= Guid.Parse("9EA0DE57-D220-4A26-810C-A3D11C3BC4A8"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                UserId= Guid.Parse("A0672378-C7F9-4C88-96A2-B33796483CCB") //ekleme kısmı
            },
            new Article
            {
                Id = Guid.NewGuid(),
                Title = "Visual Stuido Deneme Makale Denemesi",
                Content = "Visual Studio is a popular Integrated Development Environment (IDE) used by many software developers to create various applications. It was first released by Microsoft in 1997 and has since undergone numerous updates and improvements. In this article, we will explore some of the key features and benefits of Visual Studio.\r\n\r\nOne of the primary advantages of using Visual Studio is its wide range of tools and capabilities. It supports multiple programming languages, including C++, C#, and Visual Basic, and includes features such as code highlighting, debugging tools, and code completion. Visual Studio also has a user-friendly interface that is easy to navigate, allowing developers to quickly find and access the tools they need.",
                ViewCount = 15,
                IsDeleted = false,
                CategoryId=Guid.Parse("83B17459-554C-4DD3-841E-325D81938418"),
                ImageId=Guid.Parse("D0C31942-4C4E-434E-A6B5-C081084CACBB"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                UserId=Guid.Parse("656752FD-788E-4922-AD50-E7CD13A70BA5") //ekleme kısmı
            }

            );
            

        }
    }
}
