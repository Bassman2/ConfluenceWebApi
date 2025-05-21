using System.Threading;

namespace ConfluenceWebApi;

public sealed class Confluence : IDisposable
{
    private ConfluenceService? service;

    public Confluence(string storeKey, string appName)
       : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    public Confluence(Uri host, string token, string appName)
    {
        service = new ConfluenceService(host,
            new MultiAuthenticator(           
                new BearerAuthenticator(token) 
                //, new Basic2Authenticator(token)
                
                ),
            appName);
    }

    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

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

    public IAsyncEnumerable<ContentModel> SearchContentAsync(string sql, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        return service.SearchContentAsync(sql, expand, cancellationToken); 
    }

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
}