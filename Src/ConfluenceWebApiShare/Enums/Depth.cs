namespace ConfluenceWebApi;

/// <summary>
/// Specifies the depth of a query or operation, such as whether to include all levels or only the root level.
/// </summary>
public enum Depth
{
    /// <summary>
    /// Includes all levels in the operation or query.
    /// </summary>
    All, 

    /// <summary>
    /// Includes only the root level in the operation or query.
    /// </summary>
    Root
}

