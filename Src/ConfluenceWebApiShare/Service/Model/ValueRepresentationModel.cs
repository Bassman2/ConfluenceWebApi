namespace ConfluenceWebApi.Service.Model;

internal class ValueRepresentationModel
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    
    [JsonPropertyName("representation")]
    public string? Representation { get; set; }
}
