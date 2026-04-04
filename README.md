# 💰 MyCryptoApi - Minimal APIs

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue)](https://dotnet.microsoft.com)
[![C#](https://img.shields.io/badge/C%23-12-green)](https://docs.microsoft.com/dotnet/csharp)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Minimal%20APIs-v8-purple)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
[![GitHub](https://img.shields.io/badge/GitHub-Public-black)](https://github.com/RafaelBatistaDev/Minimal-APIs-.NET)

API de cotações de criptmoedas construída com ASP.NET Minimal APIs e integração em tempo real com CoinGecko.

**Licença:** MIT — Open source, use livremente! 📜

---

## 📋 Sobre o Projeto

**MyCryptoApi** é uma API leve e moderna que demonstra as melhores práticas com **Minimal APIs** do ASP.NET Core:

✅ **Minimal APIs** — Sintaxe simplificada do ASP.NET  
✅ **Cotações em tempo real** via CoinGecko (Bitcoin, Ethereum)  
✅ **Banco de dados SQLite** com Entity Framework Core  
✅ **OpenAPI/Swagger** com Scalar UI  
✅ **C# moderno** (.NET 8+, records, nullable types)  
✅ **Pronto para produção** — Zero bloat, máximo performance  

### Funcionalidades Principais

- **GET /** — Redireciona para documentação Scalar
- **GET /precos** — Retorna cotações de BTC/ETH em USD
- **GET /openapi/v1.json** — Schema OpenAPI
- **Scalar UI** — Documentação interativa e elegante
- **SQLite integrado** — Banco local sem configuração
- **HttpClient factory** — Requisições eficientes

---

## 🛠️ Requisitos

- **.NET SDK 8.0** ou superior ([Download](https://dotnet.microsoft.com/download))
- **Git** (para versionamento)
- Internet (para chamar CoinGecko API)

**Verificar instalação:**
```bash
dotnet --version
```

---

## 📂 Estrutura do Projeto

```
Minimal-APIs-.NET/
├── Program.cs                    # Entry point com todos endpoints
├── MyCryptoApi.csproj            # Configuração do projeto
├── MyCryptoApi.sln               # Solução Visual Studio
│
├── Properties/
│   └── launchSettings.json       # Configuração de launch
│
├── appsettings.json              # Configurações gerais
├── appsettings.Development.json  # Config development
├── MyCryptoApi.http              # Testes de endpoints
│
├── .github/workflows/            # CI/CD
├── .gitignore
├── .editorconfig
├── LICENSE
└── README.md
```

---

## 🚀 Quick Start

### 1. **Clone o Repositório**
```bash
git clone https://github.com/RafaelBatistaDev/Minimal-APIs-.NET.git
cd Minimal-APIs-.NET
```

### 2. **Instale Dependências**
```bash
dotnet restore
```

### 3. **Execute em Desenvolvimento**
```bash
dotnet run
```

A API estará disponível em: `https://localhost:5001`  
Scalar UI: `https://localhost:5001/scalar/v1`

### 4. **Testar Endpoints**

**Obter Preços (BTC/ETH):**
```bash
curl https://localhost:5001/precos
```

**Response:**
```json
{
  "btc": 45000.00,
  "eth": 2500.00
}
```

### 5. **Build de Produção**
```bash
dotnet publish -c Release -o ./publish
```

---

## 📦 Dependências

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `Microsoft.AspNetCore.OpenApi` | 8.0+ | OpenAPI/Swagger |
| `Scalar.AspNetCore` | 1.0+ | Documentação UI (alternativa elegante ao Swagger) |
| `Microsoft.EntityFrameworkCore.Sqlite` | 8.0+ | Banco de dados |
| `System.Net.Http.Json` | 8.0+ | Integração JSON com HTTP |

---

## 🔌 Endpoints da API

### Documentação
```http
GET / HTTP/1.1
Host: localhost:5001
```
**Redireciona para:** Scalar UI (documentação visual)

### Obter Preços
```http
GET /precos HTTP/1.1
Host: localhost:5001
Accept: application/json
```

**Response (200 OK):**
```json
{
  "btc": 45000.00,
  "eth": 2500.00
}
```

**Response (erro - 500):**
```json
{
  "error": "Falha ao buscar preços da CoinGecko"
}
```

---

## 📊 Como Funciona

### Fluxo da Requisição

```
GET /precos
    ↓
HttpClientFactory cria cliente
    ↓
Headers incluem "User-Agent: CryptoApp"
    ↓
Chamada: https://api.coingecko.com/api/v3/simple/price
    ↓
Parse JSON com CoinGeckoDto record
    ↓
200 OK com { BTC, ETH }
```

### Integração CoinGecko

A API chamada:
```
https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd
```

Resposta bruta:
```json
{
  "bitcoin": { "usd": 45000.00 },
  "ethereum": { "usd": 2500.00 }
}
```

---

## 🧪 Testes

### Via MyCryptoApi.http
Abra o arquivo `MyCryptoApi.http` na IDE e execute os testes visual.

### Via terminal
```bash
# Teste de conectividade
curl -k https://localhost:5001/

# Teste de preços
curl -k https://localhost:5001/precos | jq
```

### Testes automatizados
```bash
dotnet new xunit -n MyCryptoApi.Tests
dotnet add MyCryptoApi.Tests reference MyCryptoApi
dotnet test
```

---

## 📦 Deploy

### 1. **Azure App Service**
```bash
dotnet publish -c Release
az webapp up --name mycryptoapi --resource-group mygroup
```

### 2. **Docker**
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyCryptoApi.dll"]
```

### 3. **Heroku / Railway**
```bash
dotnet publish -c Release -o ./publish
# Deploy manual ou via CLI
```

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Por favor:

1. **Fork** o repositório
2. **Crie um branch** (`git checkout -b feature/NovaCriptmoeda`)
3. **Commit** as mudanças (`git commit -m 'feat: adiciona cotação Litecoin'`)
4. **Push** para o branch (`git push origin feature/NovaCriptmoeda`)
5. **Abra uma Pull Request**

### Padrões de Código
- ✅ C# moderno (C# 12+)
- ✅ Minimal APIs com sintaxe clara
- ✅ Records para DTOs
- ✅ Async/await
- ✅ Testes unitários
- ✅ Documentação completa

---

## 📜 Uso Público & Licença

Este projeto é **100% Open Source** sob a licença **MIT**.

### ✅ Você pode:
- ✅ Usar em projetos pessoais e comerciais
- ✅ Modificar e adaptar livremente
- ✅ Distribuir obras derivadas
- ✅ Usar privadamente

### ⚠️ Condições:
- ⚠️ Incluir cópia da licença MIT
- ⚠️ Indicar mudanças significativas

---

## 🔐 Segurança

- ✅ Sem chaves de API no repositório
- ✅ Validação de entrada
- ✅ HTTPS em produção
- ✅ User-Agent configurado (cortesia com CoinGecko)
- ✅ Code aberto para auditoria

---

## 📞 Suporte e Recursos

- 📖 [ASP.NET Minimal APIs Docs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- 🪙 [CoinGecko API](https://www.coingecko.com/en/api/documentation)
- 📘 [Scalar UI Documentation](https://docs.scalar.com)
- 🐛 [Issues](https://github.com/RafaelBatistaDev/Minimal-APIs-.NET/issues)

---

## 🎯 Roadmap

- [ ] Suporte a mais criptmoedas (Litecoin, Ripple, etc)
- [ ] Cache com Redis
- [ ] Rate limiting
- [ ] Histórico de preços
- [ ] Alertas por email
- [ ] Banco de dados PostgreSQL
- [ ] Testes de integração
- [ ] Docker Compose

---

**Última atualização:** 4 de abril de 2026  
**Versão:** 1.0.0  
**Licença:** MIT  
**Status:** Production Ready ✅

---

## 👤 Autor

**Rafael Batista**  
🔗 GitHub: [@RafaelBatistaDev](https://github.com/RafaelBatistaDev)  
💼 LinkedIn: [Seu Perfil](https://linkedin.com)
