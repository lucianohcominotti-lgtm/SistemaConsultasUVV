# Sistema de Gestão de Consultas UVV

Aplicação Web desenvolvida para a disciplina de Desenvolvimento Web Back-end da UVV.

O sistema permite o cadastro de usuários e o gerenciamento de consultas médicas ou profissionais, utilizando arquitetura MVC, Entity Framework Core, SQL Server e autenticação segura.


## Tecnologias utilizadas

- C#
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- HTML5
- CSS3
- Git e GitHub

---

## Arquitetura do projeto

O projeto utiliza o padrão arquitetural MVC:

```text
Controllers
Data
Migrations
Models
ViewModels
Views
wwwroot
```

### Principais componentes

- **Models:** entidades `Usuario` e `Consulta`;
- **ViewModels:** modelos específicos para login, cadastro e dashboard;
- **Controllers:** responsáveis pelo fluxo da aplicação;
- **Views:** telas da aplicação;
- **Data:** contexto do Entity Framework Core;
- **Migrations:** histórico de alterações do banco de dados;
- **wwwroot:** arquivos CSS e recursos visuais.

---

## Funcionalidades

### Usuários

- Cadastro de usuário;
- Validação de nome, e-mail e senha;
- Verificação de e-mail duplicado;
- Login;
- Logout;
- Senhas armazenadas com hash seguro;
- Autenticação por Cookie.

### Consultas

- Cadastro de consulta;
- Listagem das consultas do usuário;
- Edição de consulta;
- Exclusão de consulta;
- Visualização dos detalhes;
- Definição de status;
- Busca por especialidade ou descrição;
- Filtro por status;
- Validação de data futura;
- Bloqueio de consultas no mesmo horário.

### Status disponíveis

- Agendada;
- Realizada;
- Cancelada.

---

## Segurança e validação

O sistema utiliza:

- Rotas protegidas com `[Authorize]`;
- Acesso de cada usuário somente às próprias consultas;
- Proteção contra requisições CSRF;
- Validações no servidor;
- Data Annotations;
- Senhas protegidas com `PasswordHasher`;
- Autenticação configurada no pipeline da aplicação;
- Validação de e-mail duplicado;
- Bloqueio de datas passadas;
- Bloqueio de horários duplicados.

---

## Pré-requisitos

Para executar o projeto, é necessário instalar:

- Visual Studio Community;
- Workload **ASP.NET e desenvolvimento Web**;
- .NET 8 SDK;
- SQL Server Developer ou SQL Server Express;
- SQL Server Management Studio.

---

## Configuração do banco de dados

O projeto utiliza SQL Server local com autenticação do Windows.

A conexão padrão está configurada no arquivo:

```text
appsettings.json
```

Connection String padrão:

```text
Server=localhost;Database=SistemaConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True
```

Caso o SQL Server esteja instalado como `SQLEXPRESS`, utilize:

```text
Server=localhost\SQLEXPRESS;Database=SistemaConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True
```

---

## Como executar o projeto

1. Clone ou baixe este repositório;
2. Abra o arquivo abaixo no Visual Studio:

```text
SistemaConsultasUVV.csproj
```

3. Confirme se o SQL Server está em execução;
4. Abra o **Console do Gerenciador de Pacotes** em:

```text
Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes
```

5. Verifique se o projeto padrão selecionado é:

```text
SistemaConsultasUVV
```

6. Se as migrations já estiverem no projeto, execute:

```powershell
Update-Database
```

7. Caso as migrations ainda não existam, execute:

```powershell
Add-Migration InitialCreate
Update-Database
```

8. Execute a aplicação pelo botão HTTPS do Visual Studio ou pressione:

```text
Ctrl + F5
```

---

## Fluxo de demonstração

Para testar o sistema:

1. Acesse a página inicial;
2. Crie uma conta;
3. Faça login;
4. Acesse o Dashboard;
5. Cadastre uma nova consulta;
6. Altere o status da consulta;
7. Consulte os detalhes;
8. Pesquise uma consulta;
9. Utilize o filtro por status;
10. Edite uma consulta;
11. Exclua uma consulta;
12. Faça logout;
13. Tente acessar a área de consultas sem login.

---

## Participantes

Substitua os exemplos abaixo pelos nomes reais dos integrantes, em ordem alfabética:

1. Caiqui Soares Wandekoken
2. Luciano Huwer Cominotti
3. Vitor França Barcelos Araújo Bravin
  

---

## Repositório

(https://github.com/lucianohcominotti-lgtm/SistemaConsultasUVV)


## Vídeo demonstrativo

O vídeo deve apresentar o cadastro, login, Dashboard e gerenciamento das consultas.

Substitua o endereço abaixo pelo link real do vídeo:

[ Assistir ao vídeo demonstrativo ](COLE_AQUI_O_LINK_DO_VIDEO)

---

## Observação

Este projeto foi desenvolvido para fins acadêmicos, com o objetivo de demonstrar conceitos de desenvolvimento Web Back-end, arquitetura MVC, persistência de dados, segurança e gerenciamento de consultas médicas ou profissionais.
