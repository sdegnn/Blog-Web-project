using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public abstract class EntityBase:IEntityBase
    {
     
        public EntityBase() //bu class newlenirken direkt çalışır.
        {
            Id= Guid.NewGuid();
            CreatedDate = DateTime.Now; //altta kullandığımız gibi veya bu şekilde consturctor methotla kullanabiliriz.default olarak
            IsDeleted = false;
            
        }
        public virtual Guid Id { get; set; } //= Guid.NewGuid();//yeni guid değeri oluşturur int olduğunda 1 1 artar. //override edebiliriz bu sayede//int göre daha güveli veri tipi guid
        //Makale kim tarafından yaratıldı,düzenlendi,silindi

        public virtual string CreatedBy { get; set; } = "Undefined";

        public virtual string? ModifiedBy { get; set; } //ilk oluşturduğumuzda düzenlenmiş olamayacağı veya biri düzenlemediği sürece aktif olmayacağı için ? koyarak nullaber olduklarını belli ederiz.

        public virtual string? DeletedBy { get; set; }

        public  virtual DateTime CreatedDate { get; set; }//= DateTime.Now; //deafult olarak now olur

        public virtual DateTime? ModifiedDate { get; set; }

        public virtual DateTime? DeletedDate { get; set; }
        public virtual bool IsDeleted { get; set; } //= false; //bu makale aktif mi  //default olarak false olur.



    }
}
