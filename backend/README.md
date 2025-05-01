# 🧠 Sales Management API (.NET 8)

API responsável por gerenciar operações de vendas, aplicando regras de negócio específicas e publicando eventos em arquitetura orientada a eventos com RabbitMQ.

## ⚙️ Tecnologias Principais

- **.NET 8**, **Entity Framework Core**
- **PostgreSQL**, **RabbitMQ**
- **MediatR**, **AutoMapper**
- **xUnit** (testes), **Swagger/OpenAPI**
- **Docker & Docker Compose**

## 🚀 Funcionalidades

- CRUD completo de vendas
- Regras de desconto por quantidade:
  - 4–9 itens: 10%
  - 10–20 itens: 20%
  - +20 itens: não permitido
- Publicação de eventos:
  - `SaleCreated`, `SaleModified`, `SaleCancelled`, `ItemCancelled`

## ▶️ Executando o Backend

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/wesleyheron/Ambev.DeveloperEvaluation.git
   cd backend

2. **Start the services**:
  ```bash
  docker-compose up --build

## 🌐 Access Points

1. API (Swagger UI): http://localhost:8080/swagger/index.html

2. RabbitMQ Management UI: http://localhost:15672
Username: guest | Password: guest

## ✅ Running Tests
  ```bash
  dotnet test
