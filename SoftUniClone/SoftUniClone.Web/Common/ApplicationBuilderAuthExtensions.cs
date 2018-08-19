using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SoftUniClone.Models;


namespace SoftUniClone.Web.Common
{
    public static class ApplicationBuilderAuthExtensions
    {

        private const string DefaultAdminPasswprd = "admin123"; // the framework HASH this pass
        private const string DefaultLecturerPasswprd = "lecturer123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Lecturer")
        };

        // TODO: Use a dictionary (string-> user) for roles and users

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        var result = await roleManager.CreateAsync(role);
                    }
                }

                var admin = await userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    admin = new User()
                    {
                        UserName = "admin",
                        Email = "admin@example.com"
                    };

                    var result = await userManager.CreateAsync(admin, DefaultAdminPasswprd);

                    result = await userManager.AddToRoleAsync(admin, roles[0].Name);
                }


                var lecturer = await userManager.FindByNameAsync("lecturer");
                if (lecturer == null)
                {
                    lecturer = new User()
                    {
                        UserName = "lecturer",
                        Email = "lecturer@example.com"
                    };

                    var result = await userManager.CreateAsync(lecturer, DefaultLecturerPasswprd);

                    result = await userManager.AddToRoleAsync(lecturer, roles[1].Name);
                }
            }
        }
    }
}
