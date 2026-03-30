namespace ConfluenceWebApi.Model;

internal class RestrictionActionModel : BaseModel
{
    [JsonPropertyName("operation")]
    public string? Operation { get; set; }

    [JsonPropertyName("restrictions")]
    public RestrictionsRestrictionsModel? Restrictions { get; set; }

    [JsonPropertyName("lastModificationDate")]
    public string? LastModificationDate { get; set; }
}
