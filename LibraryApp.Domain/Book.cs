using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Publisher { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
