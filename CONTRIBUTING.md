# 🤝 Guia de Contribuição

Obrigado por se interessar em contribuir para o **MyCryptoApi**! 🚀

## Código de Conduta

Este projeto segue um Código de Conduta respeitoso:

- ✅ Seja respeitoso com todos
- ✅ Críticas sobre código, não sobre pessoas
- ✅ Inclusão é de todos nós

---

## Como Contribuir

### 1. Abra uma Issue

Antes de começar:

```markdown
**Descrição:**
Adicionar suporte a Litecoin

**Contexto:**
Aproveitar filtro de moedas do CoinGecko

**Versão .NET:**
dotnet --version
```

### 2. Fork & Clone

```bash
# Fork no GitHub (botão "Fork")
git clone https://github.com/SEU-USUARIO/Minimal-APIs-.NET.git
cd Minimal-APIs-.NET

# Adicionar remoto upstream
git remote add upstream https://github.com/RafaelBatistaDev/Minimal-APIs-.NET.git
```

### 3. Criar Branch

```bash
git fetch upstream
git checkout -b feature/addon-litecoin
```

### 4. Fazer Mudanças

**Padrão Minimal APIs:**

```csharp
// Program.cs - tudo organizado em ordem

// 1. Registrar serviços
builder.Services.AddHttpClient();
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite("Data Source=cripto.db")
);

// 2. Build app
var app = builder.Build();

// 3. Middleware
if (app.Environment.IsDevelopment())
    app.MapScalarApiReference();

// 4. Endpoints novos
app.MapGet("/novas-moedas", GetNovasMoedas);

// 5. Runner
app.Run();

// 6. Implementações (ao final)
async Task<IResult> GetNovasMoedas(IHttpClientFactory factory)
{
    try
    {
        var client = factory.CreateClient();
        var url = "...";
        var data = await client.GetFromJsonAsync<Dto>(url);
        return Results.Ok(data);
    }
    catch (HttpRequestException ex)
    {
        return Results.StatusCode(503);
    }
}
```

### 5. Testes

```bash
# Criar testes
dotnet new xunit -n MyCryptoApi.Tests
dotnet test

# Teste manual no MyCryptoApi.http
```

### 6. Commit Semântico

```bash
git commit -m "feat: adiciona Litecoin aos preços"
```

### 7. Push & PR

```bash
git push origin feature/addon-litecoin

# No GitHub, abra Pull Request com template:
```

**Template PR:**
```markdown
## Descrição
Resolve #123

Adiciona suporte a Litecoin.

## Checklist
- [x] Código segue style guidelines
- [x] Novo endpoint testado
- [x] Documentação atualizada
- [x] Sem breaking changes
```

---

## Padrões de Código

### Minimal APIs Style
```csharp
// ✅ BOM: Separação clara
app.MapGet("/dados", GetDados);
app.MapPost("/criar", CreateDado);

async Task<IResult> GetDados(IHttpClientFactory factory, AppDbContext db)
{
    try
    {
        var dados = await db.Dados.ToListAsync();
        return Results.Ok(dados);
    }
    catch (Exception ex)
    {
        return Results.StatusCode(500);
    }
}

// ❌ RUIM: Tudo inline
app.MapGet("/dados", async (IHttpClientFactory factory, AppDbContext db) => {
    // 100 linhas de código aninhado
});
```

### Records
```csharp
// ✅ BOM
public record PriceDto(decimal usd, decimal eur);

// ❌ EVITAR
public class PriceDto
{
    public decimal USD { get; set; }
    public decimal EUR { get; set; }
}
```

### Error Handling
```csharp
try
{
    var data = await client.GetFromJsonAsync<Dto>(url);
    return Results.Ok(data);
}
catch (HttpRequestException ex)
{
    return Results.StatusCode(503, "Serviço externo indisponível");
}
catch (Exception ex)
{
    return Results.StatusCode(500, "Erro interno");
}
```

---

## Checklist de PR

Antes de enviar:

- [ ] Código compila sem erros
- [ ] `dotnet test` passa
- [ ] Novo endpoint testado (MyCryptoApi.http)
- [ ] Comentários em código complexo
- [ ] README/DEVELOPMENT atualizado
- [ ] Commit message semântica
- [ ] Sem dados sensíveis commitados
- [ ] Sem breaking changes

---

## Comunicação

- 💬 Discussions: [GitHub Discussions](https://github.com/RafaelBatistaDev/Minimal-APIs-.NET/discussions)
- 🐛 Issues: [GitHub Issues](https://github.com/RafaelBatistaDev/Minimal-APIs-.NET/issues)

---

## Licença

Ao contribuir, você concorda que suas contribuições serão licenciadas sob [MIT License](LICENSE).

---

**Muito obrigado pela contribuição!** 🙏
