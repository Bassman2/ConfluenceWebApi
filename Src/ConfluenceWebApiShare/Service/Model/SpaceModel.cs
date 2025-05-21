namespace ConfluenceWebApi.Service.Model;

/// <summary>
/// Represents a Confluence space with its basic properties.
/// </summary>
public class SpaceModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the space.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the key of the space.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the space.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the status of the space (e.g., "current", "archived").
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

}
