namespace SchoolApp.Data;

public class Student : BaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }
}
