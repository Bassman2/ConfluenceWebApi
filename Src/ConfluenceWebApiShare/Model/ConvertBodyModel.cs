namespace ConfluenceWebApi.Model;

internal class ConvertBodyModel
{
    [JsonPropertyName("representation")]
    public string Representation { get; set; } = "";

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";
}
