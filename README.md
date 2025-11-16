1. Overview

Este repositório contém o backend oficial utilizado como API para o módulo de Pacientes do sistema Be3 Health.

A solução foi desenvolvida em .NET (ASP.NET Core) com Entity Framework Core, seguindo uma organização em camadas (API / Application / Domain / Infra) alinhada a padrões utilizados em ambientes corporativos.

Embora o foco do desafio técnico seja o frontend (Angular), este backend foi criado como um serviço de apoio, permitindo testes funcionais reais (CRUD completo) e integração fim a fim com a aplicação de pacientes.

🔗 Frontend associado ao projeto:
https://github.com/antoniolispbr/be3-health-frontend

2. Objetivos da API

A API tem como objetivos principais:

Disponibilizar endpoints REST para cadastro e gestão de Pacientes

Servir de backend para o módulo de Pacientes no frontend Angular

Demonstrar conhecimento em:

ASP.NET Core Web API

Entity Framework Core

Organização em camadas (Domain/Application/Infra/API)

Boas práticas de estrutura e legibilidade de código

3. Tecnologias & Frameworks
Tecnologia	Utilização
ASP.NET Core Web API	Exposição dos endpoints REST
Entity Framework Core	Acesso a dados e mapeamento ORM
C#	Linguagem principal da solução
FluentValidation / Data Annotations (se usado)	Validações de entrada
SQL Server / PostgreSQL / InMemory	Persistência (dependendo da configuração)
xUnit / MSTest (opcional)	Suporte a testes unitários

(Ajuste os itens conforme o que você efetivamente está usando.)

4. Arquitetura da Solução

A solução está organizada em camadas para facilitar manutenção, testes e evolução:

Be3.Health.sln
├── Be3.Health.Api/          # Camada de apresentação (Web API)
├── Be3.Health.Application/  # Casos de uso, serviços de aplicação, DTOs
├── Be3.Health.Domain/       # Entidades de domínio e regras de negócio
└── Be3.Health.Infra/        # Acesso a dados, EF Core, repositórios, contexto

Princípios aplicados

Separação de responsabilidades por camada

Entidades de domínio desacopladas de detalhes de infraestrutura

Serviços de aplicação orquestrando regras de negócio e persistência

Controllers finos, delegando lógica para serviços

Organização compatível com ambientes corporativos de médio e grande porte

5. Como Executar em Ambiente de Desenvolvimento
1. Restaurar dependências
dotnet restore

2. Compilar a solução
dotnet build

3. (Opcional) Aplicar migrations no banco de dados

Se estiver usando Entity Framework Core com migrations configuradas:

dotnet ef database update


Certifique-se de executar o comando na pasta do projeto que contém o DbContext (geralmente Be3.Health.Infra ou similar), ou ajuste o --project e --startup-project conforme necessário.

4. Executar a API
dotnet run --project Be3.Health.Api


A API ficará disponível em URLs semelhantes a:

https://localhost:5001
http://localhost:5000


(Endereços exatos dependem da configuração de launchSettings.json.)

6. Configuração de Ambiente

As configurações de ambiente ficam concentradas nos arquivos:

Be3.Health.Api/
└── appsettings.json
    appsettings.Development.json

Exemplos de configurações comuns
String de conexão
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Be3Health;User Id=...;Password=...;"
}

Configuração de logging (exemplo simplificado)
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}


Em contexto de avaliação técnica, é comum manter credenciais genéricas ou usar banco em memória.

7. Endpoints Principais

A API expõe endpoints voltados ao domínio de Pacientes (ajuste conforme sua implementação):

GET    /api/pacientes           # Lista pacientes
GET    /api/pacientes/{id}      # Detalhe de um paciente
POST   /api/pacientes           # Criação de paciente
PUT    /api/pacientes/{id}      # Atualização de paciente
DELETE /api/pacientes/{id}      # Exclusão de paciente


Esses endpoints são consumidos diretamente pelo frontend Angular no módulo Be3 Pacientes.


8. Diferenciais Entregues

Apesar de o backend não ser o foco do desafio, esta API entrega alguns pontos relevantes:

✔️ Backend completo criado proativamente para suportar o frontend

✔️ Organização em camadas (API, Application, Domain, Infra)

✔️ Uso de ASP.NET Core e EF Core aderente a práticas modernas

✔️ Estrutura pronta para expansão de novas entidades e módulos

✔️ Permite testes funcionais reais do frontend (CRUD completo)

✔️ Facilita demonstração de visão full-stack em contexto corporativo

9. Conclusão

Este backend foi desenvolvido com o objetivo de:

Suportar o módulo de Pacientes do frontend Be3 Health

Demonstrar capacidade de trabalhar com .NET / ASP.NET Core

Oferecer uma base limpa, organizada e preparada para evolução

Mesmo não sendo o foco principal do desafio técnico, ele agrega valor ao processo, evidenciando:

autonomia,

versatilidade,

rapidez de aprendizado

e visão de sistema completo (front + back).

Obrigado pela análise e pela oportunidade! 👋
