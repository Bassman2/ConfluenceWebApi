namespace ConfluenceWebApi.Service.Model;

internal class ErrorModel
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;

    [JsonPropertyName("status-code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("sub-code")]
    public int SubCode { get; set; }

    public override string ToString() => $"Message: \"{Message}\", StatusCode: {StatusCode}, SubCode: {SubCode}";
}


