using Microsoft.Azure.Cosmos;

namespace Cobalt.CosmosConnector.Repository;

/// <summary>
/// Cosmos repository providing basic functionality
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICosmosRepository<T>
{
    /// <summary>
    /// Get all items in the container
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAll(Container container);

    /// <summary>
    /// Add a new item to the container
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="item">Your object</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T> AddNew(Container container, T item, string partitionKey);

    /// <summary>
    /// Delete Item via cosmos id
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="itemId">String variant ID</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<bool> DeleteItem(Container container, string itemId, string partitionKey);

    /// <summary>
    /// Delete item via cosmos id
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="itemId">Guid variant ID</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<bool> DeleteItem(Container container, Guid itemId, string partitionKey);

    /// <summary>
    /// Inserts or updates an item in the container
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="item">Your object</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T> UpsertItem(Container container, T item, string partitionKey);

    /// <summary>
    /// Get the item in the container via the cosmos id
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="id">Cosmos ID</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T?> GetById(Container container, string id, string partitionKey);

    /// <summary>
    /// Get the item in the container via the cosmos id
    /// </summary>
    /// <param name="container">The container to communicate with</param>
    /// <param name="id">Guid overload Cosmos ID</param>
    /// <param name="partitionKey">Cosmos partition key</param>
    /// <returns></returns>
    Task<T?> GetById(Container container, Guid id, string partitionKey);
}