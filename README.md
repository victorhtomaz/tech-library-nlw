# TechLibrary
Uma aplicação backend construida durante o evento Next Level Week Connect, para gerenciamento de uma biblioteca.

## Tecnologias
- C#
- Entity Framework
- Sql Lite
- Jwt e Bearer

## Como usar

### Requisitos

- [.NET 9.0](https://dotnet.microsoft.com/pt-br/)

### Clonar o repositorio


```bash
git clone https://github.com/victorhtomaz/tech-library-nlw.git TechLibrary
```

### Inicializando

```bash
cd TechLibrary
dotnet build
dotnet run --project TechLibrary.Api
```

## API Endpoints

| Rotas                            | Descrição                                          | Requer autorização (Token) |
|----------------------------------|----------------------------------------------------|----------------------------|
| <kbd>POST v1/users</kbd>         | Faz o registro de um usuário                       | Não                        |
| <kbd>POST v1/login</kbd>         | Faz login para acesso                              | Não                        |
| <kbd>GET v1/books/filter</kbd>   | Lista os livros cadastrados, podendo ser usado filtros | Não                        |
| <kbd>POST v1/checkouts/{bookId}</kbd> | Faz um empréstimo de um livro para um usuário      | Sim                        |
| <kbd>PATCH v1/checkouts/{bookId}</kbd> | Faz o retorno do livro                           | Sim                        |

## Alerta

Para endpoints que precissam de login é necessário utilizar o token gerado.

## Exemplos

### ✅ POST v1/users

**REQUEST**
```json
{
  "name": "teste",
  "email": "teste@gmail.com",
  "password": "SenhaForte"
}
```
**RESPONSE**
```json
{
    "name": "teste",
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA2OTQ2NzNlLTI1NDAtNDEwNi04Mjk0LWUxODM5ZjRhODA3YyIsInVuaXF1ZV9uYW1lIjoidGVzdGUiLCJuYmYiOjE3NDAwODY1NzIsImV4cCI6MTc0MDA5Mzc3MiwiaWF0IjoxNzQwMDg2NTcyfQ.UdmoVSuglIAwva5DsWb48AyS8oWwiIp8Dxl4l-U0rIw"
}
```
---

### ✅ POST v1/login

**REQUEST**
```json
{
  "email": "teste@gmail.com",
  "password": "SenhaForte"
}
```
**RESPONSE**
```json
{
    "name": "teste",
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA2OTQ2NzNlLTI1NDAtNDEwNi04Mjk0LWUxODM5ZjRhODA3YyIsInVuaXF1ZV9uYW1lIjoidGVzdGUiLCJuYmYiOjE3NDAwODY1NzIsImV4cCI6MTc0MDA5Mzc3MiwiaWF0IjoxNzQwMDg2NTcyfQ.UdmoVSuglIAwva5DsWb48AyS8oWwiIp8Dxl4l-U0rIw"
}
```
---

### ✅ GET /v1/books/filter?pageNumber=1&title=Algorithms

**RESPONSE**
```json
{
  "pagination": {
    "pageNumber": 1,
    "totalCount": 2
  },
  "books": [
    {
      "id": "5d7c189b-dab6-4677-9510-9cfa7a79b9e2",
      "title": "Algorithms",
      "author": "Robert Sedgewick"
    },
    {
      "id": "04c2904a-f65e-42eb-9f8a-02a1de98c009",
      "title": "Introduction to Algorithms",
      "author": "Thomas H. Cormen"
    }
  ]
}
```
---

### ✅ POST v1/checkouts/5d7c189b-dab6-4677-9510-9cfa7a79b9e2

**RESPONSE**
```json
{
    "checkoutDate": "2025-02-20T21:27:19.3813794Z",
    "expectedReturnDate": "2025-02-27T21:27:19.3814276Z",
    "returnedDate": null
}
```
---

### ✅ PATCH v1/checkouts/5d7c189b-dab6-4677-9510-9cfa7a79b9e2

**RESPONSE**
```json
{
    "checkoutDate": "2025-02-20T21:27:19.3813794",
    "expectedReturnDate": "2025-02-27T21:27:19.3814276",
    "returnedDate": "2025-02-20T21:29:34.002695Z"
}
```
---

### ❌ ERROR RESPONSE
```json
{
  "errors": [
    "Algo deu erro."
  ]
}
```
