namespace ConfluenceWebApi.Service.Model;

internal class UserModel
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("userKey")]
    public string? UserKey { get; set; }

    [JsonPropertyName("profilePicture")]
    public ProfilePictureModel? ProfilePicture { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("_links")]
    public UserLinksModel? Links { get; set; }

    [JsonPropertyName("_expandable")]
    public ExpandableModel? Expandable { get; set; }
}
