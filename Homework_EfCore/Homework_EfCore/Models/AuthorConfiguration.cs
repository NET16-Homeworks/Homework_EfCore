using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Models
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.AuthorId);
            builder.Property(q => q.AuthorId).UseIdentityColumn();
            builder.Property(q => q.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(q => q.LastName).HasMaxLength(50).IsRequired();
            builder.Property(q => q.Country).HasMaxLength(50).IsRequired();
            builder.Property(q => q.BirthDate).IsRequired();
        }
    }
}
