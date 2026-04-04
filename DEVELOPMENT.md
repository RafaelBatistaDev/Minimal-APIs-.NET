# 🔨 Guia de Desenvolvimento

## Ambiente Recomendado

### IDE/Editor
- **Visual Studio 2022** (recomendado para Windows)
- **Visual Studio Code** + C# DevKit
- **JetBrains Rider** (pago)

### Extensões VS Code
```json
{
  "recommendations": [
    "ms-dotnettools.csharp",
    "ms-dotnettools.vscode-dotnet-runtime",
    "eamodio.gitlens",
    "EditorConfig.EditorConfig",
    "humao.rest-client"
  ]
}
```

---

## Setup Inicial

### 1. Clone e Dependências
```bash
git clone https://github.com/RafaelBatistaDev/Minimal-APIs-.NET.git
cd Minimal-APIs-.NET
dotnet restore
```

### 2. Entender a Estrutura Minimal APIs

```csharp
// Program.cs - Tudo em um arquivo
var builder = WebApplication.CreateBuilder(args);

// Registrar serviços (DI)
builder.Services.AddHttpClient();
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite("Data Source=cripto.db")
);
builder.Services.AddOpenApi();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();           // Swagger schema
    app.MapScalarApiReference(); // UI elegante
}

// Endpoints (rotas)
app.MapGet("/precos", GetPrecos);

app.Run();

// Implementação (ao final do arquivo)
async Task<IResult> GetPrecos(IHttpClientFactory factory)
{
    var client = factory.CreateClient();
    var url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";
    var data = await client.GetFromJsonAsync<CoinGeckoDto>(url);
    return Results.Ok(new { BTC = data?.bitcoin.usd, ETH = data?.ethereum.usd });
}
```

### 3. Build Local
```bash
# Debug
dotnet build

# Release
dotnet build -c Release
```

### 4. Run em Watch Mode
```bash
dotnet watch run
```

### 5. Debug
```bash
# VS Code: F5
# Visual Studio: F5
# Swagger: https://localhost:5001/openapi/v1.json
# Scalar UI: https://localhost:5001/scalar/v1
```

---

## Workflow de Feature

### Exemplo: Adicionar Nova Criptmoeda

```bash
# 1. Branch
git checkout -b feature/adiciona-litecoin

# 2. Editar Program.cs
# Modificar linha da URL do CoinGecko:
# De: ?ids=bitcoin,ethereum&vs_currencies=usd
# Para: ?ids=bitcoin,ethereum,litecoin&vs_currencies=usd

# 3. Update response model
# Adicionar à DTO CoinGeckoDto: record CoinGeckoDto(PriceDetail bitcoin, PriceDetail ethereum, PriceDetail litecoin);

# 4. Testar localmente
dotnet run
# Acessar: https://localhost:5001/precos

# 5. Teste manual no MyCryptoApi.http
# Adicionar novo teste

# 6. Commit
git add .
git commit -m "feat: adiciona cotação de Litecoin"

# 7. Push e PR
git push origin feature/adiciona-litecoin
```

---

## Commands Úteis

### Diagnosticar
```bash
dotnet --version
dotnet project-info
dotnet sln list
```

### Limpeza
```bash
dotnet clean
dotnet nuget locals all --clear
rm -rf bin obj
```

### Testes HTTP
```bash
# Usando arquivo MyCryptoApi.http (VS Code)
# Ou via REST Client extension

# Terminal
curl -k https://localhost:5001/precos | jq

# PowerShell
Invoke-WebRequest -Uri "https://localhost:5001/precos" | ConvertTo-Json
```

---

## Padrões de Código

### Minimal APIs Pattern
```csharp
// ✅ BOM: Claro e direto
app.MapGet("/precos", GetPrecos);
app.MapPost("/alertas", CreateAlerta);

async Task<IResult> GetPrecos(IHttpClientFactory factory)
{
    try
    {
        // lógica
        return Results.Ok(data);
    }
    catch (HttpRequestException ex)
    {
        return Results.StatusCode(503);
    }
}

// ❌ EVITAR: Lambda desorganizado
app.MapGet("/precos", async (IHttpClientFactory factory) => 
{
    // 200 linhas de código
});
```

