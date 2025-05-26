namespace ConfluenceWebApi.Service.Model;

/// <summary>
/// Represents a Confluence space with its basic properties.
/// </summary>
internal class SpaceModel : BaseModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the space.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

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
    public Statuses Status { get; set; }

    [JsonPropertyName("icon")]
    public IconModel? Icon { get; set; }

    [JsonPropertyName("description")]
    public DescriptionModel? Description { get; set; }

    [JsonPropertyName("homepage")]
    public ContentModel? Homepage { get; set; }

    [JsonPropertyName("type")]
    public Types Type { get; set; }

    [JsonPropertyName("metadata")]
    public MetadataModel? Metadata { get; set; }
}
