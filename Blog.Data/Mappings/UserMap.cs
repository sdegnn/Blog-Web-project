using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder) //bu şekilde değilde direkt builde ile alarak appdbcontextde onmodelconf metodunta tanımlayabiliriz.
        {
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var superadmın = new AppUser //builder hasdata mantığıyla direkt yapamayız şifreleme kullanıcaz password
            {
                Id = Guid.Parse("A0672378-C7F9-4C88-96A2-B33796483CCB"), //roller ve user arasında çoka çok bir ilişki var userrole diye bir sınıfım var örneğin bunalrı bu sınıflar birbirne bağlayıp ıdlerin birbirine referans vermesini sağlarız.
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "+900534908078",
                FirstName = "Sude",
                LastName = "Agun",
                PhoneNumberConfirmed = true, //doğrulama sağlanmış mı 
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(), //rolemapdekiyle aynı mantıkta
                ImageId=Guid.Parse("9EA0DE57-D220-4A26-810C-A3D11C3BC4A8") //ekleme


            };
            superadmın.PasswordHash = CreatePasswordHash(superadmın, "123456");//password eşleme
            var admın =new AppUser
            {
                Id = Guid.Parse("656752FD-788E-4922-AD50-E7CD13A70BA5"), //roller ve user arasında çoka çok bir ilişki var userrole diye bir sınıfım var örneğin bunalrı bu sınıflar birbirne bağlayıp ıdlerin birbirine referans vermesini sağlarız.
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PhoneNumber = "+900534908078",
                FirstName = "Ada",
                LastName = "Yalçın",
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId=Guid.Parse("D0C31942-4C4E-434E-A6B5-C081084CACBB") //ekleme


            };
            admın.PasswordHash = CreatePasswordHash(admın, "123456");

            builder.HasData(superadmın, admın);

        }
        private string CreatePasswordHash(AppUser user,string password) //bir kullanıcı eklediğimizde şifresiyle hashlenmiş olur ıdentity metotlarıyla gerçekleşir o yüzden direkt bir password yazıp kaydedemiyoruz
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
