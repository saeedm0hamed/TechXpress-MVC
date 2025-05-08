using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TechXpress.Web;

public class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service)
    {
        try
        {
            var context = service.GetService<ApplicationDbContext>();

            // this block will check if there are any pending migrations and apply them
            if ((await context.Database.GetPendingMigrationsAsync()).Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            // create admin role if not exists
            var adminRoleExists = await roleMgr.RoleExistsAsync(Roles.Admin.ToString());

            if (!adminRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }

            // create user role if not exists
            var userRoleExists = await roleMgr.RoleExistsAsync(Roles.User.ToString());

            if (!userRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }

            // create admin user

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }


            if (!context.Categorys.Any())
            {
                await SeedCategoryAsync(context);
            }

            if (!context.orderStatuses.Any())
            {
                await SeedOrderStatusAsync(context);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #region private methods

    private static async Task SeedCategoryAsync(ApplicationDbContext context)
    {
        var categorys = new[]
         {
            new Category { CategoryName = "Laptops" },
            new Category { CategoryName = "Phones" },
            new Category { CategoryName = "Monitors" },
            new Category { CategoryName = "Cables" },
            new Category { CategoryName = "Accessories" },
            new Category { CategoryName = "General" }
        };

        await context.Categorys.AddRangeAsync(categorys);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrderStatusAsync(ApplicationDbContext context)
    {
        var orderStatuses = new[]
        {
            new OrderStatus { StatusId = 1, StatusName = "Pending" },
            new OrderStatus { StatusId = 2, StatusName = "Shipped" },
            new OrderStatus { StatusId = 3, StatusName = "Delivered" },
            new OrderStatus { StatusId = 4, StatusName = "Cancelled" },
            new OrderStatus { StatusId = 5, StatusName = "Returned" },
            new OrderStatus { StatusId = 6, StatusName = "Refund" }
        };

        await context.orderStatuses.AddRangeAsync(orderStatuses);
        await context.SaveChangesAsync();
    }

    #endregion
}
