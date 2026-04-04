using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// SERVIÇOS
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=cripto.db"));

var app = builder.Build();

// PIPELINE
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => Results.Redirect("/scalar/v1"));

// ENDPOINT: PREÇO REAL
app.MapGet("/precos", async (IHttpClientFactory clientFactory) =>
{
    var client = clientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("User-Agent", "CryptoApp");
    var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";
    
    var dados = await client.GetFromJsonAsync<CoinGeckoDto>(url);
    return Results.Ok(new { BTC = dados?.bitcoin.usd, ETH = dados?.ethereum.usd });
});

app.Run();

// MODELOS E DTOs
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
public record CoinGeckoDto(PriceDetail bitcoin, PriceDetail ethereum);
public record PriceDetail(decimal usd);