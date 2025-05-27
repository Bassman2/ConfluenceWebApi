namespace ConfluenceWebApi.Service.Model;

internal class ErrorMessageModel
{
    [JsonPropertyName("args")]
    public List<string>? Args { get; set; }

    [JsonPropertyName("translation")]
    public string? Translation { get; set; }

    public override string ToString()
    {
        string args = Args != null ? string.Join(',', Args) : string.Empty;
        return $"{args} - {Translation}";
    }

}
