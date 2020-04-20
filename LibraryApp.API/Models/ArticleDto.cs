using System;

namespace LibraryApp.API.Mapper
{
    public class ArticleDto
    {
        /// <summary>
        /// The id of the Article
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The Title of the Article
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Body of the Article
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The Release Date of the Article
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The Author of the Article
        /// </summary>
        public string Author { get; set; }
    }
}
