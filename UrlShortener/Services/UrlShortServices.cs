using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Services
{
    public class UrlShortServices
    {

        public const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-123456789";

        private readonly Random _random = new();
        private readonly AppDbContext _dbContext;

       public UrlShortServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];

           while (true)
            {
                for (var i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    int randomIndex = _random.Next(Alphabet.Length - 1);
                    codeChars[i] = Alphabet[randomIndex];

                }

                var code = new string(codeChars);

                if (!await _dbContext.ShortUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }

            
        }
         
    }

}
