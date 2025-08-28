namespace ConfluenceWebApi.Model;

internal class ExtensionsModel : BaseModel
{
    [JsonPropertyName("mediaType")]
    public string? MediaType { get; set; }

    [JsonPropertyName("fileSize")]
    public long? FileSize { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}
