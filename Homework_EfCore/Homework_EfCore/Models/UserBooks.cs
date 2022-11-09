using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Models
{
    public class UserBooks
    {
        public int UserBooksId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }

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
