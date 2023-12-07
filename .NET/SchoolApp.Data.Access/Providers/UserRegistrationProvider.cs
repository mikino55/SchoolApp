using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.Access.Providers;

public interface IUserRegistrationProvider
{
    Task<Teacher> RegisterTeacherAsync(Teacher teacher, string password);
    Task<Student> RegisterStudentAsync(Student student, string password);
}
public class UserRegistrationProvider(
    UserManager<User> userManager,
    IUserStore<User> userStore) : IUserRegistrationProvider
{   
    private readonly UserManager<User> userManager = userManager;
    private readonly IUserStore<User> userStore = userStore;

    public async Task<Student> RegisterStudentAsync(Student student, string password)
    {
        return await this.RegisterUserInternalAsync(student, password, UserRoleNames.Student);
    }

    public async Task<Teacher> RegisterTeacherAsync(Teacher teacher, string password)
    {
        return await this.RegisterUserInternalAsync(teacher, password, UserRoleNames.Teacher);
    }

    private async Task<TUser> RegisterUserInternalAsync<TUser>(TUser user, string password, string role) where TUser : User
    {
        var emailStore = (IUserEmailStore<User>)this.userStore;

        await this.userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, user.Email, CancellationToken.None);
        var result = await this.userManager.CreateAsync(user, password);
        await this.userManager.AddToRoleAsync(user, role);
        return user;
    }
}
