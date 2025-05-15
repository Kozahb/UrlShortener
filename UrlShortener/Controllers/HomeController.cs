using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Entities;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Shorten(ShortUrlRequest request, [FromServices] UrlShortServices urlShortServices, [FromServices] AppDbContext dbContext)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                ViewBag.Error = "A URL informada é inválida.";
                return View("Index");  
            }

            var code = await urlShortServices.GenerateUniqueCode();

            var shortUrl = new ShortUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = request.Url,
                Code = code,
                ShortUrls = $"{Request.Scheme}: //{Request.Host}/api/{code}",
                CreatedOnUtc = DateTime.UtcNow
            };

            dbContext.ShortUrls.Add(shortUrl);
            await dbContext.SaveChangesAsync();

            ViewBag.ShortUrl = shortUrl.ShortUrls;
            return View("Index");

        }
    
        
    }
}
