using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Configurations
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            // need to config further
        }
    }
}
