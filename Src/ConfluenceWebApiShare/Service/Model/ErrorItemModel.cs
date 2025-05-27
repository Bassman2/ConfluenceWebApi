namespace ConfluenceWebApi.Service.Model;

internal class ErrorItemModel
{
    [JsonPropertyName("message")]
    public ErrorMessageModel? Message { get; set; }

    public override string ToString() => Message?.ToString() ?? string.Empty;
}
