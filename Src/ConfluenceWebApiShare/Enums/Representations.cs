namespace ConfluenceWebApi;

[JsonConverter(typeof(JsonStringEnumConverter<Representations>))]
public enum Representations
{
    storage,
    Plain,
    View
}
