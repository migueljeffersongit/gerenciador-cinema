# Gerenciador Cinema API 🎬🍿🥤
[![.NET](https://github.com/migueljeffersongit/gerenciador-cinema/actions/workflows/dotnet.yml/badge.svg)](https://github.com/migueljeffersongit/gerenciador-cinema/actions/workflows/dotnet.yml)
## Descrição
Este projeto implementa uma API RESTful usando .NET 7, projetada para gerenciar a exibição de filmes em um cinema. A API utiliza uma arquitetura em camadas e é capaz de manipular informações de salas e filmes, onde uma sala pode conter vários filmes, e um filme pode existir sem estar associado a uma sala específica.

## Características
 - Gestão de Salas: Adicione, atualize, delete e consulte salas de cinema.
    - Atributos de Sala: Número da Sala, Descrição.
 - Gestão de Filmes: Adicione, atualize, delete e consulte filmes.
    - Atributos de Filme: Nome, Diretor, Duração, SalaId.
 - Relacionamento: Uma sala pode hospedar vários filmes; um filme pode ser independente de uma sala.

## Tecnologias Utilizadas
- ASP.NET Core 7
- Entity Framework Core.
- MySQL.
- Swagger para documentação e teste da API.
- Docker e Docker Compose para containerização e orquestração.

## Funcionalidades Extras
- Docker: Containerização da aplicação e do banco de dados.
- Docker Compose: Orquestração dos containers para desenvolvimento e produção.
- Paginação Dinâmica: Suporte para consultas paginadas, melhorando a performance em grandes conjuntos de dados.

## Testes
Implementação de testes unitários cobrindo:

- Controllers
- Camada de Serviço


## Como Usar
Clonar o Repositório

```
git clone https://github.com/migueljeffersongit/gerenciador-cinema.git
```

e acessar o diretório gerenciador-cinema

#### Executar com Docker Compose

```
docker-compose up -d --build
```

#### Parar com Docker Compose

```
docker-compose down -v
```

## Exemplos de Uso da API

### Criar uma Sala

Para criar uma nova sala no sistema, você deve enviar uma solicitação POST para o endpoint `/api/salas`. Aqui está um exemplo de como fazer essa solicitação com os dados da sala em formato JSON:

**Endpoint:**
POST /api/salas

**Exemplo de Corpo da Requisição:**
```json
{
  "numeroSala": "Sala 12",
  "descricao": "Sala de projeção 3D com capacidade para 200 espectadores, som surround e poltronas reclináveis."
}
```

```bash
curl -X POST "http://localhost:8000/api/salas" -H  "accept: */*" -H  "Content-Type: application/json" -d "{  \"numeroSala\": \"Sala 12\",  \"descricao\": \"Sala de projeção 3D com capacidade para 200 espectadores, som surround e poltronas reclináveis.\"}"
```
### Criar um Filme

Para adicionar um novo filme ao sistema, associando-o a uma sala existente, envie uma solicitação POST para o endpoint /api/filmes. Inclua os dados do filme em formato JSON como mostrado abaixo:

**Endpoint:**
POST /api/filmes

**Formato da Duração (`duracao`):**
A duração do filme deve ser passada no formato ISO 8601, que é representado pela letra `P`, seguida por um período `T`, as horas (`H`), os minutos (`M`) e opcionalmente os segundos (`S`). Por exemplo, `PT3H21M` representa 3 horas e 21 minutos.

**Exemplo de Corpo da Requisição:**
```json
{
  "nome": "O Senhor dos Anéis: O Retorno do Rei",
  "diretor": "Peter Jackson",
  "duracao": "PT3H21M",
  "salaId": "9401bbb8-9499-4a9e-9475-2e61f16cb336" //  ID Sala 2 criado inicialmente no migrations ou substitua por um id criado
}
```

```bash
curl -X POST "http://localhost:8000/api/filmes" -H  "accept: */*" -H  "Content-Type: application/json" -d "{  \"nome\": \"O Senhor dos Anéis: O Retorno do Rei\",  \"diretor\": \"Peter Jackson\",  \"duracao\": \"PT3H21M\",  \"salaId\": \"9401bbb8-9499-4a9e-9475-2e61f16cb336\"}"
```
Estes exemplos proporcionam informações detalhadas sobre como utilizar os endpoints da API para criar recursos importantes no sistema de gerenciamento de cinema. Eles são úteis para desenvolvedores integrarem ou testarem a API rapidamente.

## Boas Práticas
A API foi projetada para aderir estritamente aos códigos de status HTTP apropriados, proporcionando uma resposta consistente e previsível para os consumidores da API.

## Documentação
Acesse a documentação interativa da API e teste os endpoints utilizando Swagger UI, que pode ser acessado navegando até http://localhost:8000/swagger/index.html após iniciar o aplicativo.
