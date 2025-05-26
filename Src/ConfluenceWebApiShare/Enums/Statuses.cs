namespace ConfluenceWebApi;

[JsonConverter(typeof(JsonStringEnumConverter<Statuses>))]
public enum Statuses
{
    Current,
    Archived
}
