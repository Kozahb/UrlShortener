namespace UrlShortener.Services
{
    public class UrlShortServices
    {

        private const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-123456789";

        private readonly Random _random = new();

        public string GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];

            for (var i = 0; i < NumberOfCharsInShortLink; i++) 
            {
                int randomIndex = _random.Next(Alphabet.Length - 1);
                codeChars[i] = Alphabet[randomIndex];   

            }

            return new string(codeChars);
        }
         
    }

}
