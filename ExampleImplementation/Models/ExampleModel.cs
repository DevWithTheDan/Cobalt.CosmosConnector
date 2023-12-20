using Cobalt.CosmosConnector.Model;

namespace ExampleImplementation.Models;

public class ExampleModel : BaseCosmosModel
{
    public string Name { get; set; } = null!;
    public double Value { get; set; }
}