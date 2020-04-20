using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Mapper
{
    /// <summary>
    /// An article for update with Id, Title, Body, ReleaseDate, AuthorId fields
    /// </summary>
    public class ArticleForUpdateDto
    {
        /// <summary>
        /// The id of the Article
        /// </summary>
        [Required]
        public Guid Id { get; set; }

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
        public Guid AuthorId { get; set; }
    }
}
