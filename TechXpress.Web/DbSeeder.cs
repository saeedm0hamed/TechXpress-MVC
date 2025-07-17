using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TechXpress.Services;

namespace TechXpress.Web;

public class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service)
    {
        try
        {
            var context = service.GetService<ApplicationDbContext>();

            // this block will check if there are any pending migrations and apply them
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
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

            if (!context.Products.Any())
            {
                await SeedProductAsync(context);
            }

            if (!context.Stocks.Any())
            {
                await SeedStockAsync(context);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    private static async Task SeedCategoryAsync(ApplicationDbContext context)
    {
        var categorys = new[]
         {
            new Category { CategoryName = "Laptops" },
            new Category { CategoryName = "Phones" },
            new Category { CategoryName = "Monitors" },
            new Category { CategoryName = "Accessories" }
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

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        var products = new[]
        {
        new Product { ProductName = "MacBook Pro 16\"", Price = 2399.99, Image = "laptop.jpg", CategoryId = 1, },
        new Product { ProductName = "Dell XPS 15", Price = 1599.99, Image = "laptop.jpg", CategoryId = 1 },
        new Product { ProductName = "HP Spectre x360", Price = 1299.99, Image = "laptop.jpg", CategoryId = 1 },
        new Product { ProductName = "Lenovo ThinkPad X1", Price = 1799.99, Image = "laptop.jpg", CategoryId = 1 },
        new Product { ProductName = "Asus ROG Zephyrus", Price = 1999.99, Image = "laptop.jpg", CategoryId = 1 },
        new Product { ProductName = "Acer Swift 3", Price = 699.99, Image = "laptop.jpg", CategoryId = 1 },

        new Product { ProductName = "iPhone 15 Pro", Price = 999.99, Image = "phone.jpg", CategoryId = 2 },
        new Product { ProductName = "Samsung Galaxy S23", Price = 799.99, Image = "phone.jpg", CategoryId = 2 },
        new Product { ProductName = "Google Pixel 7", Price = 599.99, Image = "phone.jpg", CategoryId = 2 },
        new Product { ProductName = "OnePlus 11", Price = 699.99, Image = "phone.jpg", CategoryId = 2 },
        new Product { ProductName = "Xiaomi Redmi Note 12", Price = 299.99, Image = "phone.jpg", CategoryId = 2 },
        new Product { ProductName = "Motorola Edge 40", Price = 599.99, Image = "phone.jpg", CategoryId = 2 },

        new Product { ProductName = "Dell 27\" 4K Monitor", Price = 399.99, Image = "monitor.jpg", CategoryId = 3 },
        new Product { ProductName = "LG UltraFine 32\"", Price = 499.99, Image = "monitor.jpg", CategoryId = 3 },
        new Product { ProductName = "Samsung Odyssey G7", Price = 699.99, Image = "monitor.jpg", CategoryId = 3 },
        new Product { ProductName = "ASUS ProArt 27\"", Price = 599.99, Image = "monitor.jpg", CategoryId = 3 },
        new Product { ProductName = "Acer Predator XB3", Price = 899.99, Image = "monitor.jpg", CategoryId = 3 },

        new Product { ProductName = "HDMI Cable 6ft", Price = 9.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "USB-C to USB-A Cable", Price = 7.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Ethernet Cable 15ft", Price = 12.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Thunderbolt 4 Cable", Price = 29.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "DisplayPort Cable", Price = 14.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Wireless Mouse", Price = 29.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Mechanical Keyboard", Price = 89.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Laptop Stand", Price = 24.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Webcam 1080p", Price = 49.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Noise Cancelling Headphones", Price = 199.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "USB Flash Drive 128GB", Price = 19.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "External SSD 1TB", Price = 129.99, Image = "accessories.jpg", CategoryId = 4 },
        new Product { ProductName = "Surge Protector", Price = 24.99, Image = "accessories.jpg", CategoryId = 4 }
    };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
    private static async Task SeedStockAsync(ApplicationDbContext context)
    {
        var allProducts = await context.Products.ToListAsync();
        var stockEntries = allProducts.Select(p => new Stock
        {
            ProductId = p.Id,
            Quantity = 100
        }).ToList();

        await context.Stocks.AddRangeAsync(stockEntries);
        await context.SaveChangesAsync();
    }
}
