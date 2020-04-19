using System;

namespace LibraryApp.API.Mapper
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Author { get; set; }
    }
}
