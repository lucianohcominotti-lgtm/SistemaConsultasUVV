# Sistema de Gestão de Consultas UVV

Aplicação ASP.NET Core MVC com .NET 8, Entity Framework Core, SQL Server e autenticação por cookie.

## Executar
1. Abra `SistemaConsultasUVV.sln` ou `SistemaConsultasUVV.csproj` no Visual Studio.
2. Abra o **Console do Gerenciador de Pacotes** em Ferramentas > Gerenciador de Pacotes do NuGet.
3. Execute:

```powershell
Add-Migration InitialCreate
Update-Database
```

4. Execute pelo botão HTTPS.

A conexão padrão usa SQL Server local com Windows Authentication:
`Server=localhost;Database=SistemaConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True`.

## Funcionalidades
- Cadastro e login com hash de senha;
- Rotas de consultas protegidas com `[Authorize]`;
- CRUD de consultas pertencentes ao usuário autenticado;
- Validações server-side com Data Annotations.

## Entrega
Substitua esta seção pelos nomes dos integrantes em ordem alfabética, o link do GitHub e o link do vídeo demonstrativo.
