using UrlShortener.Models;

namespace UrlShortener.Entities
{
    public class ShortUrl 
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string ShortUrls { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;    

        public DateTime CreatedOnUtc { get; set; }
    }
}
