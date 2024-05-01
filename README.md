# Gerenciador Cinema API

## Descrição
Este projeto implementa uma API RESTful usando .NET 7, projetada para gerenciar a exibição de filmes em um cinema. A API utiliza uma arquitetura em camadas e é capaz de manipular informações de salas e filmes, onde uma sala pode conter vários filmes, e um filme pode existir sem estar associado a uma sala específica.

## Características
 - Gestão de Salas: Adicione, atualize, delete e consulte salas de cinema.
    - Atributos de Sala: Número da Sala, Descrição.
 - Gestão de Filmes: Adicione, atualize, delete e consulte filmes.
    - Atributos de Filme: Nome, Diretor, Duração.
 - Relacionamento: Uma sala pode hospedar vários filmes; um filme pode ser independente de uma sala.

## Tecnologias Utilizadas
- ASP.NET Core 7
- Entity Framework Core para acesso a dados.
- MySQL como sistema de gerenciamento de banco de dados.
- Swagger para documentação e teste da API.
- Docker e Docker Compose para containerização e orquestração.

## Funcionalidades Extras
- Docker: Containerização da aplicação e do banco de dados.
- Docker Compose: Orquestração dos containers para desenvolvimento e produção.
- Paginação Dinâmica: Suporte para consultas paginadas, melhorando a performance em grandes conjuntos de dados.

## Testes
Implementação de testes unitários cobrindo:

Controllers
Camada de Serviço


## Como Usar
Clonar o Repositório

git clone https://github.com/migueljeffersongit/gerenciador-cinema.git

#### Executar com Docker Compose

docker-compose up --build

## Boas Práticas
A API foi projetada para aderir estritamente aos códigos de status HTTP apropriados, proporcionando uma resposta consistente e previsível para os consumidores da API.

## Documentação
Acesse a documentação interativa da API e teste os endpoints utilizando Swagger UI, que pode ser acessado navegando até http://localhost:porta/swagger após iniciar o aplicativo.