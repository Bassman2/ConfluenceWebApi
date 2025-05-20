namespace ConfluenceWebApi.Service;

// https://developer.atlassian.com/server/confluence/rest/v920/api-group-space/#api-group-space

internal sealed class ConfluenceService(Uri host, IAuthenticator? authenticator, string appName) : JsonService(host, authenticator, appName, SourceGenerationContext.Default)
{
    protected override string? AuthenticationTestUrl => null;

    protected override void InitializeClient(HttpClient client)
    {
        base.InitializeClient(client);
        client.DefaultRequestHeaders.Add("X-Atlassian-Token", "no-check");
                                         
        //client.DefaultRequestHeaders.MaxForwards = 5;
    }

    #region Content Labels

    public async Task DeleteLabelAsync(string id, string label, CancellationToken cancellationToken)
    {
        await DeleteAsync($"/rest/api/content/{id}/label?name={label}", cancellationToken);
    }

    #endregion
 
    #region Content Resource

    public IAsyncEnumerable<ContentModel> SearchContentAsync(string cql, string? expand, CancellationToken cancellationToken)
    {
        expand = string.IsNullOrEmpty(expand) ? "" : $"&expand={expand}";

        return GetYieldAsync<ContentModel>($"/rest/api/content/search?cql={cql}{expand}", cancellationToken);
    }

    #endregion

    #region Children

    //public async Task<ChildrenModel?> GetChildPagesAsync(int id, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<ChildrenModel>($"{prefix}/pages/{id}/children", cancellationToken);
    //    return res;
    //}

    //public async Task<ChildrenModel?> GetChildCustomContentAsync(int id, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<ChildrenModel>($"/rest/api/custom-content/{id}/children", cancellationToken);
    //    return res;
    //}

    #endregion

    #region Space

    public IAsyncEnumerable<SpaceModel> GetContentsInSpaceAsync(string spaceKey, CancellationToken cancellationToken)
    {
        return GetYieldAsync<SpaceModel>("/rest/api/space/{spaceKey}/content?expand=history", cancellationToken);
    }

    public async Task<SpaceModel?> GetSpaceAsync(string spaceKey, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<SpaceModel?>($"/rest/api/space/{spaceKey}", cancellationToken);
        return res;
    }

    //public async Task<List<SpaceModel>?> GetSpacesAsync(CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<List<SpaceModel>>($"{prefix}/spaces", cancellationToken);
    //    return res;
    //}

    //public async Task<SpaceModel?> CreateSpaceAsync(SpaceModel space,CancellationToken cancellationToken)
    //{
    //    var res = await PostAsJsonAsync<SpaceModel, SpaceModel>($"{prefix}/spaces", space, cancellationToken);
    //    return res;
    //}

    //public async Task<SpaceModel?> GetSpaceAsync(int id, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<SpaceModel>($"{prefix}/spaces/{id}", cancellationToken);
    //    return res;
    //}

    #endregion

    #region Folder

    //public async Task<FolderModel?> CreateFolderAsync(FolderModel folder, CancellationToken cancellationToken)
    //{
    //    var res = await PostAsJsonAsync<FolderModel, FolderModel>($"{prefix}/folders", folder, cancellationToken);
    //    return res;
    //}

    //public async Task<FolderModel?> GetFolderAsync(int id, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<FolderModel>($"{prefix}/folders/{id}", cancellationToken);
    //    return res;
    //}

    //public async Task DeleteFolderAsync(int id, CancellationToken cancellationToken)
    //{
    //    await DeleteAsync($"{prefix}/folders/{id}", cancellationToken);
    //}
    #endregion

    #region User

    public async Task<UserModel?> GetCurrentUser(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("/rest/api/user/current", cancellationToken);
        return res;
    }

    #endregion

    #region Export

    // https://support.atlassian.com/confluence/kb/rest-api-to-export-and-download-a-page-in-pdf-format/

    public async Task ExportPdfAsync(int pageId, string filePath, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));
        ArgumentNullException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

        await DownloadLocationAsync($"spaces/flyingpdf/pdfpageexport.action?pageId={pageId}", filePath, cancellationToken);
    }

    public async Task<Stream> ExportPdfStreamAsync(int pageId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));

        return await GetFromStreamAsync($"spaces/flyingpdf/pdfpageexport.action?pageId={pageId}", cancellationToken);
    }

    public async Task ExportWordAsync(int pageId, string filePath, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));
        ArgumentNullException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

        await DownloadLocationAsync($"exportword?pageId={pageId}", filePath, cancellationToken);
    }

    public async Task<Stream> ExportWordStreamAsync(int pageId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));

        return await GetFromStreamAsync($"exportword?pageId={pageId}", cancellationToken);
    }

    #endregion

    private async IAsyncEnumerable<T> GetYieldAsync<T>(string requestUri, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where T : class
    {
        string uri = requestUri;
        int start = 0;
        while (true)
        { 
            ListModel<T>? resp = await GetFromJsonAsync<ListModel<T>>(uri, cancellationToken, memberName);
            foreach (T item in resp!.Results!)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }
                yield return item;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            if (resp.Size == 0)
            {
                yield break;
            }

            start += resp.Size;
            uri = requestUri + (requestUri.Contains('?') ? "&" : "?") + $"limit={resp.Limit}&start={start}";
        }
    }

}