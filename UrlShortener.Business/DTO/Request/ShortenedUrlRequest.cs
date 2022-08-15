using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.DTO.Request
{
    public class ShortenedUrlRequest
    {
        [Required(ErrorMessage = "Url is required.")]
        public string Url { get; set; }
    }
}
