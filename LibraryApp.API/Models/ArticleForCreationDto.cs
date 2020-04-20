using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Mapper
{
    /// <summary>
    /// An article for creation with Title, Body, ReleaseDate and AuthorId fields
    /// </summary>
    public class ArticleForCreationDto
    {
        /// <summary>
        /// The Title of the Article
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// The Body of the Article
        /// </summary>
        [Required]
        [MaxLength(4000)]
        public string Body { get; set; }

        /// <summary>
        /// The Release Date of the Article
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The AuthorId of the Article
        /// </summary>
        [Required]
        public Guid AuthorId { get; set; }
    }
}
