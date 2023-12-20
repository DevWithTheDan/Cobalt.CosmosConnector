using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Cobalt.CosmosConnector.Repository;

/// <inheritdoc />
public class CosmosRepository<T>() : ICosmosRepository<T>
{
    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAll(Container container)
    {
        var query = container.GetItemLinqQueryable<T>();
        var iterator = query.ToFeedIterator();
        var list = new List<T>();
        while (iterator.HasMoreResults)
        {
            var currentIteration = await iterator.ReadNextAsync();
            list.AddRange(currentIteration);
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<T> AddNew(Container container, T item, string partitionKey)
    {
        var response = await container.CreateItemAsync(item, new PartitionKey(partitionKey));
        return response.Resource;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteItem(Container container, string itemId, string partitionKey)
    {
        var response = await container.DeleteItemAsync<T>(itemId, new PartitionKey(partitionKey));
        var resource = response.Resource;
        return resource == null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteItem(Container container, Guid itemId, string partitionKey)
    {
        var exists = await GetById(container, itemId, partitionKey);
        if (exists == null)
        {
            return false;
        }

        var response = await container.DeleteItemAsync<T>(itemId.ToString(), new PartitionKey(partitionKey));
        var resource = response.Resource;
        return resource == null;
    }

    /// <inheritdoc />
    public async Task<T> UpsertItem(Container container, T item, string partitionKey)
    {
        var response = await container.UpsertItemAsync(item, new PartitionKey(partitionKey));
        return response.Resource;
    }

    /// <inheritdoc />
    public async Task<T?> GetById(Container container, string id, string partitionKey)
    {
        var response = await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
        return response.StatusCode == HttpStatusCode.NotFound ? default : response.Resource;
    }

    /// <inheritdoc />
    public async Task<T?> GetById(Container container, Guid id, string partitionKey)
    {
        var response = await container.ReadItemAsync<T>(id.ToString(), new PartitionKey(partitionKey));
        return response.Resource;
    }
}