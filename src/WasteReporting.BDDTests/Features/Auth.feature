# language: pt-br
Funcionalidade: Autenticação de Usuário e Segurança ESG
  Para garantir a governança e segurança dos dados, apenas usuários autenticados devem acessar o sistema.

Cenário: Login com credenciais válidas
    Dado que eu tenho um payload de login com email "admin@esg.com" e senha "SenhaForte123!"
    Quando eu consultar o endpoint de auth "/auth/login"
    Então o status code deve ser 200
    E o corpo da resposta deve validar o schema JSON "AuthResponseSchema.json"

Cenário: Login com credenciais inválidas
    Dado que eu tenho um payload de login com email "errado@esg.com" e senha "errado123"
    Quando eu consultar o endpoint de auth "/auth/login"
    Então o status code deve ser 401
