# language: pt-br
Funcionalidade: Gestão de Coletas e Relacionamentos
  Para fechar o ciclo de vida do resíduo, precisamos agendar coletas e associar resíduos.

Cenário: Agendar e Listar Coletas
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "CollectionPointId": 1,
      "CollectionDate": "2026-10-10T10:00:00Z",
      "RecyclerId": 1,
      "FinalDestinationId": 1,
      "Status": "AGENDADA"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/collections"
    Então o status code deve ser 201
    E que eu não tenho payload
    Quando eu enviar um GET para o endpoint "/api/collections"
    Então o status code deve ser 200

Cenário: Atualizar e Deletar Coleta
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "CollectionPointId": 1,
      "CollectionDate": "2026-10-11T10:00:00Z",
      "RecyclerId": 1,
      "FinalDestinationId": 1,
      "Status": "CONCLUIDA"
    }
    """
    Quando eu enviar um PUT para o endpoint "/api/collections/1"
    Então o status code deve ser 200
    E que eu não tenho payload
    Quando eu enviar um DELETE para o endpoint "/api/collections/1"
    Então o status code deve ser 204

Cenário: Associar Resíduo à Coleta
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    {
      "CollectionPointId": 1,
      "CollectionDate": "2026-10-10T10:00:00Z",
      "RecyclerId": 1,
      "FinalDestinationId": 1,
      "Status": "AGENDADA"
    }
    """
    Quando eu enviar um POST para o endpoint "/api/collections"
    E que eu tenho um payload JSON:
    """
    {
      "CollectionId": 2,
      "WasteId": 1,
      "WeightKg": 50.5
    }
    """
    Quando eu enviar um POST para o endpoint "/api/collection-wastes"
    Então o status code deve ser 200
    E que eu não tenho payload
    Quando eu enviar um GET para o endpoint "/api/collection-wastes"
    Então o status code deve ser 200
    Quando eu enviar um DELETE para o endpoint "/api/collection-wastes/2/1"
    Então o status code deve ser 204
