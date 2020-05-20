using System;

namespace LibraryApp.API.Mapper
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
