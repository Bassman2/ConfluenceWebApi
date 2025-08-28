namespace ConfluenceWebApi.Model;

internal class ErrorModel
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }

    [JsonPropertyName("data")]
    public ErrorDataModel? Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("sub-code")]
    public int SubCode { get; set; }

    public override string ToString() => $"\nStatusCode: {StatusCode}\nSubCode: {SubCode}\nMessage: \"{Message}\"\nReason: {Reason}\nErrors: {Data?.ToString()}\n";
}


