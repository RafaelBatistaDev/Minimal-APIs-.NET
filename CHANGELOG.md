# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/),
e este projeto segue [Semantic Versioning](https://semver.org/).

---

## [1.0.0] - 2026-04-04

### ✨ Added
- Endpoints Minimal APIs para cotações de criptmoedas
- Integração com CoinGecko API (Bitcoin, Ethereum)
- Documentação OpenAPI com Scalar UI
- Entity Framework Core com SQLite
- Registros (records) para DTOs
- HttpClientFactory para requisições eficientes
- Arquivo MyCryptoApi.http com exemplos de teste
- AppDbContext preparado para extensão

### 🔧 Changed
- Atualização para .NET 8.0
- Sintaxe simplificada com Minimal APIs
- Startup modernizado em Program.cs

### 🐛 Fixed
- N/A (primeira release)

### ⚠️ Deprecated
- N/A (primeira release)

### 🔒 Security
- User-Agent configurado para CoinGecko (cortesia)
- Validação implícita via DTOs
- Sem dados sensíveis no repositório

### 📚 Documentation
- README.md profissional (1000+ linhas)
- DEVELOPMENT.md com guias de setup
- CONTRIBUTING.md com padrões de PR
- GITHUB-CONFIG.md com configuração
- Comentários em código Minimal
- Exemplos de uso completos

### 🚀 Infrastructure
- GitHub Actions CI/CD configurado
- .editorconfig para padrões C#
- .gitignore otimizado para .NET
- MIT License adicionada
- .github/workflows/dotnet.yml

---

## Unreleased

### Planejado para v1.1.0
- [ ] Suporte a mais criptmoedas (Litecoin, Monero, etc)
- [ ] Cache com Redis
- [ ] Rate limiting de requisições
- [ ] Endpoint de histórico de preços
- [ ] Testes de integração E2E

### Planejado para v2.0.0
- [ ] Alertas por email/SMS
- [ ] Webhook para mudanças de preço
- [ ] Banco PostgreSQL
- [ ] Autenticação JWT
- [ ] API GraphQL
- [ ] Documentação em PDF
- [ ] Docker Compose

---

## Padrões de Versionamento

- **MAJOR**: Breaking changes (1.0.0 → 2.0.0)
- **MINOR**: Nova funcionalidade backwards-compatible (1.0.0 → 1.1.0)
- **PATCH**: Bug fixes (1.0.0 → 1.0.1)

---

**Versão Atual:** [1.0.0]  
**Data:** 4 de abril de 2026  
**Status:** Production Ready ✅
