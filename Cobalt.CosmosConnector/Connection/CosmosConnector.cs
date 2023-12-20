using Microsoft.Azure.Cosmos;

namespace Cobalt.CosmosConnector.Connection;

///  <inheritdoc />
public class CosmosConnector : ICosmosConnector
{
    /// <inheritdoc />
    public Task<CosmosClient> GetClient(string connectionString)
    {
        return Task.FromResult(new CosmosClient(connectionString));
    }
    
    /// <inheritdoc />
    public async Task<Database?> GetOrCreateDatabase(string connectionString, string databaseName)
    {
        var client = await GetClient(connectionString);
        var dbResponse = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        return dbResponse.Database;
    }  
    
    /// <inheritdoc />
    public async Task<Database?> GetOrCreateDatabase(CosmosClient client, string databaseName)
    {
        var dbResponse = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        return dbResponse.Database;
    }

    /// <inheritdoc />
    public async Task<Container?> GetOrCreateContainer(CosmosClient client, string databaseName, string containerName, string partitionKeyPath)
    {
        var db = await GetOrCreateDatabase(client, databaseName);
        if (db == null)
        {
            return null;
        }
        var containerResponse = await db.CreateContainerIfNotExistsAsync(new ContainerProperties(containerName, partitionKeyPath));
        return containerResponse.Container;
    }
    
    
    public async Task<Container?> GetOrCreateContainer(string connectionString, string databaseName, string containerName, string partitionKeyPath)
    {
        var client = await GetClient(connectionString);
        var db = await GetOrCreateDatabase(client, databaseName);
        if (db == null)
        {
            return null;
        }
        var containerResponse = await db.CreateContainerIfNotExistsAsync(new ContainerProperties(containerName, partitionKeyPath));
        return containerResponse.Container;
    }
    
    /// <inheritdoc />
    public async Task<Container?> GetOrCreateContainer(Database database, string containerName, string partitionKeyPath)
    {
        var containerResponse = await database.CreateContainerIfNotExistsAsync(new ContainerProperties(containerName, partitionKeyPath));
        return containerResponse.Container;
    }
}