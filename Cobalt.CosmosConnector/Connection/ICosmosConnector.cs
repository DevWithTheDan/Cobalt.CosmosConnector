using Microsoft.Azure.Cosmos;

namespace Cobalt.CosmosConnector.Connection;

/// <summary>
/// Provides connections to cosmos
/// </summary>
public interface ICosmosConnector
{
    /// <summary>
    /// Get the cosmos client via the passed the connection string
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    Task<CosmosClient> GetClient(string connectionString);
    
    /// <summary>
    /// Gets or creates the cosmos database based on the database name. Connections via a new client over the connection string
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="databaseName"></param>
    /// <returns></returns>
    Task<Database?> GetOrCreateDatabase(string connectionString, string databaseName);
    
    /// <summary>
    /// Get or creates the cosmos database using the given name, using the given client
    /// </summary>
    /// <param name="client"></param>
    /// <param name="databaseName"></param>
    /// <returns></returns>
    Task<Database?> GetOrCreateDatabase(CosmosClient client, string databaseName);

    /// <summary>
    /// Gets or creates a container for the cosmos database, using the given client
    /// </summary>
    /// <param name="client"></param>
    /// <param name="databaseName"></param>
    /// <param name="containerName"></param>
    /// <param name="partitionKeyPath"></param>
    /// <returns></returns>
    Task<Container?> GetOrCreateContainer(CosmosClient client, string databaseName, string containerName,
        string partitionKeyPath);

    /// <summary>
    /// Gets or creates a container for the cosmos database using a new cosmos client via the connection string
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="databaseName"></param>
    /// <param name="containerName"></param>
    /// <param name="partitionKeyPath"></param>
    /// <returns></returns>
    Task<Container?> GetOrCreateContainer(string connectionString, string databaseName, string containerName,
        string partitionKeyPath);

    /// <summary>
    /// Gets or creates a container using the given database
    /// </summary>
    /// <param name="database"></param>
    /// <param name="databaseName"></param>
    /// <param name="containerName"></param>
    /// <param name="partitionKeyPath"></param>
    /// <returns></returns>
    Task<Container?> GetOrCreateContainer(Database database, string containerName,
        string partitionKeyPath);
}