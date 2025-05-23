namespace ConfluenceWebApi.Service.Model;

internal class ResultListModel<T>
{
    [JsonPropertyName("results")]
    public List<T>? Results { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

}
