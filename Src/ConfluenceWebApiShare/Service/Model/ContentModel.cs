namespace ConfluenceWebApi.Service.Model;

/// <summary>
/// Represents a content item in Confluence, including its metadata and associated space.
/// </summary>
internal class ContentModel
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

    [JsonPropertyName("history")]
    public HistoryModel? History { get; set; }

    [JsonPropertyName("version")]
    public VersionModel? Version { get; set; }

    [JsonPropertyName("ancestors")]
    public List<ContentModel>? Ancestors { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("operations")]
    public List<OperationModel>? Operations { get; set; }

    [JsonPropertyName("children")]
    public ChildrenModel? Children { get; set; }

    [JsonPropertyName("descendants")]
    public DescendantsModel? Descendants { get; set; }

    [JsonPropertyName("container")]
    public ContainerModel? Container { get; set; }

    [JsonPropertyName("body")]
    public BodyModel? Body { get; set; }

    [JsonPropertyName("metadata")]
    public MetadataModel? Metadata { get; set; }

    [JsonPropertyName("extensions")]
    public ExtensionsModel? Extensions { get; set; }

    [JsonPropertyName("restrictions")]
    public RestrictionsModel? Restrictions { get; set; }

    [JsonPropertyName("_links")]
    public ContentLinksModel? Links { get; set; }
}
