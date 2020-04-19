using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Mapper
{
    public class ArticleForCreationDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Body { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
