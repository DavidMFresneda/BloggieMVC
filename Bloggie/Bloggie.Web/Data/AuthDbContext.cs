using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {

        private Guid _superAdminId = new Guid("accd64f2-2690-43c0-a96a-fedbf83e9232");
        private Guid _adminId = new Guid("90a023de-10ca-49bc-9596-e854d11a49ab");
        private Guid _userId = new Guid("a5a56bc5-b6cb-4373-a143-692a48c0c535");
        private Guid _superAdminRoleId = new Guid("b5b3b3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b");
        private Guid _adminRoleId = new Guid("a5a5a5a5-5a5a-5a5a-5a5a-5a5a5a5a5a5a");
        private Guid _userRoleId = new Guid("c5c5c5c5-5c5c-5c5c-5c5c-5c5c5c5c5c5c");

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed roles (User, admin, super admin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "User", NormalizedName = "User", Id = _userRoleId.ToString(), ConcurrencyStamp = _userRoleId.ToString() },
                new IdentityRole{Name = "Admin", NormalizedName = "Admin", Id = _adminRoleId.ToString(), ConcurrencyStamp = _adminRoleId.ToString() },
                new IdentityRole{Name = "SuperAdmin", NormalizedName = "SuperAdmin", Id = _superAdminRoleId.ToString(), ConcurrencyStamp = _superAdminRoleId.ToString() }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Crear superAdmin
            var superAdmin = new IdentityUser
            {
                Id = _superAdminId.ToString(),
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                NormalizedEmail = "superadmin@bloggie.com".ToUpper()
            };

            superAdmin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdmin, "SuperAdmin123!");

            builder.Entity<IdentityUser>().HasData(superAdmin);


            //Agregar roles a superAdmin

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>{RoleId = _superAdminRoleId.ToString(),
                                             UserId = _superAdminId.ToString()},
                new IdentityUserRole<string>{RoleId = _userRoleId.ToString(),
                                             UserId = _superAdminId.ToString()},
                new IdentityUserRole<string>{RoleId = _adminRoleId.ToString(),
                                             UserId = _superAdminId.ToString()}
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }

    }
}
