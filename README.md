# Projeto TryBets

## Descrição
O projeto TryBets é uma aplicação de backend para um site de apostas, dividida em microsserviços. A aplicação permite o cadastro e login de usuários, visualização de times e partidas, registro de apostas e atualização dinâmica das odds.

## Microsserviços
A aplicação é composta pelos seguintes microsserviços:

1. **TryBets.Users**: Responsável pelo cadastro e login de usuários.
   - Porta: 5501
   - Rotas:
     - `POST /user/signup` (gera e retorna token)
     - `POST /user/login` (gera e retorna token)

2. **TryBets.Matches**: Responsável pela visualização de times e partidas.
   - Porta: 5502
   - Rotas:
     - `GET /team`
     - `GET /match/{finished}`

3. **TryBets.Bets**: Responsável pelo cadastro e visualização de apostas.
   - Porta: 5503
   - Rotas:
     - `POST /bet` (requer token)
     - `GET /bet/{BetId}` (requer token)

4. **TryBets.Odds**: Responsável pela atualização das odds de cada partida.
   - Porta: 5504
   - Rotas:
     - `PATCH /odd/{matchId}/{TeamId}/{BetValue}`

## Tecnologias Utilizadas
- **Linguagem de Programação**: C#
- **Framework**: .NET
- **Banco de Dados**: SQL Server
- **Contêineres**: Docker
- **Orquestração de Contêineres**: Docker Compose
- **Autenticação**: JWT

## Instalação

### Pré-requisitos
- Docker
- Docker Compose
- .NET SDK

### Passos para instalar
1. Clone o repositório:
   ```sh
   git clone git@github.com:AmsBarros/trybets.git
   cd trybets/

2. Instale as dependências:
   ```sh
   cd src/
   dotnet restore

## Como rodar

### Banco de Dados
1. Suba o banco de dados com Docker Compose:
   ```sh
   docker-compose up -d --build

2. Para se conectar ao seu sistema de gerenciamento de banco de dados, utilize as seguintes credenciais:
- **Server**: localhost
- **User**: sa
- **Password**: TryBets123456!
- **Trust server certificate**: true

### Monolito
1. Rode a aplicação monolítica:
   ```sh
   cd src/
   dotnet run

### Microsserviços
1. Para cada microsserviço, entre na respectiva pasta e rode o serviço:
   ```sh
   cd src/TryBets.<Microsserviço>
   dotnet run

Substitua `<Microsserviço>`por `Users`, `Matches`, `Bets` ou `Odds`.   