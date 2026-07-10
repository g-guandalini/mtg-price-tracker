# Arquitetura

## Objetivo

Este projeto tem como objetivo estudar arquitetura moderna utilizando .NET, separando responsabilidades e preparando a aplicação para evolução em microsserviços.

---

## Organização

Cada funcionalidade possui sua própria estrutura:

```text
Application
├── Commands
├── Queries
├── Handlers
└── DTOs
```

Essa separação segue o padrão CQRS.

---

## CardsService

Responsável por:

- pesquisa de cartas
- autenticação
- monitoramento

---

## Banco

```text
User

↓

TrackedCard

↓

Card
```

Uma carta é cadastrada apenas uma vez.

O usuário monitora a carta através da entidade TrackedCard.

---

## Próxima evolução

Separar autenticação e monitoramento de preços em microsserviços independentes.

---

## Mensageria

RabbitMQ

Utilizado para comunicação entre serviços.

Kafka

Utilizado para armazenamento de eventos de alteração de preço.

Seq

Centralização de logs.