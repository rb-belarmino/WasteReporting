# language: pt-br
Funcionalidade: Monitoramento de Impacto Ambiental
  Para promover a gestão de resíduos, cidadãos devem registrar denúncias de descarte.

Cenário: Criar um relato de descarte irregular
    Dado que eu estou autenticado como "cidadao@esg.com" com senha "SenhaCidadao123!"
    E que eu tenho um payload de relato com localização "Rua Central, 100" e descrição "Descarte de entulho"
    Quando eu enviar um POST para o endpoint "/api/reports"
    Então o status code deve ser 201
    E o corpo da resposta deve validar o schema JSON "ReportResponseSchema.json"

Cenário: Listar meus relatos
    Dado que eu estou autenticado como "cidadao@esg.com" com senha "SenhaCidadao123!"
    Quando eu enviar um GET para o endpoint "/api/reports/my-reports"
    Então o status code deve ser 200
Cenário: Listar todos os relatos (Admin)
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    Quando eu enviar um GET para o endpoint "/api/reports"
    Então o status code deve ser 200

Cenário: Atualizar status do relato (Admin)
    Dado que eu estou autenticado como "cidadao@esg.com" com senha "SenhaCidadao123!"
    E que eu tenho um payload de relato com localização "Rua Central, 100" e descrição "Descarte"
    Quando eu enviar um POST para o endpoint "/api/reports"
    Então o status code deve ser 201
    Dado que eu estou autenticado como "admin@esg.com" com senha "SenhaForte123!"
    E que eu tenho um payload JSON:
    """
    "RESOLVIDO"
    """
    Quando eu enviar um PUT para o endpoint "/api/reports/1/status"
    Então o status code deve ser 200
