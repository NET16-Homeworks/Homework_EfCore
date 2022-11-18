using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(q => q.UserId);
            builder.Property(q => q.FirstName).IsRequired();
            builder.Property(q => q.LastName).IsRequired();
            builder.Property(q => q.BirthDate).IsRequired();
            builder.Property(q => q.Email).IsRequired();
            builder.HasIndex(q => q.Email).IsUnique();
            builder.Property(q => q.FirstName).HasMaxLength(30);
            builder.Property(q => q.LastName).HasMaxLength(30);
            builder.Property(q => q.Email).HasMaxLength(30);
        }
    }
}
