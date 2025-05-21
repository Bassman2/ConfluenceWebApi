namespace ConfluenceWebApi.Service.Model;

/// <summary>
/// Represents a content item in Confluence, including its metadata and associated space.
/// </summary>
public class ContentModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the content item.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the content (e.g., "page", "blogpost").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the status of the content (e.g., "current", "archived").
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the title of the content.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the space to which this content belongs.
    /// </summary>
    [JsonPropertyName("space")]
    public SpaceModel? Space { get; set; }
}
