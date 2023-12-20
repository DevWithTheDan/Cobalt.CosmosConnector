using Cobalt.CosmosConnector.Connection;
using Cobalt.CosmosConnector.Repository;
using ExampleImplementation.Models;
using Microsoft.Azure.Cosmos;
using Serilog;

namespace ExampleImplementation.Services;

public class ExampleService(
    IConfiguration configuration,
    ICosmosConnector cosmosConnector,
    ICosmosRepository<ExampleModel> exampleRepo) : IExampleService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ICosmosConnector _connector = cosmosConnector;
    private readonly ICosmosRepository<ExampleModel> _cosmosRepository = exampleRepo;

    ///<summary>
    ///  Gets the cosmos container using app settings configuration
    /// </summary>
    private Task<Container?> GetContainer()
    {
        var connectionString = _configuration.GetSection("Cosmos:CosmosDb").Value;
        var dbName = _configuration.GetSection("Cosmos:CosmosDbName").Value;
        var containerName = _configuration.GetSection("Cosmos:CosmosExampleContainerName").Value;

        // If connection details cannot be found throw an error
        if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(dbName) ||
            string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(ExampleService));
        }

        // For this example the partition key is mapped the to cosmos id
        return _connector.GetOrCreateContainer(connectionString, dbName, containerName, "/id");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExampleModel>?> GetAllExamples()
    {
        var container = await GetContainer();
        if (container == null)
        {
            throw new ArgumentNullException(nameof(ExampleService));
        }

        try
        {
            return await _cosmosRepository.GetAll(container);
        }
        // Handle cosmos errors in implementation how you see fit
        catch (CosmosException cosmosException)
        {
            Log.Error(cosmosException, "Cosmos exception");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ExampleModel?> AddNewRandom()
    {
        var container = await GetContainer();
        if (container == null)
        {
            throw new ArgumentNullException(nameof(ExampleService));
        }

        var random = new Random();
        var exampleModel = new ExampleModel()
        {
            Name = "Random Value",
            Value = random.NextDouble()
        };
        try
        {
            // For this example the partition key is mapped the to cosmos id
            return await _cosmosRepository.AddNew(container, exampleModel, exampleModel.Id);
        }
        // Handle cosmos errors in implementation how you see fit
        catch (CosmosException cosmosException)
        {
            Log.Error(cosmosException, "Cosmos exception");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Delete(string id)
    {
        var container = await GetContainer();
        if (container == null)
        {
            throw new ArgumentNullException(nameof(ExampleService));
        }

        try
        {
            // For this example the partition key is mapped the to cosmos id
            return await _cosmosRepository.DeleteItem(container, id, id);
        }
        // Handle cosmos errors in implementation how you see fit
        catch (CosmosException cosmosException)
        {
            Log.Error(cosmosException, "Cosmos exception");
            return false;
        }
    }


    /// <inheritdoc />
    public async Task<bool> Delete(Guid id)
    {
        var container = await GetContainer();
        if (container == null)
        {
            throw new ArgumentNullException(nameof(ExampleService));
        }

        try
        {
            // For this example the partition key is mapped the to cosmos id
            return await _cosmosRepository.DeleteItem(container, id, id.ToString());
        }
        // Handle cosmos errors in implementation how you see fit
        catch (CosmosException cosmosException)
        {
            Log.Error(cosmosException, "Cosmos exception");
            return false;
        }
    }
}