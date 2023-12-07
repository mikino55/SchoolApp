using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.Access.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasDiscriminator(x => x.UserType)
            .HasValue<User>(UserType.User)
            .HasValue<Student>(UserType.Student)
            .HasValue<Teacher>(UserType.Teacher);

        builder.Property(x => x.FirstName).IsRequired(true);
        builder.Property(x => x.LastName).IsRequired(true);
    }
}

internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasBaseType<User>();
    }
}


internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasBaseType<User>();
    }
}

