using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class Article:EntityBase //article oluşturmaya çalıştığımızda entitybase kullanılabilir.
    {
        public Article()
        {
            
        }
        public Article(string title,string content, string createdBy, Guid userId,Guid categoryId,Guid imageId)
        {
            
            Title = title;
            Content = content;
            UserId = userId;
            CategoryId = categoryId;
            ImageId = imageId;
            CreatedBy = createdBy;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid? ImageId { get; set; } //Null gelme ihtimali var ? koyduk

        public Image Image { get; set; }
        public Guid UserId { get; set; } //bağlama işlemi
        public AppUser User { get; set; } //bağlama işlemi


      


    }
}

