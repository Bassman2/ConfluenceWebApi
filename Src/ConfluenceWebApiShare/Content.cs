namespace ConfluenceWebApi;

[DebuggerDisplay("{Id}: {Title} - {Status}")]
public class Content
{
    internal Content(ContentModel model)
    {
        Id = model.Id;
        Type = model.Type;
        Status = model.Status;
        Title = model.Title;
        Space = model.Space.CastModel<Space>();
    }

    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("space")]
    public Space? Space { get; set; }
}
