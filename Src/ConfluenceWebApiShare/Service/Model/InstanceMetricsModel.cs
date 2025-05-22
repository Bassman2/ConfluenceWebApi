namespace ConfluenceWebApi.Service.Model;

internal class InstanceMetricsModel
{
    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    [JsonPropertyName("spaces")]
    public int Spaces { get; set; }

    [JsonPropertyName("users")]
    public int Users { get; set; }
}
