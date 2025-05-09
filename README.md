# Projeto Full Stack (.NET + Next.js)

Este repositório contém uma aplicação full stack composta por:

- **Back-end**: .NET (C#)
- **Front-end**: Next.js (React)

A aplicação simula uma carteira digital com as seguintes funcionalidades principais:

- 🔐 Autenticação de usuários
- 👤 Criação de usuário
- 💰 Consulta de saldo da carteira de um usuário
- ➕ Adição de saldo à carteira
- 🔁 Criação de transferências entre usuários (carteiras)
- 📄 Listagem de transferências realizadas por um usuário, com filtro opcional por período (data inicial e final)

## 👥 Usuários pré-cadastrados

Para facilitar os testes, o sistema já vem com os seguintes usuários cadastrados automaticamente (via seed):

| Nome            | E-mail                     | Senha    |
| --------------- | -------------------------- | -------- |
| Joaquim Silva   | joaquim@wallet.com         | 12345678 |
| Francisco Lima  | francisco@wallet.com       | 12345678 |
| Ana Beatriz     | ana.beatriz@wallet.com     | 12345678 |
| Carlos Souza    | carlos.souza@wallet.com    | 12345678 |
| Mariana Rocha   | mariana.rocha@wallet.com   | 12345678 |
| Pedro Henrique  | pedro.henrique@wallet.com  | 12345678 |
| Camila Duarte   | camila.duarte@wallet.com   | 12345678 |
| Lucas Mendes    | lucas.mendes@wallet.com    | 12345678 |
| Fernanda Costa  | fernanda.costa@wallet.com  | 12345678 |
| Rafael Oliveira | rafael.oliveira@wallet.com | 12345678 |

## 📁 Estrutura do Projeto

- `/BackEnd` → Aplicação .NET com arquitetura baseada em DDD (Domain-Driven Design)
- `/FrontEnd` → Aplicação Next.js

---

## ⚙️ Pré-requisitos

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)
- [Node.js 24+](https://nodejs.org/)
- [Yarn](https://classic.yarnpkg.com/) ou npm
- [Docker](https://www.docker.com/) (opcional, mas recomendado)

---

## 🚀 Como rodar o projeto

### 🐳 Docker

A aplicação foi totalmente configurada para rodar com Docker. Se você já tem o Docker instalado, basta executar o comando abaixo na raiz do projeto:

1. Para rodar com os logs no terminal:

   ```bash
   docker-compose up
   ```

1. Para rodar em segundo plano (sem logs no terminal):

   ```bash
   docker-compose up -d
   ```

A aplicação estará disponível em: http://localhost:3000 e o back-end (Swagger) em: http://localhost:5124/index.html

se não siga os passo abaixo.

### 🌐 Front-end (Next.js)

1. Acesse a pasta do front-end:

   ```bash
   cd FrontEnd
   ```

2. Crie o arquivo `.env.local` na raiz com o seguinte conteúdo:

   ```bash
   NEXT_PUBLIC_API_URL=https://localhost:7050/api/
   NEXT_PUBLIC_API_URL_SERVER=http://localhost:5124/api/
   NEXTAUTH_SECRET=testedsaddad
   ```

Observações:

- A variável `NEXTAUTH_SECRET` pode ser qualquer valor.

- As variáveis `NEXT_PUBLIC_API_URL` e `NEXT_PUBLIC_API_URL_SERVER` devem corresponder aos endpoints configurados no back-end. Só altere se você mudar as portas da API.

3. Instale as dependências:

   ```bash
   yarn install
   ```

   ou

   ```bash
   npm install
   ```

4. Rode o front-end:

   ```bash
   yarn install
   ```

   ou

   ```bash
   npm install
   ```

A aplicação estará disponível em: http://localhost:3000

### 🖥️ Back-end (.NET)

1. Acesse a pasta do back-end:

   ```bash
   cd BackEnd
   ```

2. Crie um banco de dados PostgreSQL (localmente ou em um provedor externo) e configure a string de conexão no arquivo `appsettings.json` dentro da pasta `Wallet.API`:

```bash
   "ConnectionStrings": {
        "DatabaseConnection": "Host=localhost;Port=5432;Database=wallet-challenges;Username=root;Password=root"
    }
```

Explicação da string de conexão:

- Host: endereço do servidor PostgreSQL (ex: localhost, 127.0.0.1, ou URL do provedor)

- Port: porta do PostgreSQL (padrão é 5432)

- Database: nome do banco de dados (ex: wallet-challenges)

- Username: nome do usuário do banco (ex: root)

- Password: senha do banco (ex: root)

2. Restaure os pacotes:

   ```bash
   dotnet restore
   ```

3. Rode a aplicação:
   ```bash
   dotnet run
   ```

Por padrão, a API estará disponível em:

- HTTPS: https://localhost:7050

- HTTP: http://localhost:5124
