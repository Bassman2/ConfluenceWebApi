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

    //protected override async Task ErrorCheckAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    if (!response.IsSuccessStatusCode)
    //    {
    //        await ErrorHandlingAsync(response, memberName, cancellationToken);
    //    }
    //}

    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        JsonTypeInfo<ErrorModel> jsonTypeInfoOut = (JsonTypeInfo<ErrorModel>)context.GetTypeInfo(typeof(ErrorModel))!;
        var error = await response.Content.ReadFromJsonAsync<ErrorModel>(jsonTypeInfoOut, cancellationToken);
        
        throw new WebServiceException(error?.ToString(), response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
    }

    #region Access Mode

    public async Task<string?> GetAccessModeStatusAsync(CancellationToken cancellationToken)
    {
        var res = await GetStringAsync("/rest/api/accessmode", cancellationToken);
        return res;
    }

    #endregion

    // Admin Group

    // Admin User

    #region Attachments

    public IAsyncEnumerable<AttachmentModel?> GetAttachmentAsync(string id, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        return GetResultListYieldAsync<AttachmentModel>(req, cancellationToken);
    }

    public async Task<AttachmentModel?> CreateAttachmentAsync(string id, IEnumerable<KeyValuePair<string, System.IO.Stream>> files, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        return await PostFilesFromJsonAsync<AttachmentModel>(req, files, cancellationToken);
    }

    public async Task MoveAttachmentAsync(string id, string attachmentId, string newName, string newContentId, CancellationToken cancellationToken)
    {
        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId, "/move", ("newName", newName), ("newContentId", newContentId));
        await PostAsync(req, cancellationToken);
    }

    public async Task RemoveAttachmentAsync(string id, string attachmentId, CancellationToken cancellationToken)
    {
        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId);
        await DeleteAsync(req, cancellationToken);
    }

    public async Task RemoveAttachmentVersionAsync(string id, string attachmentId, string version, CancellationToken cancellationToken)
    {
        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId, "version", version);
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Backup and Restore

    // Category

    #region Child Content

    public IAsyncEnumerable<ContentModel?> GetChildrenOfContentAsync(string id, int? parentVersion, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "child", ("parentVersion", parentVersion), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    public IAsyncEnumerable<ContentModel?> GetChildrenOfContentByTypeAsync(string id, string type, int parentVersion, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "child", type, ("parentVersion", parentVersion), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    public IAsyncEnumerable<ContentModel?> GetCommentsOfContentAsync(string id, string depth, Locations? location, int parentVersion, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "child/comment", ("depth", depth), ("location", location), ("parentVersion", parentVersion), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    #endregion



    // Content Blueprint
    // Content Body

    #region Content Descendant

    public IAsyncEnumerable<ContentModel?> GetDescendantsAsync(string id, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "descendant", ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    public IAsyncEnumerable<ContentModel?> GetDescendantsOfTypeAsync(string id, string type, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "descendant", type, ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    #endregion

    #region Content Labels

    public IAsyncEnumerable<LabelModel> GetLabelsAsync(string id, string? prefix, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "label", ("prefix", prefix));
        return GetResultListYieldAsync<LabelModel>(req, cancellationToken);
    }

    public IAsyncEnumerable<LabelModel> AddLabelsAsync(string id, LabelModel label, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "label");
        return PostResultListYieldAsync<LabelModel, LabelModel>(req, label, cancellationToken);
    }
    
    public async Task DeleteLabelAsync(string id, string name, CancellationToken cancellationToken)
    {
        //var req = CombineUrl("rest/api/content", id, "label", name));
        var req = CombineUrl("rest/api/content", id, "label", ("name", name));
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Content Property

    #region Content Resource

    public IAsyncEnumerable<ContentModel?> GetContentAsync(string spaceKey, DateTime? postingDay, string? title, string? type, string? status, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", ("spaceKey", spaceKey), ("postingDay", postingDay?.ToString("yyyy-MM-dd")), ("title", title), ("type", type), ("status", status), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    public async Task<ContentModel?> GetContentByIdAsync(string id, string? version, string? status, string? expand, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, ("version", version), ("status", status), ("expand", expand));
        return await GetFromJsonAsync<ContentModel>(req, cancellationToken);
    }

    public async Task DeleteContentAsync(string id, string? status, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, ("status", status));
        await DeleteAsync(req, cancellationToken);
    }

    public IAsyncEnumerable<ContentModel> SearchContentAsync(string cql, string? cqlcontext, string? expand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cql, nameof(cql));

        var req = CombineUrl("/rest/api/content/search", ("cql", cql), ("cqlcontext", cqlcontext), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    #endregion


    // Content Restrictions

    #region Content Version

    public async Task DeleteContentHistoryAsync(string id, string versionNumber, CancellationToken cancellationToken)
    {
        var req = CombineUrl("rest/api/content", id, "version", versionNumber);
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Content Watchers
    // GlobalColorScheme
    // Group

    #region Instance Metrics

    public async Task<InstanceMetricsModel?> GetInstanceMetricsAsync(string id, string? version, string? status, string? expand, CancellationToken cancellationToken)
    {
        return await GetFromJsonAsync<InstanceMetricsModel>("rest/api/instance-metrics", cancellationToken);
    }

    #endregion

    // Label
    // Long Task
    // Search
    // Server Information

    #region Space

    public IAsyncEnumerable<ContentModel> GetContentsInSpaceAsync(string spaceKey, Depth depth, string? expand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(spaceKey, nameof(spaceKey));

        var req = CombineUrl("/rest/api/space", spaceKey, "content", ("depth", depth), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }


    public IAsyncEnumerable<ContentModel> GetContentsByTypeAsync(string spaceKey, Types type, string? expand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(spaceKey, nameof(spaceKey));

        var req = CombineUrl("/rest/api/space", spaceKey, "content/page", ("type", type), ("expand", expand));
        return GetResultListYieldAsync<ContentModel>(req, cancellationToken);
    }

    public async Task<SpaceModel?> GetSpaceAsync(string spaceKey, string? expand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(spaceKey, nameof(spaceKey));

        var req = CombineUrl("/rest/api/space", spaceKey, ("expand", expand));
        var res = await GetFromJsonAsync<SpaceModel>(req, cancellationToken);
        return res;

    }

    public async Task DeleteSpaceAsync(string spaceKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(spaceKey, nameof(spaceKey));

        var req = CombineUrl("/rest/api/space", spaceKey);
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Space Label
    // Space Permissions
    // Space Property
    // Space Watchers
    // SpaceColorScheme

    #region User

    public async Task<UserModel?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("/rest/api/user/current", cancellationToken);
        return res;
    }

    #endregion

    // User Group

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

    #region private

    private async IAsyncEnumerable<OUT> GetPageListYieldAsync<OUT>(string requestUri, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where OUT : class
    {
        string uri = requestUri;
        int start = 0;
        while (true)
        { 
            PageListModel<OUT>? resp = await GetFromJsonAsync<PageListModel<OUT>>(uri, cancellationToken, memberName);
            foreach (OUT item in resp!.Page!.Results!)
            {
                if (cancellationToken.IsCancellationRequested) yield break;
                yield return item;
            }
            
            if (resp!.Page!.Size < resp!.Page!.Limit) yield break;
            
            start += resp!.Page!.Size;

            if (resp!.Page!.Size != resp!.Page.Results.Count)
            {
                throw new Exception("XXXXXXXXXXXXXX");
            }

            uri = requestUri + (requestUri.Contains('?') ? "&" : "?") + $"limit={resp.Page.Limit}&start={start}";
        }        
    }

    private async IAsyncEnumerable<OUT> GetResultListYieldAsync<OUT>(string requestUri, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where OUT : class
    {
        string uri = requestUri;
        int start = 0;
        while (true)
        {
            ResultListModel<OUT>? resp = await GetFromJsonAsync<ResultListModel<OUT>>(uri, cancellationToken, memberName);
            foreach (OUT item in resp!.Results!)
            {
                if (cancellationToken.IsCancellationRequested) yield break;
                yield return item;
            }

            if (resp!.Size < resp!.Limit) yield break;

            start += resp!.Size;

            if (resp!.Size != resp!.Results.Count)
            {
                throw new Exception("XXXXXXXXXXXXXX");
            }

            uri = requestUri + (requestUri.Contains('?') ? "&" : "?") + $"limit={resp.Limit}&start={start}";
        }
    }

    private async IAsyncEnumerable<OUT> PostResultListYieldAsync<IN, OUT>(string requestUri, IN obj, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where IN : class where OUT : class
    {
        string uri = requestUri;
        int start = 0;
        while (true)
        {
            ResultListModel<OUT>? resp = await PostAsJsonAsync<IN, ResultListModel<OUT>>(uri, obj, cancellationToken, memberName);
            foreach (OUT item in resp!.Results!)
            {
                if (cancellationToken.IsCancellationRequested) yield break;
                yield return item;
            }

            if (resp!.Size < resp!.Limit) yield break;

            start += resp!.Size;

            if (resp!.Size != resp!.Results.Count)
            {
                throw new Exception("XXXXXXXXXXXXXX");
            }

            uri = requestUri + (requestUri.Contains('?') ? "&" : "?") + $"limit={resp.Limit}&start={start}";
        }
    }

    #endregion


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

}