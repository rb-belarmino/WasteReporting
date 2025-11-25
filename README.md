# Waste Reporting API ("Denúncia de Entulho")

API RESTful desenvolvida em .NET 9 para gestão de denúncias de descarte irregular de resíduos. O projeto segue boas práticas de arquitetura, segurança e escalabilidade.

## 🚀 Tecnologias Utilizadas

*   **.NET 9** (Web API)
*   **Oracle Database** (Entity Framework Core)
*   **JWT** (JSON Web Token) para Autenticação
*   **xUnit + Moq** para Testes Unitários
*   **Docker** para Containerização

## ✨ Funcionalidades

### 1. Autenticação (JWT)
*   **Registro**: Criação de conta com senha criptografada (BCrypt).
*   **Login**: Geração de Token JWT para acesso seguro.

### 2. Denúncias (Cidadão)
*   **Criar Denúncia**: Reportar um problema com localização e descrição.
*   **Minhas Denúncias**: Listar denúncias do usuário logado (com paginação).

### 3. Gestão (Admin)
*   **Listar Todas**: Visão geral de todas as denúncias (com paginação).
*   **Atualizar Status**: Alterar status (ex: PENDENTE -> RESOLVIDO).

## 🛠️ Como Rodar

### Opção 1: Docker (Recomendado)
Basta ter o Docker instalado e rodar:
```bash
docker-compose up --build
```
A API estará disponível em: `http://localhost:5001/swagger`

### Opção 2: Local (.NET CLI)
1.  **Rodar a API**:
    ```bash
    dotnet run --project WasteReporting.API/WasteReporting.API.csproj
    ```
    Acesse: `http://localhost:5169/swagger`

2.  **Rodar Testes**:
    ```bash
    dotnet test
    ```

## 🧪 Como Testar (Passo a Passo)

### 1. Criar Conta
*   **POST** `/auth/register`
*   Body: `{"username": "teste", "email": "teste@email.com", "password": "123"}`

### 2. Login
*   **POST** `/auth/login`
*   Body: `{"email": "teste@email.com", "password": "123"}`
*   **Copie o Token** retornado.

### 3. Autenticar no Swagger
*   Clique no cadeado (**Authorize**).
*   Digite: `Bearer SEU_TOKEN_AQUI`.

### 4. Usar a API
*   **POST** `/api/denuncias`: Criar denúncia.
*   **GET** `/api/denuncias/minhas?page=1&pageSize=10`: Listar suas denúncias.

## 🏗️ Estrutura do Projeto
*   **Controllers**: Endpoints da API.
*   **Services**: Regras de negócio.
*   **DTOs**: Objetos de transferência de dados (ViewModel).
*   **Models**: Entidades do banco de dados.
*   **Data**: Contexto do Entity Framework.
