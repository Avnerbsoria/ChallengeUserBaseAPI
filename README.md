#Documentação de primeira execução da API 📜

Bem-vindo(a) ao **Readme da API**! Aqui você encontrará alguns detalhes de como executar o projeto, fico à disposição caso haja mais duvidas.

## Instalação SQL Server no Docker 

Segue os comandos necessários :

docker pull mcr.microsoft.com/mssql/server:2019-latest

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433  --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

## Comandos de migração dos modelos de entidades no EF Core(abrir o console do gerenciador de pacotes NuGet no Visual Studio menu Ferramentas e selecionar o projeto Infrastructure):

Add-Migration AddUser

Update-Database

##Adicionar o primeiro usuário:
 Para adicionar o primeiro usuário poderá comentar a tag [Authorize] no UserController e executar o endpoint ‘CreateUser’ :

 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "managerid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "email": "avner@gmail.com",
  "firstname": "Avner",
  "lastname": "Soria",
  "phone": "4364563546",
  "phone2": "6634563456",
  "birthday": "1993-03-07T19:20:57.443Z",
  "password": "123456",
  "role": "adm",
  "docnumber": 03396576574

## Após isso poderá verificar todas as funcionalidades pelo Swagger, que já está documentado.🎉 










