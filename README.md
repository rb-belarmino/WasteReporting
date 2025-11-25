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

## 🧪 Guia de Testes (Passo a Passo Detalhado)

### 1. Criar Conta (Registro)
*   **Rota**: `POST /auth/register`
*   **Descrição**: Cria um novo usuário no sistema.
*   **Body (JSON)**:
    ```json
    {
      "username": "usuario_teste",
      "email": "teste@email.com",
      "password": "123"
    }
    ```
*   **Retorno Esperado (201 Created)**:
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "username": "usuario_teste",
      "email": "teste@email.com",
      "role": "User"
    }
    ```

### 2. Login (Autenticação)
*   **Rota**: `POST /auth/login`
*   **Descrição**: Autentica o usuário e retorna o Token JWT.
*   **Body (JSON)**:
    ```json
    {
      "email": "teste@email.com",
      "password": "123"
    }
    ```
*   **Retorno Esperado (200 OK)**:
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "username": "usuario_teste",
      "email": "teste@email.com",
      "role": "User"
    }
    ```
    ⚠️ **Importante**: Copie o valor do `token` para usar nos próximos passos.

### 3. Configurar Autenticação (Swagger)
1.  No topo da página do Swagger, clique no botão **Authorize** (cadeado).
2.  No campo "Value", digite: `Bearer SEU_TOKEN_AQUI` (ex: `Bearer eyJhb...`).
3.  Clique em **Authorize** e depois em **Close**.

### 4. Criar Denúncia
*   **Rota**: `POST /api/denuncias`
*   **Descrição**: Cria uma nova denúncia vinculada ao usuário logado.
*   **Body (JSON)**:
    ```json
    {
      "localizacao": "Rua das Flores, 123 - Centro",
      "descricao": "Entulho acumulado na calçada atrapalhando a passagem."
    }
    ```
*   **Retorno Esperado (201 Created)**:
    ```json
    {
      "id": 1,
      "localizacao": "Rua das Flores, 123 - Centro",
      "descricao": "Entulho acumulado na calçada atrapalhando a passagem.",
      "status": "PENDENTE",
      "dataCriacao": "2024-11-24T22:00:00Z",
      "usuarioNome": "usuario_teste"
    }
    ```

### 5. Listar Minhas Denúncias
*   **Rota**: `GET /api/denuncias/minhas`
*   **Parâmetros (Opcionais)**: `page=1`, `pageSize=10`
*   **Descrição**: Lista apenas as denúncias feitas por você.
*   **Retorno Esperado (200 OK)**: Lista de denúncias (JSON Array).

### 6. Listar Todas (Apenas Admin)
*   **Rota**: `GET /api/denuncias`
*   **Descrição**: Lista denúncias de *todos* os usuários. Requer usuário com `Role = "Admin"`.

### 7. Atualizar Status (Apenas Admin)
*   **Rota**: `PUT /api/denuncias/{id}/status`
*   **Body (JSON - String)**: `"RESOLVIDO"`
*   **Descrição**: Atualiza o status de uma denúncia específica.

## 🏗️ Estrutura do Projeto
*   **Controllers**: Endpoints da API.
*   **Services**: Regras de negócio.
*   **DTOs**: Objetos de transferência de dados (ViewModel).
*   **Models**: Entidades do banco de dados.
*   **Data**: Contexto do Entity Framework.