### Records para DTOs
```csharp
// ✅ BOM: Conciso e claro
public record CoinGeckoDto(PriceDetail bitcoin, PriceDetail ethereum);
public record PriceDetail(decimal usd);

// Uso
var data = await client.GetFromJsonAsync<CoinGeckoDto>(url);
var price = data?.bitcoin.usd;
```

### Async/Await
```csharp
// ✅ BOM
app.MapGet("/precos", async () =>
{
    var data = await client.GetFromJsonAsync<Dto>(url);
    return Results.Ok(data);
});

// ❌ ERRADO
app.MapGet("/precos", () =>
{
    var data = client.GetFromJsonAsync<Dto>(url).Result; // Deadlock!
    return Results.Ok(data);
});
```

---

## Entity Framework Core (Minimal)

### DbContext
```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    public DbSet<CriptomiedaAlerta> Alertas { get; set; } = null!;
}

public class CriptomiedaAlerta
{
    public int Id { get; init; }
    public string Moeda { get; set; } = null!;
    public decimal PrecoCritico { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
```

### Usar em Endpoints
```csharp
app.MapPost("/alertas", async (AppDbContext db, CriptomiedaAlerta alert) =>
{
    db.Alertas.Add(alert);
    await db.SaveChangesAsync();
    return Results.Created($"/alertas/{alert.Id}", alert);
});

app.MapGet("/alertas", async (AppDbContext db) =>
{
    var alertas = await db.Alertas.ToListAsync();
    return Results.Ok(alertas);
});
```

---

## Testing com Minimal APIs

### xUnit + FluentAssertions
```csharp
[Fact]
public async Task GetPrecos_ReturnsOkResult()
{
    // Arrange
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

    // Act
    var response = await client.GetAsync("/precos");

    // Assert
    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    var content = await response.Content.ReadAsStringAsync();
    content.Should().Contain("btc");
}
```

---

## Troubleshooting

### "Port 5001 already in use"
```bash
# Listar
lsof -i :5001  # macOS/Linux
netstat -ano | findstr :5001  # Windows

# Matar
kill -9 <PID>

# Usar porta diferente
dotnet run --urls "https://localhost:5002"
```

### "Cannot GET https://localhost:5001"
```bash
# Verificar se a app está rodando
curl -k https://localhost:5001/scalar/v1

# Verificar logs
dotnet run  # Ver output direto
```

### "CoinGecko API error"
```bash
# Testar diretamente
curl "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd"

# Rate limiting?
# CoinGecko tem limite de 10-50 req/min gratuito
# Adicione delay ou cache
```

---

## Git Workflow

### Branches
```
main (production)
  ↑
  ├── develop (staging)
  └── feature/* (features)
```

### Commits
```
feat:    Nova criptmoeda
fix:     Bug fix
docs:    Docs
refactor: Refactor
perf:    Performance
test:    Testes
chore:   Manutenção
```

---

## Performance Tips

### HttpClient Reuse
```csharp
// ✅ BOM: Factory pattern (reutiliza conexões)
builder.Services.AddHttpClient();
var client = httpClientFactory.CreateClient();

// ❌ RUIM: Nova instância cada vez
var client = new HttpClient();  // Esgota sockets!
```

### Async All The Way
```csharp
// ✅ BOM
async Task<IResult> GetPrecos(IHttpClientFactory factory)
{
    var data = await client.GetFromJsonAsync(url);
    return Results.Ok(data);
}

// ❌ RUIM
IResult GetPrecos(IHttpClientFactory factory)  // Sync
{
    var data = client.GetFromJsonAsync(url).Result;  // Deadlock
    return Results.Ok(data);
}
```

---

## Recursos

- 📖 [ASP.NET Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- 🪙 [CoinGecko API](https://www.coingecko.com/en/api/documentation)
- 📘 [Scalar UI](https://docs.scalar.com)
- 🧪 [xUnit](https://xunit.net/)

---

**Boa sorte desenvolvendo!** 🚀
