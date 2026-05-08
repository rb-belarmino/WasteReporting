# language: pt-br
Funcionalidade: Gestão Cadastral ESG
  Para permitir a gestão de resíduos, administradores devem cadastrar pontos de coleta, recicladores, destinos finais e tipos de resíduos.

Cenário: Cadastrar Ponto de Coleta
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "Location": "Avenida Paulista, 1000",
      "Responsible": "Prefeitura"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/collection-points"
    Então o status code deve ser 201

Cenário: Cadastrar Reciclador
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "Name": "Recicla Mais",
      "Category": "Cooperativa"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/recyclers"
    Então o status code deve ser 201

Cenário: Cadastrar Destino Final
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "Description": "Aterro Sanitário Municipal"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/final-destinations"
    Então o status code deve ser 201

Cenário: Cadastrar e Listar Tipos de Resíduos
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "Type": "Plástico"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/wastes"
    Então o status code deve ser 201
    E que eu não tenho payload
    Quando eu enviar um GET para o endpoint "/api/wastes"
    Então o status code deve ser 200
