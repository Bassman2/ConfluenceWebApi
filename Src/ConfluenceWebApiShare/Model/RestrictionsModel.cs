namespace ConfluenceWebApi.Model;

internal class RestrictionsModel : BaseModel
{
    [JsonPropertyName("read")]
    public RestrictionActionModel? Read { get; set; }

    [JsonPropertyName("update")]
    public RestrictionActionModel? Update { get; set; }
}
