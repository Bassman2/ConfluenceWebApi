namespace ConfluenceWebApi;

[JsonConverter(typeof(JsonStringEnumConverter<Representations>))]
public enum Representations
{
    Storage,
    Plain,
    View
}
