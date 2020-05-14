# Crud operation Cosmos DB and Azure .NET Core DSK

Microsoft Azure .NET Core DSK provides comprehensive API to connect Azure services. In this sample has used Microsoft.Azure.Cosmos package to query Cosmos DB database through Cosmos DB SQL API. 

It is a smaple crud application which demonstrates Add, Edit, Update and Delete data stored in Cosmos DB. 

In order to run application in local development environment, a Cosmos DB Emulator would be required. Whichi is available on below microsoft link

https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes


# Cosmos DB Connection String

In order to connect to database, connection strign would be required. The Connection string is combination of URL and authorization key. Which can be found on Quick Start section of local emulator's data explorer console. 


The application has two part
1. .net core api for communicatation to CosmosDb using Azure.NET SDK 
2. User interface developed in angular 

Happy learning!!!
