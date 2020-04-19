using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
