using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Models
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBooks>
    {
        public void Configure(EntityTypeBuilder<UserBooks> builder)
        {
            builder.HasKey(x => x.UserBooksId);
            builder.Property(q => q.UserId).IsRequired();
            builder.Property(q => q.BookId).IsRequired();
        }
    }
}
