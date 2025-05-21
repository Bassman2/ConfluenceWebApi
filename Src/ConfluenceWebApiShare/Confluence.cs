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
        service = new ConfluenceService(host,
            new MultiAuthenticator(           
                new BearerAuthenticator(token) 
                //, new Basic2Authenticator(token)
                
                ),
            appName);
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

    #region Access Mode

    public async Task<string?> GetAccessModeStatusAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetAccessModeStatusAsync(cancellationToken);
        return res?.Trim('"');
    }

    #endregion

    #region Space

    public async IAsyncEnumerable<Content> GetContentsInSpaceAsync(string spaceKey, Depth depth = Depth.All, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentsInSpaceAsync(spaceKey, depth, expand, cancellationToken);
        await foreach (var item in res)
        {
            //if (cancellationToken.IsCancellationRequested) yield break;

            yield return item.CastModel<Content>()!;
        }
    }

    public async IAsyncEnumerable<Content> GetContentsByTypeAsync(string spaceKey, Types type = Types.Page, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentsByTypeAsync(spaceKey, type, expand, cancellationToken);
        await foreach (var item in res)
        {
            //if (cancellationToken.IsCancellationRequested) yield break;

            yield return item.CastModel<Content>()!;
        }
    }

    public async Task<Space?> GetSpaceAsync(string spaceKey, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetSpaceAsync(spaceKey, expand, cancellationToken);
        return res.CastModel<Space>(); 
    }

    public async Task DeleteSpaceAsync(string spaceKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteSpaceAsync(spaceKey, cancellationToken);
    }

    #endregion

    #region User

    public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetCurrentUserAsync(cancellationToken);
        return res.CastModel<User>();
    }

    #endregion
    public async Task DeleteLabelAsync(string id, string label, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteLabelAsync(id, label, cancellationToken);
    }

    /// <summary>
    /// Searches for content in Confluence using a CQL (Confluence Query Language) query.
    /// </summary>
    /// <param name="sql">The CQL query string.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="ContentModel"/> objects that match the query.</returns>
    #region Content Resource

    public IAsyncEnumerable<ContentModel> SearchContentAsync(string sql, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        return service.SearchContentAsync(sql, expand, cancellationToken); 
    }

    #endregion

    #region Export

    public async Task ExportPdfAsync(int pageId, string filePath, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.ExportPdfAsync(pageId, filePath, cancellationToken);
    }

    public async Task<Stream> ExportPdfStreamAsync(int pageId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        return await service.ExportPdfStreamAsync(pageId, cancellationToken);
    }

    #endregion
}