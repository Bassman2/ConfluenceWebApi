namespace ConfluenceWebApi.Service.Model;

internal class LabelModel
{
    [JsonPropertyName("prefix")]
    public string Prefix { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("label")]
    public string Text { get; set; } = null!;
}
