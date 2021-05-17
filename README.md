# TesteLuizaLabs

Projeto de teste para a LuizaLabs

### Tecnologias utilizadas
* [.Net Core 5](https://dotnet.microsoft.com/download)
* [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [AutoMapper](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/)
* [JWT](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/)
* [Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
* [Angular](https://angularjs.org/)

### Organização do projeto
##### [API](https://github.com/thiagomotoca/TesteLuizaLabs/tree/master/Backend/TesteLuizaLabs.Api)
* Controllers
* Helpers (AutoMapper e Filters)
* DTOs (Models)
* Configuração JWT
* Configuração Autorização
* Swagger

##### [Aplicacao](https://github.com/thiagomotoca/TesteLuizaLabs/tree/master/Backend/TesteLuizaLabs.Aplicacao)
* Entidades
* Registro das interfaces de Serviço e Repositório
* Registro das regras de negócio
* Aplicação das regras de negócio de acordo com a ação executada
* Acesso a dados
* Configuração das Tabelas do Banco
* Execução do Repositório
* Migrations

##### [Util](https://github.com/thiagomotoca/TesteLuizaLabs/tree/master/Backend/TesteLuizaLabs.Lib)
* Validação de email
* Exceção customizada para tratamento de erro
* Envio de e-mail

##### [Front](https://github.com/thiagomotoca/TesteLuizaLabs/tree/master/Frontend/LuizaLabs)
* Front end em Angular 8 para consumo da API

### Exposição das APIS
Para facilitar a execução do teste, utilizei banco de dados in memory.

Para executar a api, rodar os comandos na pasta TesteLuizaLabs.Api em ordem "dotnet restore", "dotnet build" e "dotnet run"

A API estará exposta na URL http://localhost:4000/

Você encontrará a documentação da API em http://localhost:4000/swagger/

### Execução do Front
Para executar o front é necessário ter o angular 8 instalado, executar os comandos na pasta LuizaLabs em ordem "npm install" e "ng serve"

O Front estará exposto na URL http://localhost:4200/

### Coisas a fazer (ToDo):
* Configurar CORS
* Testes unitários do serviço
* Dockerizar o backend e frontend