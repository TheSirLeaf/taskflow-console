# TaskFlow

Repositório do projeto **TaskFlow**, com dois alvos em andamento:

- **TaskFlow.Console** — aplicação console de gerenciamento de tarefas (*To-Do List*), conforme a [lista de exercícios](lista_de_exerc_cios_git_semantic_flow_e_net.md).
- **TaskFlow.API** — Web API (ASP.NET Core + Entity Framework Core) para o mesmo domínio de tarefas.

## Pré-requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) ou superior (`dotnet --version`)
- (Opcional, para a API) SQL Server acessível via connection string `ConexaoPadrao`

## Estrutura da solução

| Projeto | Descrição |
|---|---|
| `TaskFlow.Console/` | Aplicação console (estrutura inicial — implementação pendente) |
| `TaskFlow.Tests/` | Projeto de testes unitários com xUnit, referenciando o console |
| `TaskFlow.API/` | Web API com EF Core, controllers e migrations |

## Compilar

```bash
dotnet build TaskFlow.slnx
```

## Executar os testes

```bash
dotnet test TaskFlow.slnx
```

> Neste momento o projeto de testes contém apenas o teste de exemplo gerado pelo template (`UnitTest1`). Os testes de `TaskManager` previstos na lista de exercícios ainda não foram implementados.

## Rodar a aplicação console

```bash
dotnet run --project TaskFlow.Console
```

> O console ainda está na estrutura inicial (`Hello, World!`). O menu interativo previsto na lista de exercícios ainda não foi implementado.

## Rodar a API

```bash
dotnet run --project TaskFlow.API
```

Com o ambiente de desenvolvimento ativo, o Swagger fica disponível em `https://localhost:<porta>/swagger`.

---

# TaskFlow.API — Web API de Tarefas

Desafio de projeto do módulo de **API e Entity Framework** (trilha .NET, [DIO](https://www.dio.me)): um sistema gerenciador de tarefas com CRUD completo.

## Contexto

A aplicação deverá permitir cadastrar, consultar, atualizar e deletar tarefas. A classe principal é `Tarefa`:

![Diagrama da classe Tarefa](TaskFlow.API/diagrama.png)

As migrations do Entity Framework Core já estão versionadas em `TaskFlow.API/Migrations`.

## Métodos esperados

**Swagger**

![Métodos Swagger](TaskFlow.API/swagger.png)

**Endpoints**

| Verbo  | Endpoint                | Parâmetro | Body          |
|--------|-------------------------|-----------|---------------|
| GET    | /Tarefa/{id}            | id        | N/A           |
| PUT    | /Tarefa/{id}            | id        | Schema Tarefa |
| DELETE | /Tarefa/{id}            | id        | N/A           |
| GET    | /Tarefa/ObterTodos      | N/A       | N/A           |
| GET    | /Tarefa/ObterPorTitulo  | titulo    | N/A           |
| GET    | /Tarefa/ObterPorData    | data      | N/A           |
| GET    | /Tarefa/ObterPorStatus  | status    | N/A           |
| POST   | /Tarefa                 | N/A       | Schema Tarefa |

Schema (model) de `Tarefa` utilizado nos métodos que exigirem body:

```json
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "data": "2022-06-08T01:31:07.056Z",
  "status": "Pendente"
}
```

## Configuração do banco

A connection string fica em `TaskFlow.API/appsettings.Development.json`, na chave `ConexaoPadrao`:

```
Data Source=localhost;Initial Catalog=Agenda;Integrated Security=True; TrustServerCertificate=True
```

Ajuste-a conforme o seu ambiente antes de rodar a API.
