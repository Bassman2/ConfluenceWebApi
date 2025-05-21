namespace ConfluenceWebApi;

/// <summary>
/// Represents a content item in Confluence, including its metadata and associated space.
/// </summary>
[DebuggerDisplay("{Id}: {Title} - {Status}")]
public class Content
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Content"/> class using the specified <see cref="ContentModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="ContentModel"/> containing content data.</param>
    internal Content(ContentModel model)
    {
        Id = model.Id;
        Type = model.Type;
        Status = model.Status;
        Title = model.Title;
        Space = model.Space.CastModel<Space>();
    }

    /// <summary>
    /// Gets or sets the unique identifier of the content item.
    /// </summary>
    public string Id { get; set; }

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
    public Space? Space { get; set; }
}
