using System.Collections;

namespace PGK.WebApi.Models
{
    public class ErrorDetails
    {
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
        public IDictionary Date { get; internal set; }
    }
}
