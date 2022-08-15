using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.BusinessException
{
    public class URLShortenerBusinessException : Exception
    {
        public URLShortenerBusinessException(string Message): base(Message)
        {

        }
    }
}
