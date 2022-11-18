using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Configurations
{
    public class BooksConfigurations : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.HasKey(q => q.BookId);
            builder.Property(q => q.Name).IsRequired();
            builder.Property(q => q.Name).HasMaxLength(30);
            builder.Property(q => q.Year).IsRequired();
            builder.HasOne(q => q.Author).WithMany(q => q.Books).HasForeignKey(q => q.AuthorId);
            builder.HasMany(q => q.Users).WithMany(q => q.Books).UsingEntity<UserBooks>(
                                                            q=>q
                                                                .HasOne(w=>w.User)
                                                                .WithMany(w=>w.UserBooks)
                                                                .HasForeignKey(w=>w.UserId)
                                                                .OnDelete(DeleteBehavior.Cascade),
                                                            q=>q
                                                                .HasOne(w=>w.Book)
                                                                .WithMany(w=>w.UserBooks)
                                                                .HasForeignKey(w=>w.BookId)
                                                                .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
