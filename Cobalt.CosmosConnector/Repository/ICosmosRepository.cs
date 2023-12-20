namespace Cobalt.CosmosConnector;

public interface ICosmosRepository<T>
{
    /// <summary>
    /// Get all items in the container
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Add a new item to the container
    /// </summary>
    /// <param name="item">Your object</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T> AddNew(T item, string partitionKey);

    /// <summary>
    /// Delete Item via cosmos id
    /// </summary>
    /// <param name="itemId">Your object</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<bool> DeleteItem(string itemId, string partitionKey);

    /// <summary>
    /// Inserts or updates an item in the container
    /// </summary>
    /// <param name="item">Your object</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T> UpsertItem(T item, string partitionKey);

    /// <summary>
    /// Get the item in the container via the cosmos id
    /// </summary>
    /// <param name="id">Cosmos ID</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T> GetById(string id, string partitionKey);
}