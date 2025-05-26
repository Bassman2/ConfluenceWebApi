namespace ConfluenceWebApi.Service.Model;

internal class IconModel
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = null!;

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }
}
