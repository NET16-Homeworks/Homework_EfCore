using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Database.Configurations
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.HasKey(q => new { q.UserId, q.BookId });
            builder.Property(q => q.UserId).IsRequired();
            builder.Property(q => q.BookId).IsRequired();
            builder.ToTable("UserBooks");
        }
    }
}
