# Projeto - Cidades ESG Inteligentes (Waste Reporting API)

API RESTful desenvolvida em .NET 9 para gestão de denúncias de descarte irregular de resíduos, visando promover cidades mais sustentáveis (ESG).

## 🚀 Tecnologias Utilizadas

*   **.NET 9** (Web API)
*   **Oracle Database** (Entity Framework Core)
*   **JWT** (JSON Web Token) para Autenticação
*   **xUnit + Moq** para Testes Unitários
*   **Docker & Docker Compose** para Containerização e Orquestração
*   **GitHub Actions** para Pipeline CI/CD
*   **Azure Web App** para Hospedagem em Staging e Produção

## 🐳 Como executar localmente com Docker

Para subir a aplicação e suas dependências localmente, siga os passos abaixo:

1.  Certifique-se de ter o **Docker** e o **Docker Compose** instalados.
2.  Clone o repositório.
3.  (Opcional) Ajuste as variáveis de ambiente no `docker-compose.yml` ou crie um arquivo `.env` baseado no `.env.example`.
4.  Execute o comando na raiz do projeto:
    ```bash
    docker-compose up --build
    ```
5.  A API estará disponível em: `http://localhost:5001/swagger`

## 🚀 Pipeline CI/CD

O projeto utiliza **GitHub Actions** para automação de processos, dividido em dois workflows principais:

*   **CI (Integração Contínua)**: 
    - Executado em cada Pull Request para a branch `main`.
    - Realiza o **Build** da solução.
    - Executa os **Testes Automatizados** (xUnit).
    - Gera os artefatos de publicação (Publish).
*   **CD (Entrega Contínua)**:
    - Executado em pushes para as branches `main` (Produção) e `develop` (Staging).
    - Chama o workflow de CI para garantir a integridade.
    - Realiza o **Deploy** automático para o **Azure Web App**.

## 📦 Containerização

A aplicação foi containerizada utilizando uma estratégia de **Multi-stage Build** no `Dockerfile`, o que reduz o tamanho final da imagem e aumenta a segurança ao não incluir o SDK no ambiente de execução.

**Conteúdo do Dockerfile:**
- **Base**: Utiliza a imagem `aspnet:9.0` para o runtime.
- **Build**: Utiliza a imagem `sdk:9.0` para compilar o código e restaurar dependências.
- **Publish**: Gera os binários otimizados.
- **Final**: Copia apenas os binários para a imagem de runtime.

A orquestração via **Docker Compose** permite configurar variáveis de ambiente, portas de rede e volumes de forma centralizada.

## 📸 Prints do funcionamento

*Os prints devem ser incluídos aqui pelo usuário após a execução:*
- [ ] Print do Build/Testes passando no GitHub Actions.
- [ ] Print do Deploy realizado com sucesso no Azure.
- [ ] Print da API rodando no ambiente de Staging.
- [ ] Print da API rodando no ambiente de Produção.

## 📁 Estrutura do Projeto

```text
/
├── .github/workflows/  # Configurações do GitHub Actions (CI/CD)
├── src/                # Código-fonte do projeto
│   ├── WasteReporting.API/     # Projeto principal da API
│   └── WasteReporting.Tests/   # Projeto de testes automatizados
├── .env.example        # Exemplo de variáveis de ambiente
├── Dockerfile          # Configuração de containerização da API
├── docker-compose.yml  # Orquestração de serviços
└── WasteReporting.sln  # Solução do Visual Studio
```

## ✅ Checklist de Entrega

| Item | OK |
| :--- | :---: |
| Projeto compactado em .ZIP com estrutura organizada | ☐ |
| Dockerfile funcional | ✅ |
| docker-compose.yml ou arquivos Kubernetes | ✅ |
| Pipeline com etapas de build, teste e deploy | ✅ |
| README.md com instruções e prints | ✅ |
| Documentação técnica com evidências (PDF ou PPT) | ☐ |
| Deploy realizado nos ambientes staging e produção | ☐ |
