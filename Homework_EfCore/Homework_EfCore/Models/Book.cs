using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Homework_EfCore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int Year { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }

    }

   
}
