using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(author => author.AuthorId);
            builder.Property(author => author.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(author => author.LastName).HasMaxLength(100).IsRequired();
            builder.Property(author => author.BirthDate).IsRequired();
            builder.HasIndex(author => new { author.FirstName, author.LastName }).IsUnique();
        }
    }
}
