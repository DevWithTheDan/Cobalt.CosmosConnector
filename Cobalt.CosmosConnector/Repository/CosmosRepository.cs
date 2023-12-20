using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Cobalt.CosmosConnector.Repository;

public class CosmosRepository<T>(Container container) : ICosmosRepository<T>
{
    private readonly Container _container = container;

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAll()
    {
        var query = _container.GetItemLinqQueryable<T>();
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
    public async Task<T> AddNew(T item, string partitionKey)
    {
        var response = await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
        return response.Resource;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteItem(string itemId, string partitionKey)
    {
        var response = await _container.DeleteItemAsync<T>(itemId, new PartitionKey(partitionKey));
        var resource = response.Resource;
        return resource == null;
    }

    /// <inheritdoc />
    public async Task<T> UpsertItem(T item, string partitionKey)
    {
        var response = await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
        return response.Resource;
    }

    /// <inheritdoc />
    public async Task<T> GetById(string id, string partitionKey)
    {
        var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
        return response.Resource;
    }
}