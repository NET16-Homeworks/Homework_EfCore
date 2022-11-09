using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }

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
