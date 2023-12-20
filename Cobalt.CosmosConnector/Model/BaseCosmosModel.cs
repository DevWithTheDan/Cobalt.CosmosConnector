﻿using Newtonsoft.Json;

namespace Cobalt.CosmosConnector.Model;

public class BaseCosmosModel
{
    /// <summary>
    /// Base cosmos identifier this is mapped to lower case for you
    /// <para>
    /// This has been defaulted to a New Guid mapped to a string you can override this if you would like
    ///</para>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}