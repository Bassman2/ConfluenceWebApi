namespace ConfluenceWebApi;

/// <summary>
/// Specifies the types of Confluence content entities.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Types>))]
public enum Types
{
    /// <summary>
    /// Represents a Confluence page.
    /// </summary>
    page, 

    /// <summary>
    /// Represents a Confluence blog post.
    /// </summary>
    Blogpost,

    Personal
}
