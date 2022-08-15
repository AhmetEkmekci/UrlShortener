using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.DTO.Response
{
    public class ShortenedUrlResponse
    {
        public string Hash { get; set; }
        public string Url { get; set; }
    }
}
