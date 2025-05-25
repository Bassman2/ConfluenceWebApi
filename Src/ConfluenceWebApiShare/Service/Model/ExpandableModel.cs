namespace ConfluenceWebApi.Service.Model;

/// <summary>
/// Represents a model that can be expanded with additional properties, including status information.
/// </summary>
public class ExpandableModel
{
    /// <summary>
    /// Gets or sets the status of the model.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
