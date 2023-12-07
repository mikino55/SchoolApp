using Microsoft.AspNetCore.Identity;

namespace SchoolApp.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class User : IdentityUser
{
    public UserType UserType { get; set; } = UserType.User;

    public required string FirstName { get; set; }

    public required string LastName { get; set; }
}

public class Teacher : User
{
    public Teacher()
    {
        this.UserType = UserType.Teacher;
    }
}

public class Student : User
{
    public Student()
    {
        this.UserType = UserType.Student;
    }
}

public enum UserType
{
    User = 1,
    Teacher = 2,
    Student = 3,
}

