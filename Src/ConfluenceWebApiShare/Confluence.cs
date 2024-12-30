namespace ConfluenceWebApi;

public sealed class Confluence : IDisposable
{
    private ConfluenceService? service;

    public Confluence(string storeKey)
    {
        var key = WebServiceClient.Store.KeyStore.Key(storeKey)!;
        string host = key.Host!;
        string token = key.Token!;
        service = new ConfluenceService(new Uri(host), token);
    }

    public Confluence(Uri host, string apikey)
    {
        service = new ConfluenceService(host, apikey);
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