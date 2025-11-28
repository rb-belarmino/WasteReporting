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
*   **Rota**: `POST /api/reports`
*   **Descrição**: Cria uma nova denúncia vinculada ao usuário logado.
*   **Body (JSON)**:
    ```json
    {
      "location": "Rua das Flores, 123 - Centro",
      "description": "Entulho acumulado na calçada atrapalhando a passagem."
    }
    ```
*   **Retorno Esperado (201 Created)**:
    ```json
    {
      "id": 1,
      "location": "Rua das Flores, 123 - Centro",
      "description": "Entulho acumulado na calçada atrapalhando a passagem.",
      "status": "PENDENTE",
      "createdAt": "2024-11-24T22:00:00Z",
      "userName": "usuario_teste"
    }
    ```

### 5. Listar Minhas Denúncias
*   **Rota**: `GET /api/reports/my-reports`
*   **Parâmetros (Opcionais)**: `page=1`, `pageSize=10`
*   **Descrição**: Lista apenas as denúncias feitas por você.
*   **Retorno Esperado (200 OK)**: Lista de denúncias (JSON Array).

### 6. Listar Todas (Apenas Admin)
*   **Rota**: `GET /api/reports`
*   **Descrição**: Lista denúncias de *todos* os usuários. Requer usuário com `Role = "Admin"`.

### 7. Atualizar Status (Apenas Admin)
*   **Rota**: `PUT /api/reports/{id}/status`
*   **Body (JSON - String)**: `"RESOLVIDO"`
*   **Descrição**: Atualiza o status de uma denúncia específica.

## 📦 Outros Endpoints (Gestão de Resíduos)

### 8. Pontos de Coleta
*   **Rota**: `POST /api/collection-points`
*   **Descrição**: Cadastra um novo ponto de coleta.
*   **Body (JSON)**:
    ```json
    {
      "location": "Av. Principal, 500",
      "responsible": "Maria Silva"
    }
    ```

### 9. Recicladores
*   **Rota**: `POST /api/recyclers`
*   **Descrição**: Cadastra um novo reciclador parceiro.
*   **Body (JSON)**:
    ```json
    {
      "name": "Recicla Mais",
      "category": "Plástico/Papel"
    }
    ```

### 10. Destinos Finais
*   **Rota**: `POST /api/final-destinations`
*   **Descrição**: Cadastra um local de destino final (ex: Aterro).
*   **Body (JSON)**:
    ```json
    {
      "description": "Aterro Sanitário Municipal"
    }
    ```

### 11. Tipos de Resíduos
*   **Rota**: `POST /api/wastes` (Criar)
*   **Rota**: `GET /api/wastes` (Listar)
*   **Parâmetros (Opcionais)**: `page=1`, `pageSize=10`
*   **Descrição**: Gerencia os tipos de resíduos aceitos.
*   **Body (Criar)**:
    ```json
    {
      "type": "Eletrônico"
    }
    ```

### 12. Coletas
*   **Rota**: `POST /api/collections` (Agendar)
*   **Rota**: `GET /api/collections` (Listar)
*   **Parâmetros (Opcionais)**: `page=1`, `pageSize=10`
*   **Rota**: `GET /api/collections/{id}` (Detalhes)
*   **Rota**: `PUT /api/collections/{id}` (Atualizar)
*   **Rota**: `DELETE /api/collections/{id}` (Remover)
*   **Descrição**: Gerencia o agendamento e execução de coletas.
*   **Body (Agendar)**:
    ```json
    {
      "collectionPointId": 1,
      "recyclerId": 1, // Opcional
      "finalDestinationId": null, // Opcional
      "collectionDate": "2024-12-01T10:00:00Z"
    }
    ```

### 13. Associação Coleta-Resíduo
*   **Rota**: `POST /api/collection-wastes` (Associar)
*   **Rota**: `DELETE /api/collection-wastes/{collectionId}/{wasteId}` (Desassociar)
*   **Descrição**: Vincula tipos de resíduos e pesos a uma coleta específica.
*   **Body (Associar)**:
    ```json
    {
      "collectionId": 1,
      "wasteId": 2,
      "weightKg": 50.5
    }
    ```

## 🏗️ Estrutura do Projeto
*   **Controllers**: Endpoints da API.
*   **Services**: Regras de negócio.
*   **ViewModels**: Modelos de visualização para transferência de dados (MVVM).
*   **Models**: Entidades do banco de dados.
*   **Data**: Contexto do Entity Framework.
