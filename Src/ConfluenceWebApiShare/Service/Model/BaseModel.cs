namespace ConfluenceWebApi.Service.Model;

internal class BaseModel
{
    [JsonPropertyName("_links")]
    public LinksModel? Links { get; set; }

    [JsonPropertyName("_expandable")]
    public ExpandableModel? Expandable { get; set; }
}
