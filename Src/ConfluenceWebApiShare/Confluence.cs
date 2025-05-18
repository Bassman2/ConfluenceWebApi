namespace ConfluenceWebApi;

/// <summary>
/// Represents a client for interacting with the Confluence API.
/// </summary>
public sealed class Confluence : IDisposable
{
    private ConfluenceService? service;

    /// <summary>
    /// Initializes a new instance of the <see cref="Confluence"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key used to retrieve the Confluence host and token from the key store.</param>
    /// <param name="appName">The name of the application.</param>
    public Confluence(string storeKey, string appName)
       : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Confluence"/> class with the specified host, token, and application name.
    /// </summary>
    /// <param name="host">The base URI of the Confluence instance.</param>
    /// <param name="token">The authentication token for accessing the Confluence API.</param>
    /// <param name="appName">The name of the application.</param>
    public Confluence(Uri host, string token, string appName)
    {
        service = new ConfluenceService(host, new BearerAuthenticator(token), appName);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="Confluence"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Deletes a label from a content item in Confluence.
    /// </summary>
    /// <param name="id">The ID of the content item.</param>
    /// <param name="label">The label to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteLabelAsync(string id, string label, CancellationToken cancellationToken = default)
    {
        await service!.DeleteLabelAsync(id, label, cancellationToken);
    }

    /// <summary>
    /// Searches for content in Confluence using a CQL (Confluence Query Language) query.
    /// </summary>
    /// <param name="sql">The CQL query string.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="ContentModel"/> objects that match the query.</returns>
    public IAsyncEnumerable<ContentModel> SearchContentAsync(string sql, string? expand = null, CancellationToken cancellationToken = default)
    {
        return service!.SearchContentAsync(sql, expand, cancellationToken);
    }
}
