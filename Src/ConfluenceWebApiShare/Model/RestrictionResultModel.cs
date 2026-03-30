namespace ConfluenceWebApi.Model;

internal class RestrictionResultModel : BaseModel
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
