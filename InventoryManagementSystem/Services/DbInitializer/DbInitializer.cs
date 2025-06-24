using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;

        public DbInitializer(UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _db=db;
        }
        public async Task Initialize()
        {
            //migartions if they aren't applied

            if ((await _db.Database.GetPendingMigrationsAsync()).Any())
                await _db.Database.MigrateAsync();

            //create admin role if it is not created
            if (!await _roleManager.RoleExistsAsync(StaticDetails.Admin_Role))
            {

                //if role isn't created , then we will create admin user as well
                await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin_Role));

                var admin = new ApplicationUser
                {
                    UserName = "Admin2530",
                    Email = "AdminUser2530@gmail.com",
                    PhoneNumber = "01555178340",
                    Address = "Shawa Mansoura Egypt",
                    Image = "m.png",
                    EmailConfirmed = true     //important when you seed data in db during creation for first time to make the user confirmed
                                              //without it the signInManager.PasswordSignInAsync returns is not allowed
                };

                var result = await _userManager.CreateAsync(admin, "Admin2530@");

                if (!result.Succeeded)
                {
                    //  ❌  log or inspect the real reason
                    foreach (var error in result.Errors)
                        Console.WriteLine($"{error.Code}: {error.Description}");
                    return;     // don’t keep going if user isn’t there
                }

                await _userManager.AddToRoleAsync(admin, StaticDetails.Admin_Role);
                return;
            }
        }

    }
}
