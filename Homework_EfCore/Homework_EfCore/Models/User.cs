using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }

    }

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
        }
    }
}
