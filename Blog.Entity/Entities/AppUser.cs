using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid ImageId { get; set; } = Guid.Parse("9EA0DE57-D220-4A26-810C-A3D11C3BC4A8");
        public Image Image { get; set; } //bağlantı kurmak için
        public ICollection<Article> Articles { get; set;} //bağlantı kurmak için

    }
}
