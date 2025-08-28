namespace ConfluenceWebApi.Model;

internal class ManifestModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("typeId")]
    public string? TypeId { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("buildNumber")]
    public int BuildNumber { get; set; }

    [JsonPropertyName("applinksVersion")]
    public string? ApplinksVersion { get; set; }

    [JsonPropertyName("isCloud")]
    public bool? IsCloud { get; set; }
}
