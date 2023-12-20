using ExampleImplementation.Models;

namespace ExampleImplementation.Services;

public interface IExampleService
{
    /// <summary>
    /// Get a collection of <see cref="ExampleModel"/> from the container
    /// </summary>
    /// <returns>A collection of <see cref="ExampleModel"/></returns>
    Task<IEnumerable<ExampleModel>?> GetAllExamples();

    /// <summary>
    /// Adds a random new <see cref="ExampleModel"/> to the container
    /// </summary>
    /// <returns></returns>
    Task<ExampleModel?> AddNewRandom();

    /// <summary>
    /// Delete a <see cref="ExampleModel"/> from the cosmos ID in the container
    /// </summary>
    /// <param name="id">Cosmos ID to remove from the container</param>
    /// <returns></returns>
    Task<bool> Delete(string id);
    
    
    /// <summary>
    /// Delete a <see cref="ExampleModel"/> from the cosmos ID in the container
    /// </summary>
    /// <param name="id">Cosmos ID to remove from the container</param>
    /// <returns></returns>
    Task<bool> Delete(Guid id);

}