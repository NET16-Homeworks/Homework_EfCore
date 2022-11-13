using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.UserId);
            builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.BirthDate).IsRequired();
            builder.HasMany(user => user.Books)
                .WithMany(book => book.Users)
                .UsingEntity<UserBook>(
                userBook => userBook.HasOne(userBook => userBook.Book)
                .WithMany(userBook => userBook.UserBooks)
                .HasForeignKey(userBook => userBook.BookId),
                userBook => userBook.HasOne(userBook => userBook.User)
                .WithMany(userBook => userBook.UserBooks)
                .HasForeignKey(userBook => userBook.UserId));
        }
    }
}


