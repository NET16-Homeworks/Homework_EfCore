using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Database.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(q => q.AuthorId);

            builder.Property(q => q.FirstName).HasMaxLength(15).IsRequired();
            builder.Property(q => q.LastName).HasMaxLength(15).IsRequired();
            builder.Property(q => q.Country).HasMaxLength(15).IsRequired();
            builder.Property(q => q.BirthDate).IsRequired();

            builder.HasMany(q => q.Books)
                .WithOne(q => q.Author)
                .HasForeignKey(q => q.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);          
            builder.ToTable("Authors");
        }
    }
}
