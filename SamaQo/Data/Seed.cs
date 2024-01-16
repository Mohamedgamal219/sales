using Microsoft.AspNetCore.Identity;
using SamaQo.Constant;

namespace SamaQo.Data
{
    public class Seed
    {
        public static async Task SeedData(IServiceProvider provider)
        {
            var userMG = provider.GetService<UserManager<IdentityUser>>();
            var RoleMg = provider.GetService<RoleManager<IdentityRole>>();

            await RoleMg.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            await RoleMg.CreateAsync(new IdentityRole(Role.User.ToString()));


            var admin = new IdentityUser
            {
                UserName = "Admin123@outLook.com",
                Email = "Admin123@outLook.com",
                EmailConfirmed = true
            };

            var IsExsit = await userMG.FindByEmailAsync(admin.Email);

            if(IsExsit is null)
            {
                await userMG.CreateAsync(admin,"Admin123@");
                await userMG.AddToRoleAsync(admin,Role.Admin.ToString());
            }
        }
    }
}
