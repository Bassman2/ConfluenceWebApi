namespace ConfluenceWebApi.Service.Model;

internal class ValueRepresentationModel : BaseModel
{
    [JsonPropertyName("content")]
    public ContentModel? Content { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
    
    [JsonPropertyName("representation")]
    public Representations Representation { get; set; }

    [JsonPropertyName("webresource")]
    public string? Webresource { get; set; }

}
