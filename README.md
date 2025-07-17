# 🏋️‍♂️ AcademiaFinal

Projeto completo de uma API REST para gestão de academias, desenvolvido em C# com .NET 8, utilizando PostgreSQL como banco de dados relacional. Este sistema tem como objetivo gerenciar clientes, professores, planos, fichas de treino e equipamentos de uma academia.

## 📌 Objetivo do Projeto

Este é um projeto autoral, feito com o intuito de praticar e consolidar conhecimentos em desenvolvimento back-end utilizando as tecnologias e boas práticas do mercado. Apesar de ainda não ter experiência profissional como desenvolvedor, este projeto representa minha dedicação, estudo e aplicação prática de conceitos fundamentais do desenvolvimento de APIs modernas.

---

## 🚀 Tecnologias Utilizadas

- ✅ **C# .NET 8**
- ✅ **Entity Framework Core**
- ✅ **PostgreSQL**
- ✅ **Swagger/OpenAPI**
- ✅ **DTOs (Data Transfer Objects)**
- ✅ **FluentValidation**
- ✅ **AutoMapper**
- ✅ **Autenticação com JWT**
- ✅ **Clean Code (estrutura em camadas)**

---

## 🔐 Autenticação

O sistema conta com autenticação via **JWT (JSON Web Token)** para proteger endpoints e garantir que apenas usuários autenticados possam acessar os recursos da API. A autenticação é feita via login e retorno de um token que deve ser enviado no header `Authorization`.

> 🔸 Não há controle de permissões (roles) neste projeto, apenas autenticação simples com JWT.

---

## 🧱 Funcionalidades da API

### 👤 Usuários
- Cadastro de novos usuários
- Login com autenticação JWT
- Consulta de dados do usuário autenticado

### 🧑‍💼 Professores
- Cadastro de professores vinculados a usuários
- Listagem e consulta por ID

### 👥 Clientes
- Cadastro de clientes vinculados a usuários
- Vinculação de planos aos clientes
- Listagem e consulta por ID

### 📅 Planos
- Cadastro de planos (mensal, trimestral, etc.)
- Associação com clientes
- Controle de data de início e fim de plano

### 🏋️ Equipamentos
- Cadastro de equipamentos da academia
- Listagem geral

### 📋 Fichas de Treino
- Criação de fichas associadas a clientes e professores
- Inclusão de equipamentos e observações por exercício

---

## 🧭 Estrutura de Pastas

AcademiaFinal/
│
├── Controllers # Camada de entrada da API (endpoints)
├── Domain # Entidades e regras de domínio
├── DTOs # Objetos de transferência de dados (Request/Response)
├── Infrastructure # Contexto de banco, mapeamento EF Core
├── Repositories # implementações dos repositórios
├── Services # Camada de regra de negócio (Services)
├── Validators # Validações com FluentValidation
└── Program.cs # Arquivo principal para configurar o app


---

## 🛠️ Como Executar o Projeto

### ✅ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/)
- IDE como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

### ⚙️ Configuração

1. Clone o repositório:
git clone https://github.com/PedroMachado2409/AcademiaFinal.git

2. Configure o arquivo appsettings.json com sua string de conexão do PostgreSQL:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=AcademiaDb;Username=seu_usuario;Password=sua_senha"
}

3.Execute as migrations para criar as tabelas no banco: dotnet ef database update

🔍 Exemplos de Endpoints
Autenticação (Login)
POST /auth/login
Body: {
  "email": "usuario@email.com",
  "senha": "123456"
}
Cadastro de Cliente
POST /api/clientes
Authorization: Bearer {token}
Body: {
  "nome": "João",
  "cpf": "12345678900",
  ...
}

💡 Possíveis Melhorias Futuras
Implementação de controle de permissões (roles: ADMIN, PROFESSOR, CLIENTE)

Testes automatizados com xUnit

Upload de imagem de perfil do usuário

Deploy em ambiente público (Render, Railway ou Azure)

Frontend com React/Chakra UI
