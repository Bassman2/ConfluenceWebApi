namespace ConfluenceWebApi;

public class ValueRepresentation
{
    internal ValueRepresentation(ValueRepresentationModel model)
    {
        Content = model.Content.CastModel<Content>();
        Value = model.Value;
        Representation = model.Representation;
        Webresource = model.Webresource;
    }
    public Content? Content { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("representation")]
    public string? Representation { get; set; }

    [JsonPropertyName("webresource")]
    public string? Webresource { get; set; }

}