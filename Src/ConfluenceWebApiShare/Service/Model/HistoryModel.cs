namespace ConfluenceWebApi.Service.Model;

internal class HistoryModel
{
    [JsonPropertyName("lastUpdated")]
    public VersionModel? LastUpdated { get; set; }

    [JsonPropertyName("latest")]
    public bool Latest { get; set; }

    [JsonPropertyName("createdBy")]
    public UserModel? CreatedBy { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("contributors")]
    public ContributorsModel? Contributors { get; set; }

    [JsonPropertyName("_links")]
    public LinksModel? Links { get; set; }

}
