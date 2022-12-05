using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(q => q.UserId).UseIdentityColumn();
            builder.Property(q => q.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(q => q.LastName).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Email).IsRequired().HasMaxLength(50);
            builder.Property(q => q.BirthDate).IsRequired();
            builder.HasIndex(q => q.Email).IsUnique();

            builder.
                HasMany(q => q.Books).
                WithMany(q => q.Users).
                UsingEntity<UserBooks>(
                q => q.HasOne(q => q.Book).WithMany(q => q.UserBooks).HasForeignKey(q => q.BookId),
                q => q.HasOne(q => q.User).WithMany(q => q.UserBooks).HasForeignKey(q => q.UserId)
                );
        }
    }
}
