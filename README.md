# Cobalt Cosmos Connector
## A helper library to get you started connecting to the Azure Cosmos SDK
This library will help get you connected to cosmos, it provides a base cosmos model, a connection service, and a generic repository

### Contents 
- Connection
- Cosmos Repository
- Model


### A few things you need to know first

- A cosmos database holds many containers. 
- Containers will usually map to a single object in c#.
  - In most instances you will need a container per object.

Database naming conventions: 
- Database name must not be null or empty
- Database name must not be longer than 256 characters

Container naming conventions: 
- Container names must start and end with a letter or a number
- Container names must only contain letters, numbers and the hyphen/minus character(-)
- Each hyphen should be preceded and followed by another acceptable character (not another hyphen)
- All letters must be lower case
- Container names must be between 3 and 63 characters long



### Connection
In this section the cosmos connection will help you get connected to cosmos. 

- Get a client connects you to a cosmos client using the parameters you give. 
  - You will need to pass a connection string, you can get this from you're Azure account
- Various overloads for connecting to the database
- Various overloads for getting a container 

### Model
In this section, you will find a Base Cosmos Model. This can be inherited by your models, it simply provides a cosmos compatible ID which defaults to new Guid as a string.This is overridable if you prefer.

### Cosmos Repository
Your model can be passed on implementation. This will allow you to reuse this with any object you need.
This will give you basic functionality covering:
- Get All
- Get By Id
- Add New
- Upsert
- Delete