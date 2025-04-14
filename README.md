# Api Template

#### Esta api foi criada como um template para APIS RESTful  com diversos recursos já configurados. Foi utilizado o .net framework 7 com design em DDD utilizando os seguintes paterns:

- **Repository**
- **Unit of Work**
- **Mediator** 
- **Dependency Injection**
- **Command**
- **CQRS**

#### Segue algumas implementações feitas:

- **Dapper com o Banco MySql**
- **Implementação de Validators**
- **Handler de erros de validação para tratamento de resposta 422**
- **Logs utilizando a biblioteca Serilog com Elastic Search**
- **Kibana no docker compose plugado no Elastic Search**
- **AutoMapper**
- Token Jwt com OAuth 2


## Instruções para rodar
Na pasta raiz do projeto rodar o comando abaixo para subir a API, o banco de dados MySql, o Kibana e o Elastic Search em containers do docker: 

```
docker-compose up -d
```
O comando abaixo remove todos os containers com suas respectivas imagens: 

```
docker-compose down --rmi all -v
```

## Endereços configurados no docker-compose

| Serviço  | Endereço |
| ------------- | ------------- |
| Api | http://localhost:8080 |
| MySql |  http://localhost:3307  | 
| Kibana | http://localhost:5601/  |
| Elastic Search | http://localhost:9200/ |


Linkedin: https://www.linkedin.com/in/fabio-guedes-dos-santos-86731046/
