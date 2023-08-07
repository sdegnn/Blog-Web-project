using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.DTOs.Articles
{
    public class ArticleDto //sadece kullanacağımız yapıları oluşturacağız.
    {
        //dto sınıfında gerçekleştirdiğmiz prop ve işlemler bizim entitiy sınıfımızdan direkt olarak bir referans alyıor.Birebir eşleme yapıyor.
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }

        //public int ViewCount { get; set; }
        public Image Image { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set;}
    }
}
