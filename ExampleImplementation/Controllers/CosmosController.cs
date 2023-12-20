using Cobalt.CosmosConnector.Connection;
using Cobalt.CosmosConnector.Repository;
using ExampleImplementation.Models;
using ExampleImplementation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleImplementation.Controllers;

[ApiController]
[Route("[controller]")]
public class CosmosController(IExampleService exampleService) : Controller
{
   private readonly IExampleService _exampleService = exampleService;
   
   
   /// <summary>
   /// Get all the examples
   /// </summary>
   /// <returns></returns>
   [HttpGet("GetAllExamples")]
   public async Task<IActionResult> GetAllExamples()
   {
      var collection = await _exampleService.GetAllExamples();
      return new OkObjectResult(collection);
   }
   
   /// <summary>
   /// Add a new random example to the container
   /// </summary>
   /// <returns></returns>
   [HttpGet("AddNewRandomExample")]
   public async Task<IActionResult> AddNewRandomExample()
   {
      var item = await _exampleService.AddNewRandom();
      return new OkObjectResult(item);
   }

   /// <summary>
   /// Deletes the item from the container
   /// </summary>
   /// <param name="id">Item id to delete</param>
   /// <returns></returns>
   [HttpPost("DeleteItem")]
   public async Task<IActionResult> DeleteFromDb(Guid id)
   {
      var success = await _exampleService.Delete(id);
      return new OkObjectResult(success);
   }

}