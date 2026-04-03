using Microsoft.AspNetCore.Identity;

namespace WorkOrderApp.Data
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Technician" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //add admin
            if (await userManager.FindByEmailAsync("admin@applevalley.gov") == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@applevalley.gov",
                    Email = "admin@applevalley.gov",
                    EmailConfirmed = true

                };

                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            //add technicians

            if (await userManager.FindByEmailAsync("tech1@applevalley.gov") == null)
            {
                var techUser1 = new IdentityUser
                {
                    UserName = "tech1@applevalley.gov",
                    Email = "tech1@applevalley.gov",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(techUser1, "Tech123!");
                await userManager.AddToRoleAsync(techUser1, "Technician");
            }

            if (await userManager.FindByEmailAsync("tech2@applevalley.gov") == null)
            {
                var techUser2 = new IdentityUser
                {
                    UserName = "tech2@applevalley.gov",
                    Email = "tech2@applevalley.gov",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(techUser2, "Tech123!");
                await userManager.AddToRoleAsync(techUser2, "Technician");
            }
        }
    }
}
