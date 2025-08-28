namespace ConfluenceWebApi.Model;

// base class with Self only
internal class LinksModel
{
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }
}
