using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domain
{
    public class Article
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(4000)]
        public string Body { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
