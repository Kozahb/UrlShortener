using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Entities;
using UrlShortener.Models;
using UrlShortener.Services;
using Web.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<UrlShortServices>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();

    app.ApplyMigrations();
}


app.MapPost("api/shorten", async (
    ShortUrlRequest request,
    UrlShortServices urlShortServices, AppDbContext dbContext, HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("The specified URL is invalid.");
    }

    var code = await urlShortServices.GenerateUniqueCode();

    var shortUrl = new ShortUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        Code = code,
        ShortUrls = $"{httpContext.Request.Scheme}: //{httpContext.Request.Host}/api/{code}",
        CreatedOnUtc = DateTime.UtcNow
    };

    dbContext.ShortUrls.Add(shortUrl); 

    await dbContext.SaveChangesAsync();

    return Results.Ok(shortUrl.ShortUrls);
});


app.MapGet("api/{code}", async (string code, AppDbContext dbContext) =>
{
    var shorUrl = await dbContext.ShortUrls.FirstOrDefaultAsync(s => s.Code == code);

    if (shorUrl is null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(shorUrl.LongUrl);  
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
