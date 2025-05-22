namespace ConfluenceWebApi;

/// <summary>
/// Represents metrics or statistical information for a Confluence instance, including the number of pages, spaces, and users.
/// </summary>
public class InstanceMetrics
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceMetrics"/> class using the specified model.
    /// </summary>
    /// <param name="model">The model containing instance metrics data.</param>
    internal InstanceMetrics(InstanceMetricsModel model)
    {
        Pages = model.Pages;
        Spaces = model.Spaces;
        Users = model.Users;
    }

    /// <summary>
    /// Gets the total number of pages in the Confluence instance.
    /// </summary>
    public int Pages { get; }

    /// <summary>
    /// Gets the total number of spaces in the Confluence instance.
    /// </summary>
    public int Spaces { get; }

    /// <summary>
    /// Gets the total number of users in the Confluence instance.
    /// </summary>
    public int Users { get; }
}
