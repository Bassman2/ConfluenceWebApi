namespace ConfluenceWebApi.Model;

internal class ContentLinksModel 
{
    [JsonPropertyName("webui")]
    public string? WebUI { get; set; }

    [JsonPropertyName("edit")]
    public string? Edit { get; set; }

    [JsonPropertyName("tinyui")]
    public string? TinyUI { get; set; }

    [JsonPropertyName("self")]
    public string? Self { get; set; }
}
