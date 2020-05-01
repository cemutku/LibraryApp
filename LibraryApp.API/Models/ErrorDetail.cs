using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Models
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
