using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(q => q.UserId);

            builder.Property(q => q.FirstName).HasMaxLength(15).IsRequired();
            builder.Property(q => q.LastName).HasMaxLength(15).IsRequired();
            builder.Property(q => q.Email).HasMaxLength(28).IsRequired();
            builder.HasIndex(q => q.Email).IsUnique();
            builder.Property(q => q.BirthDate).IsRequired();

            //эту связь лучше написать в mydbcontext? таблице?
            //builder.HasMany(q => q.Books)
            //    .WithMany(q => q.Users)
            //    .UsingEntity(j => j.ToTable("UserBooks"));

            builder.ToTable("Users");
        }
    }
}
