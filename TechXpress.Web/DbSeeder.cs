using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TechXpress.Data;

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

            if (!context.Products.Any())
            {
                await SeedProductsAsync(context);
                // update stock table
                await context.Database.ExecuteSqlRawAsync(@"
                     INSERT INTO Stock(ProductId,Quantity) 
                     SELECT 
                     b.Id,
                     10 
                     FROM Product b
                     WHERE NOT EXISTS (
                     SELECT * FROM [Stock]
                     );
           ");
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
            new Category { CategoryName = "Romance" },
            new Category { CategoryName = "Action" },
            new Category { CategoryName = "Thriller" },
            new Category { CategoryName = "Crime" },
            new Category { CategoryName = "SelfHelp" },
            new Category { CategoryName = "Programming" }
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

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        var products = new List<Product>
        {
            // Romance Products (CategoryId = 1)
            new Product { ProductName = "Pride and Prejudice", AuthorName = "Jane Austen", Price = 12.99, CategoryId = 1 },
            new Product { ProductName = "The Noteproduct", AuthorName = "Nicholas Sparks", Price = 11.99, CategoryId = 1 },
            new Product { ProductName = "Outlander", AuthorName = "Diana Gabaldon", Price = 14.99, CategoryId = 1 },
            new Product { ProductName = "Me Before You", AuthorName = "Jojo Moyes", Price = 10.99, CategoryId = 1 },
            new Product { ProductName = "The Fault in Our Stars", AuthorName = "John Green", Price = 9.99, CategoryId = 1 },
            
            // Action Products (CategoryId = 2)
            new Product { ProductName = "The Bourne Identity", AuthorName = "Robert Ludlum", Price = 14.99, CategoryId = 2 },
            new Product { ProductName = "Die Hard", AuthorName = "Roderick Thorp", Price = 13.99, CategoryId = 2 },
            new Product { ProductName = "Jurassic Park", AuthorName = "Michael Crichton", Price = 15.99, CategoryId = 2 },
            new Product { ProductName = "The Da Vinci Code", AuthorName = "Dan Brown", Price = 12.99, CategoryId = 2 },
            new Product { ProductName = "The Hunger Games", AuthorName = "Suzanne Collins", Price = 11.99, CategoryId = 2 },
            
            // Thriller Products (CategoryId = 3)
            new Product { ProductName = "Gone Girl", AuthorName = "Gillian Flynn", Price = 11.99, CategoryId = 3 },
            new Product { ProductName = "The Girl with the Dragon Tattoo", AuthorName = "Stieg Larsson", Price = 10.99, CategoryId = 3 },
            new Product { ProductName = "The Silence of the Lambs", AuthorName = "Thomas Harris", Price = 12.99, CategoryId = 3 },
            new Product { ProductName = "Before I Go to Sleep", AuthorName = "S.J. Watson", Price = 9.99, CategoryId = 3 },
            new Product { ProductName = "The Girl on the Train", AuthorName = "Paula Hawkins", Price = 13.99, CategoryId = 3 },
            
            // Crime Products (CategoryId = 4)
            new Product { ProductName = "The Godfather", AuthorName = "Mario Puzo", Price = 13.99, CategoryId = 4 },
            new Product { ProductName = "The Girl with the Dragon Tattoo", AuthorName = "Stieg Larsson", Price = 12.99, CategoryId = 4 },
            new Product { ProductName = "The Cuckoo's Calling", AuthorName = "Robert Galbraith", Price = 14.99, CategoryId = 4 },
            new Product { ProductName = "In Cold Blood", AuthorName = "Truman Capote", Price = 11.99, CategoryId = 4 },
            new Product { ProductName = "The Silence of the Lambs", AuthorName = "Thomas Harris", Price = 15.99, CategoryId = 4 },
            
            // SelfHelp Products (CategoryId = 5)
            new Product { ProductName = "The 7 Habits of Highly Effective People", AuthorName = "Stephen R. Covey", Price = 9.99, CategoryId = 5 },
            new Product { ProductName = "How to Win Friends and Influence People", AuthorName = "Dale Carnegie", Price = 8.99, CategoryId = 5 },
            new Product { ProductName = "Atomic Habits", AuthorName = "James Clear", Price = 10.99, CategoryId = 5 },
            new Product { ProductName = "The Subtle Art of Not Giving a F*ck", AuthorName = "Mark Manson", Price = 7.99, CategoryId = 5 },
            new Product { ProductName = "You Are a Badass", AuthorName = "Jen Sincero", Price = 11.99, CategoryId = 5 },
            
            // Programming Products (CategoryId = 6)
            new Product { ProductName = "Clean Code", AuthorName = "Robert C. Martin", Price = 19.99, CategoryId = 6 },
            new Product { ProductName = "Design Patterns", AuthorName = "Erich Gamma", Price = 17.99, CategoryId = 6 },
            new Product { ProductName = "Code Complete", AuthorName = "Steve McConnell", Price = 21.99, CategoryId = 6 },
            new Product { ProductName = "The Pragmatic Programmer", AuthorName = "Andrew Hunt", Price = 18.99, CategoryId = 6 },
            new Product { ProductName = "Head First Design Patterns", AuthorName = "Eric Freeman", Price = 20.99, CategoryId = 6 }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }

    #endregion
}
