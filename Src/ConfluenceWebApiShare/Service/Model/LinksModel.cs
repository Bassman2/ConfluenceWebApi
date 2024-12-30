namespace ConfluenceWebApi.Service.Model;

internal class LinksModel
{

    [JsonPropertyName("base")]
    public Uri? Base { get; set; }

    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    [JsonPropertyName("context")]
    public Uri? Context { get; set; }

    [JsonPropertyName("next")]
    public Uri? Next { get; set; }

    [JsonPropertyName("prev")]
    public Uri? Prev { get; set; }
}
