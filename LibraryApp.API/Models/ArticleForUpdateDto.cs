using System;

namespace LibraryApp.API.Mapper
{
    public class ArticleForUpdateDto
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid AuthorId { get; set; }
    }
}
