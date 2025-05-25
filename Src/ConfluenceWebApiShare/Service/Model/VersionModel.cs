namespace ConfluenceWebApi.Service.Model;

internal class VersionModel
{
    [JsonPropertyName("by")]
    public UserModel? By { get; set; }

    [JsonPropertyName("when")]
    public DateTime? When { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("minorEdit")]
    public bool MinorEdit { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("content")]
    public ContentModel? Content { get; set; }

    [JsonPropertyName("_links")]
    public LinksModel? Links { get; set; }

    [JsonPropertyName("_expandable")]
    public ExpandableModel? Expandable { get; set; }
}
