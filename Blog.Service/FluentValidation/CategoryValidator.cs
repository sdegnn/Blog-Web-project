using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidation
{
    public class CategoryValidator: AbstractValidator<Category> //tüm abstract validator sınıfından türemiş nesneleri otomatik olarak tanımlamış olur o yüzden tekrar extensionda tanımlamayız.
    {
        public CategoryValidator()
        {
            RuleFor(c=>c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithName("Kategori Adı")
                ;
            
        }
    }
}
