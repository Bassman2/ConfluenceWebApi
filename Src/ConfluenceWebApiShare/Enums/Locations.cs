namespace ConfluenceWebApi;

/// <summary>
/// Specifies the possible locations for comments or annotations within Confluence content.
/// </summary>
public enum Locations
{
    /// <summary>
    /// Indicates an inline location, such as comments or annotations placed directly within the content.
    /// </summary>
    Inline, 

    /// <summary>
    /// Indicates a footer location, such as comments or notes placed at the bottom of the content.
    /// </summary>
    Footer, 

    /// <summary>
    /// Indicates a resolved location, typically used for comments or issues that have been marked as resolved.
    /// </summary>
    Resolved
}
