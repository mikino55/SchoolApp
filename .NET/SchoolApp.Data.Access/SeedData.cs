using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.Access;
public static class SeedData
{
    public static async Task Seed(IServiceProvider serviceProvider)
    { 
        using var scope = serviceProvider.CreateScope();

        var sp = scope.ServiceProvider;
        var dbContext = sp.GetRequiredService<ApplicationDbContext>();

        var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

        await roleManager.CreateAsync(new IdentityRole { Id = Guid.Parse("a48ead7e-4b77-4dd2-a32b-0fcfef51b5d9").ToString(), Name = UserRoleNames.User });
        await roleManager.CreateAsync(new IdentityRole { Id = Guid.Parse("e65cab87-471c-4f26-86e8-929cbcb4d287").ToString(), Name = UserRoleNames.Teacher });
        await roleManager.CreateAsync(new IdentityRole { Id = Guid.Parse("34baf407-7298-4f46-9b01-055218b16f77").ToString(), Name = UserRoleNames.Student });

    }
}
