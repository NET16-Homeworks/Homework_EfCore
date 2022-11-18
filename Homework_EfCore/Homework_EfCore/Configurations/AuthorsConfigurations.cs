using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Configurations
{
    public class AuthorsConfigurations : IEntityTypeConfiguration<Authors>
    {
        public void Configure(EntityTypeBuilder<Authors> builder)
        {
            builder.HasKey(q => q.AuthorId);
            builder.HasIndex(q => new
            {
                q.FirstName,
                q.LastName,
                q.Country
            }).IsUnique();
            builder.Property(q => q.FirstName).IsRequired();
            builder.Property(q => q.LastName).IsRequired();
            builder.Property(q => q.Country).IsRequired();
            builder.Property(q => q.FirstName).HasMaxLength(30);
            builder.Property(q => q.LastName).HasMaxLength(30);
            builder.Property(q => q.Country).HasMaxLength(30);
            builder.Property(q => q.BirthDate).IsRequired();
        }
    }
}
