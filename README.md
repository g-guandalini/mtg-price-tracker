# MTG Price Tracker

Projeto de estudos desenvolvido para aprofundar conhecimentos em arquitetura de microsserviços utilizando .NET 9.

O objetivo é construir uma aplicação capaz de monitorar cartas de Magic: The Gathering, acompanhando preços, histórico de alterações e notificando usuários sobre variações de mercado.

O foco principal **não é apenas a funcionalidade**, mas a aplicação de boas práticas de arquitetura, mensageria e observabilidade.

---

## Objetivos de estudo

- ASP.NET Core Minimal API
- .NET 9
- Entity Framework Core
- PostgreSQL
- CQRS (manual)
- JWT Authentication
- Docker
- RabbitMQ
- Kafka
- Seq
- Vue 3 + TypeScript + Pinia

---

## Arquitetura

O projeto será dividido em microsserviços.

Atualmente:

```text
Vue
    │
    ▼
CardsService
    │
    ▼
PostgreSQL
```

Arquitetura planejada:

```text
                 Vue
                  │
                  ▼
             CardsService
                  │
       ┌──────────┴──────────┐
       ▼                     ▼
 RabbitMQ               PostgreSQL
       │
       ▼
 PriceMonitorService
       │
       ▼
      Kafka
       │
       ▼
 HistoryService
```

---

## Funcionalidades implementadas

- Pesquisa de cartas via Scryfall
- Cadastro de usuários
- Login com JWT
- Hash de senha
- Monitoramento de cartas
- Quantidade por carta
- Frontend em Vue
- Pinia para autenticação

---

## Roadmap

Consulte:

- docs/roadmap.md

---

## Como executar

### Infraestrutura

```bash
docker compose up -d
```

### Backend

```bash
cd src/Services/CardsService

dotnet ef database update

dotnet run
```

### Frontend

```bash
cd frontend

npm install

npm run dev
```

---

## Tecnologias

Backend

- .NET 9
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT

Infra

- Docker Compose
- RabbitMQ
- Kafka
- Seq

Frontend

- Vue 3
- TypeScript
- Pinia
- Axios

---

## Licença

Projeto desenvolvido exclusivamente para fins de estudo.