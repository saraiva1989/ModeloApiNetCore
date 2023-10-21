# ModeloApiNetCore

## Objetivo

Esse é um modelo de projeto que usa .net core 6 e já possui configuração de autenticação com o banco de dados mongo db.

Caso queira usar esse projeto recomendo que faça duas coisas:

1. No AppSettings coloque sua conexão com o banco de dados (MONGO)

```json
"ConnectionStrings": {
"BancoProd": "SeuBancoDeDadosMongo",
"BancoDev": "SeuBancoDadosMongoDev"
},
```

2. Na classe **criptografia** altere a **key** para sua criptografia, essa key serve para criptografar os dados sensiveis (Nesse caso o email do usuario), pois caso haja algum vazamento de dados o EMAIL será criptografado. Para criptografar outro campo basta usar os metodos EncryptString(string plainText) e para decriptar use o DecryptString(string cipherText).

Feito isso já é possivel usar o projeto no qual faz o pode se cadastrar e logar.

O projeto possui dois middleware (um de autenticação e um de tratamento de erro).

### Cadastro

Para cadastrar use o endpoint Cadastro (api/Usuario/cadastrar) e os campos Nome, E-mail, e Senha. Abaixo o exemplo de envio e resposta.

ENVIO

```json
{
    "nome": "jose",
    "email": "jose.aa@gmail.com",
    "senha": "abc123"
}
```

RESPOSTA

```json
{
    "status": true, //Se true significa que o cadastro foi realizado com sucesso.
    "id": 0,
    "idMongo": "",
    "mensagem": "Cadastro realizado com sucesso. ",
    "exception": "",
    "url": "",
    "objeto": {}
}
```

---

Caso ocorra algum erro o status será false, na mensagem terá o retorno do erro.

### Login

Para logar (gerar o token) use o endpoint (api/Usuario/autenticar). Abaixo exemplo de envio e resposta:

ENVIO

```json
{
  "email": "jose.aa@gmail.com",
  "senha": "abc123"
}
```

RESPOSTA

```json
{
    "status": true, //Se o login e senha estiver correto retornará true e na propriedade objeto os detalhes
    "id": 0,
    "idMongo": "",
    "mensagem": "",
    "exception": "",
    "url": "",
    "objeto": {
        "token": "8C8629BE52D97F7BAD9FD590F1D9C812C8A967CD7C3A6ACEC6A6AD3E1B76ACB9",
        "validadeToken": "2023-11-20T02:29:07.5562587+00:00",
        "ip": "",
        "nomeDispositivo": "",
        "usuarioEmail": "V2ZOaVdcixpi26wNkOov0f8J7QMxkCdKPHbB9Jowb2I="
    }
}
```

Abaixo retorna os dados do Token, validade do token e o e-mail criptografado.

Para qualquer controller que criar para que seja válido precisa enviar token pelo cabeçalho senão retornará acesso negado.

Caso use a API e tenha alguma duvida me coloco a disposição a ajudar.
