namespace ConfluenceWebApi;

public sealed class Confluence : IDisposable
{
    private ConfluenceService? service;

    public Confluence(string storeKey, string appName)
       : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    public Confluence(Uri host, string token, string appName)
    {
        service = new ConfluenceService(host, new BearerAuthenticator(token), appName);
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

    public async Task DeleteLabelAsync(string id, string label, CancellationToken cancellationToken = default)
    {
        await service!.DeleteLabelAsync(id, label, cancellationToken);
    }

    public IAsyncEnumerable<ContentModel> SearchContentAsync(string sql, string? expand = null, CancellationToken cancellationToken = default)
    {
        return service!.SearchContentAsync(sql, expand, cancellationToken); 
    }
}