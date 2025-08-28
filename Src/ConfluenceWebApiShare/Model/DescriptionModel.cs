namespace ConfluenceWebApi.Model;

internal class DescriptionModel
{
    [JsonPropertyName("plain")]
    public ValueRepresentationModel? Plain { get; set; }

    [JsonPropertyName("view")]
    public ValueRepresentationModel? View { get; set; }
}
