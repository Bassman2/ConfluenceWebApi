namespace ConfluenceWebApi.Model;

internal class MetadataModel : BaseModel
{
    [JsonPropertyName("mediaType")]
    public string? MediaType { get; set; }

    [JsonPropertyName("labels")]
    public ResultListModel<LabelModel>? Labels { get; set; }

}
