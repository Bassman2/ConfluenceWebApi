namespace ConfluenceWebApi.Model;

// base class with Self only
internal class LinksModel
{
    [JsonPropertyName("download")]
    public Uri? Download { get; set; }

    [JsonPropertyName("webui")]
    public Uri? Webui { get; set; }

    [JsonPropertyName("self")]
    public Uri? Self { get; set; }
}
