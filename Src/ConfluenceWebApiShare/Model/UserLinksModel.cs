namespace ConfluenceWebApi.Model;

internal class UserLinksModel : LinksModel
{

    [JsonPropertyName("base")]
    public Uri? Base { get; set; }
    
    [JsonPropertyName("context")]
    public Uri? Context { get; set; }

    [JsonPropertyName("next")]
    public Uri? Next { get; set; }

    [JsonPropertyName("prev")]
    public Uri? Prev { get; set; }
}
