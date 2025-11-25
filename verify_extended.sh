#!/bin/bash

# 1. Login
echo "Logging in..."
TOKEN=$(curl -s -X POST http://localhost:5001/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "test_verify@email.com", "password": "Password123!"}' | grep -o '"token":"[^"]*' | cut -d'"' -f4)

echo "Token: $TOKEN"

if [ -z "$TOKEN" ]; then
    echo "Failed to get token"
    exit 1
fi

# 2. Create Ponto Coleta
echo "Creating Ponto Coleta..."
PONTO=$(curl -s -X POST http://localhost:5001/api/pontos-coleta \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"localizacao": "Ponto A", "responsavel": "Resp A"}')
echo "Ponto: $PONTO"

# 3. Create Reciclador
echo "Creating Reciclador..."
RECICLADOR=$(curl -s -X POST http://localhost:5001/api/recicladores \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"nome": "Reciclador X", "categoria": "Cat X"}')
echo "Reciclador: $RECICLADOR"

# 4. Create Destino Final
echo "Creating Destino Final..."
DESTINO=$(curl -s -X POST http://localhost:5001/api/destinos-finais \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"descricao": "Aterro Y"}')
echo "Destino: $DESTINO"

# 5. Create Residuo
echo "Creating Residuo..."
RESIDUO=$(curl -s -X POST http://localhost:5001/api/residuos \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"tipo": "Plastico"}')
echo "Residuo: $RESIDUO"

# Extract IDs
PONTO_ID=$(echo $PONTO | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)
RECICLADOR_ID=$(echo $RECICLADOR | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)
DESTINO_ID=$(echo $DESTINO | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)
RESIDUO_ID=$(echo $RESIDUO | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)

echo "IDs: Ponto=$PONTO_ID, Reciclador=$RECICLADOR_ID, Destino=$DESTINO_ID, Residuo=$RESIDUO_ID"

# 6. Create Coleta
echo "Creating Coleta..."
COLETA=$(curl -s -X POST http://localhost:5001/api/coletas \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "{\"pontoColetaId\": $PONTO_ID, \"recicladorId\": $RECICLADOR_ID, \"destinoFinalId\": $DESTINO_ID, \"dataColeta\": \"2025-12-01T10:00:00\", \"status\": \"AGENDADA\"}")
echo "Coleta: $COLETA"

COLETA_ID=$(echo $COLETA | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)
echo "Coleta ID: $COLETA_ID"

# 7. Associate Residuo
echo "Associating Residuo..."
curl -s -X POST http://localhost:5001/api/coleta-residuos \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "{\"coletaId\": $COLETA_ID, \"residuoId\": $RESIDUO_ID, \"pesoKg\": 50.5}"

# 8. List Coletas
echo "Listing Coletas..."
curl -s -X GET http://localhost:5001/api/coletas \
  -H "Authorization: Bearer $TOKEN"
