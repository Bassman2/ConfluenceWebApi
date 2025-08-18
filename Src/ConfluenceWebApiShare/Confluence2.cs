namespace ConfluenceWebApi;

/// <summary>
/// Represents a client for interacting with the Confluence API.
/// </summary>
public sealed class Confluence2 : JsonService
{
    public Confluence2(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public Confluence2(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    protected override string? AuthenticationTestUrl => null;

    protected override void InitializeClient(HttpClient client)
    {
        base.InitializeClient(client);
        client.DefaultRequestHeaders.Add("X-Atlassian-Token", "no-check");
    }

    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        string res = response.Content.ReadAsStringAsync(cancellationToken).Result;
        if (res.StartsWith('{'))
        {
            JsonTypeInfo<ErrorModel> jsonTypeInfoOut = (JsonTypeInfo<ErrorModel>)context.GetTypeInfo(typeof(ErrorModel))!;
            var error = await response.Content.ReadFromJsonAsync<ErrorModel>(jsonTypeInfoOut, cancellationToken);
            //res = error?.ToString() ?? "Unknown";
            throw new WebServiceException(error?.Message, response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
        }
        throw new WebServiceException(res, response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
    }

    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ManifestModel>("rest/applinks/1.0/manifest", cancellationToken);
        return res != null ? $"{res.Version}.{res.BuildNumber}" : "0.0.0";
    }

    #region Access Mode

    /// <summary>
    /// Gets the current access mode status of the Confluence instance.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The access mode status as a string, or <c>null</c> if unavailable.</returns>
    public async Task<string?> GetAccessModeStatusAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetStringAsync("/rest/api/accessmode", cancellationToken);
        return res?.Trim('"');
    }

    #endregion

    // Admin Group

    // Admin User

    #region Attachments

    /// <summary>
    /// Retrieves attachments for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item whose attachments are to be retrieved.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "version", "container").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects associated with the specified content item.
    /// </returns>
    public async IAsyncEnumerable<Content> GetAttachmentAsync(string id, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    /// <summary>
    /// Creates one or more attachments for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item to which the files will be attached.</param>
    /// <param name="files">A collection of key-value pairs where the key is the file name and the value is the file stream to upload.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "version", "container").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="Content"/> object representing the created attachment, or <c>null</c> if creation failed.
    /// </returns>
    public async Task<IEnumerable<Content>?> CreateAttachmentAsync(string id, IEnumerable<KeyValuePair<string, System.IO.Stream>> files, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        var res = await PostFilesFromJsonAsync<ResultListModel<ContentModel>>(req, files, cancellationToken);
        return res?.Results.CastModel<Content>();
    }

    public async Task<IEnumerable<Content>?> CreateAttachmentAsync(string id, IEnumerable<(string fileName, string filePath)> files, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var list = files.Select(f => new KeyValuePair<string, Stream>(f.fileName, File.OpenRead(f.filePath))).ToList();
        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        var res = await PostFilesFromJsonAsync<ResultListModel<ContentModel>>(req, list, cancellationToken);
        list.ForEach(f => f.Value.Close());
        return res?.Results.CastModel<Content>();
    }

    public async Task<IEnumerable<Content>?> CreateAttachmentAsync(string id, string fileName, string filePath, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        Stream stream = File.OpenRead(filePath);
        List<KeyValuePair<string, Stream>> list = [new(fileName, stream)];

        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        var res = await PostFilesFromJsonAsync<ResultListModel<ContentModel>>(req, list, cancellationToken);

        stream.Close();
        return res?.Results.CastModel<Content>();
    }

    public async Task<IEnumerable<Content>?> CreateAttachmentAsync(string id, string fileName, Stream fileStream, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        List<KeyValuePair<string, Stream>> list = [new(fileName, fileStream)];

        var req = CombineUrl("rest/api/content", id, "child/attachment", ("expand", expand));
        var res = await PostFilesFromJsonAsync<ResultListModel<ContentModel>>(req, list, cancellationToken);

        return res?.Results.CastModel<Content>();
    }

    /// <summary>
    /// <summary>
    /// Moves an attachment to a new content item and/or renames it in Confluence.
    /// </summary>
    /// <param name="id">The unique identifier of the current content item containing the attachment.</param>
    /// <param name="attachmentId">The unique identifier of the attachment to move.</param>
    /// <param name="newName">The new name for the attachment.</param>
    /// <param name="newContentId">The unique identifier of the target content item to which the attachment will be moved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous move operation.</returns>
    public async Task MoveAttachmentAsync(string id, string attachmentId, string newName, string newContentId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId, "/move", ("newName", newName), ("newContentId", newContentId));
        await PostAsync(req, cancellationToken);
    }

    /// <summary>
    /// Removes an attachment from a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item containing the attachment.</param>
    /// <param name="attachmentId">The unique identifier of the attachment to remove.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous remove operation.
    /// </returns>
    public async Task RemoveAttachmentAsync(string id, string attachmentId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId);
        await DeleteAsync(req, cancellationToken);
    }

    /// <summary>
    /// Removes a specific version of an attachment from a Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item containing the attachment.</param>
    /// <param name="attachmentId">The unique identifier of the attachment.</param>
    /// <param name="version">The version identifier of the attachment to remove.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous remove operation.
    /// </returns>
    public async Task RemoveAttachmentVersionAsync(string id, string attachmentId, string version, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/content/", id, "child/attachment/", attachmentId, "version", version);
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Backup and Restore

    // Category


    #region Child Content

    /// <summary>
    /// Retrieves the direct child content items for a specified Confluence content item and version.
    /// </summary>
    /// <param name="id">The unique identifier of the parent content item whose children are to be retrieved.</param>
    /// <param name="parentVersion">The version of the parent content item for which to retrieve children.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "children", "ancestors").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects representing the direct children of the specified content item.
    /// </returns>
    public async Task<Children?> GetChildrenOfContentAsync(string id, int? parentVersion = null, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "child", ("parentVersion", parentVersion), ("expand", expand));
        var res = await GetFromJsonAsync<ChildrenModel>(req, cancellationToken);
        return res.CastModel<Children>();
    }

    /// <summary>
    /// Retrieves the direct child content items of a specific type for a specified Confluence content item and version.
    /// </summary>
    /// <param name="id">The unique identifier of the parent content item whose children are to be retrieved.</param>
    /// <param name="type">The type of child content to retrieve (e.g., "page", "blogpost").</param>
    /// <param name="parentVersion">The version of the parent content item for which to retrieve children.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "children", "ancestors").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects representing the direct children of the specified type.
    /// </returns>
    public async IAsyncEnumerable<Content> GetChildrenOfContentByTypeAsync(string id, string type, int parentVersion, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "child", type, ("parentVersion", parentVersion), ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);

        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    /// <summary>
    /// Retrieves the comments for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item for which to retrieve comments.</param>
    /// <param name="depth">The depth of comments to retrieve (e.g., "all", "root").</param>
    /// <param name="location">Optional. The location filter for comments (e.g., inline, footer). If <c>null</c>, all locations are included.</param>
    /// <param name="parentVersion">The version of the parent content item for which to retrieve comments.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects representing the comments associated with the specified content item.
    /// </returns>
    public async IAsyncEnumerable<Content> GetCommentsOfContentAsync(string id, string depth, Locations? location, int parentVersion, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "child/comment", ("depth", depth), ("location", location), ("parentVersion", parentVersion), ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    #endregion

    // Content Blueprint
    // Content Body

    #region Content Descendant

    /// <summary>
    /// Retrieves all descendant content items for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the parent content item whose descendants are to be retrieved.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "children", "ancestors").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects representing the descendant content items.
    /// </returns>
    public async IAsyncEnumerable<Content> GetDescendantsAsync(string id, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "descendant", ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    /// <summary>
    /// Retrieves all descendant content items of a specific type for a given Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the parent content item whose descendants are to be retrieved.</param>
    /// <param name="type">The type of descendant content to retrieve (e.g., "page", "blogpost").</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "children", "ancestors").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects representing the descendant content items of the specified type.
    /// </returns>
    public async IAsyncEnumerable<Content> GetDescendantsOfTypeAsync(string id, string type, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "descendant", type, ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    #endregion

    #region Content Labels

    /// <summary>
    /// Retrieves the labels associated with a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item for which to retrieve labels.</param>
    /// <param name="prefix">Optional. A prefix to filter labels (e.g., "global", "my"). If <c>null</c>, all labels are returned.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Label"/> objects associated with the specified content item.
    /// </returns>
    public async IAsyncEnumerable<Label> GetLabelsAsync(string id, string? prefix = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "label", ("prefix", prefix));
        var res = GetResultListYieldAsync<LabelModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Label>()!;
        }
    }

    /// <summary>
    /// Adds a label to a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item to which the label will be added.</param>
    /// <param name="label">The <see cref="Label"/> object representing the label to add.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Label"/> objects representing the labels added to the content item.
    /// </returns>
    public async IAsyncEnumerable<Label> AddLabelsAsync(string id, Label label, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "label");
        var res = PostResultListYieldAsync<LabelModel, LabelModel>(req, label, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Label>()!;
        }
    }

    /// <summary>
    /// Deletes a label from a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item from which the label will be deleted.</param>
    /// <param name="name">The name of the label to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    public async Task DeleteLabelAsync(string id, string name, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "label", ("name", name));
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    #region Content Resource

    /// <summary>
    /// Retrieves content items from a specified Confluence space, optionally filtered by posting date, title, type, or status.
    /// </summary>
    /// <param name="spaceKey">The key of the Confluence space to query.</param>
    /// <param name="postingDay">Optional. The posting date to filter content items.</param>
    /// <param name="title">Optional. The title to filter content items.</param>
    /// <param name="type">Optional. The type of content to retrieve (e.g., "page", "blogpost").</param>
    /// <param name="status">Optional. The status of the content (e.g., "current", "draft").</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects matching the specified criteria, or <c>null</c> if no content is found.
    /// </returns>
    public async IAsyncEnumerable<Content?> GetContentAsync(string spaceKey, DateTime? postingDay = null, string? title = null, string? type = null, string? status = null, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", ("spaceKey", spaceKey), ("postingDay", postingDay?.ToString("yyyy-MM-dd")), ("title", title), ("type", type), ("status", status), ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    public async Task<Content?> CreateContentAsync(Content content, string? status = null, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", ("status", status), ("expand", expand));
        var res = await PostAsJsonAsync<ContentModel, ContentModel>(req, content.ToModel(), cancellationToken);
        return res.CastModel<Content>();
    }

    public async Task<Content?> CreatePageAsync(string spaceKey, string title, string htmlPage, string? ancestorsId = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var content = new Content()
        {
            Type = Types.page,
            Title = title,
            Ancestors = ancestorsId == null ? null : [new() { Id = ancestorsId }],
            Space = new Space() { Key = spaceKey },
            Body = new Body() 
            {  
                Storage = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
                //Editor = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
                //View = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
                //ExportView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
                //AnonymousExportView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
                //StyledView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.storage },
            }

        };
        var req = CombineUrl("rest/api/content");
        var res = await PostAsJsonAsync<ContentModel, ContentModel>(req, content.ToModel(), cancellationToken);
        return res.CastModel<Content>();
    }
    
    /// <summary>
    /// Retrieves a specific content item from Confluence by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the content item.</param>
    /// <param name="version">Optional. The version of the content to retrieve. If <c>null</c>, the latest version is returned.</param>
    /// <param name="status">Optional. The status of the content (e.g., "current", "draft").</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Content"/> object representing the requested content item, or <c>null</c> if not found.
    /// </returns>
    public async Task<Content?> GetContentByIdAsync(string id, string? version = null, string? status = null, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, ("version", version), ("status", status), ("expand", expand));
        var res = await GetFromJsonAsync<ContentModel>(req, cancellationToken);
        return res.CastModel<Content>();
    }

    /// <summary>
    /// Deletes a specific content item from Confluence by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the content item to delete.</param>
    /// <param name="status">Optional. The status of the content to delete (e.g., "current", "draft"). If <c>null</c>, the default status is used.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteContentAsync(string id, string? status = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, ("status", status));
        await DeleteAsync(req, cancellationToken);
    }

    /// <summary>
    /// Searches for content in Confluence using a CQL (Confluence Query Language) query.
    /// </summary>
    /// <param name="cql">The CQL query string to filter content.</param>
    /// <param name="cqlcontext">Optional. The context in which to execute the CQL query (e.g., space or user context).</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Content"/> objects that match the specified CQL query.
    /// </returns>
    public async IAsyncEnumerable<Content> SearchContentAsync(string cql, string? cqlcontext = null, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/content/search", ("cql", cql), ("cqlcontext", cqlcontext), ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    #endregion

    // Content Restrictions

    #region Content Version

    /// <summary>
    /// Deletes a specific version from the history of a Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item whose version history will be modified.</param>
    /// <param name="versionNumber">The version number of the content history to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    public async Task DeleteContentHistoryAsync(string id, string versionNumber, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("rest/api/content", id, "version", versionNumber);
        await DeleteAsync(req, cancellationToken);
    }

    #endregion

    // Content Watchers
    // GlobalColorScheme
    // Group

    #region Instance Metrics

    /// <summary>
    /// Retrieves instance metrics for a specified Confluence content item.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="InstanceMetrics"/> object with the instance metrics, or <c>null</c> if not found.
    /// </returns>
    public async Task<InstanceMetrics?> GetInstanceMetricsAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res =  await GetFromJsonAsync<InstanceMetricsModel>("rest/api/instance-metrics", cancellationToken);
        return res.CastModel<InstanceMetrics>();
    }

    #endregion

    // Label
    // Long Task
    // Search
    // Server Information

    #region Space

    public async Task<Content?> GetRootContentInSpaceAsync(string spaceKey, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/space", spaceKey, "content", ("depth", Depth.Root), ("expand", expand));
        var res = await GetFromJsonAsync<PageListModel<ContentModel>>(req, cancellationToken);
        return res?.Page?.Results?.FirstOrDefault().CastModel<Content>();
    }

    /// <summary>
    /// Retrieves all content in a specified Confluence space.
    /// </summary>
    /// <param name="spaceKey">The key of the space.</param>
    /// <param name="depth">The depth of content to retrieve (e.g., all or root).</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="Content"/> objects in the space.</returns>
    public async IAsyncEnumerable<Content> GetContentsInSpaceAsync(string spaceKey, Depth depth = Depth.All, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/space", spaceKey, "content", ("depth", depth), ("expand", expand));
        var res = GetPageListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    /// <summary>
    /// Retrieves content of a specific type in a Confluence space.
    /// </summary>
    /// <param name="spaceKey">The key of the space.</param>
    /// <param name="type">The type of content to retrieve (e.g., page, blog post).</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="Content"/> objects of the specified type.</returns>
    public async IAsyncEnumerable<Content> GetContentsByTypeAsync(string spaceKey, Types type = Types.page, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);


        var req = CombineUrl("/rest/api/space", spaceKey, "content/page", ("type", type), ("expand", expand));
        var res = GetResultListYieldAsync<ContentModel>(req, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    /// <summary>
    /// Retrieves information about a specific Confluence space.
    /// </summary>
    /// <param name="spaceKey">The key of the space.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Space"/> object representing the space, or <c>null</c> if not found.</returns>
    public async Task<Space?> GetSpaceAsync(string spaceKey, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl("/rest/api/space", spaceKey, ("expand", expand));
        var res = await GetFromJsonAsync<SpaceModel>(req, cancellationToken);
        return res.CastModel<Space>(); 
    }

    /// <summary>
    /// Deletes a Confluence space by its key.
    /// </summary>
    /// <param name="spaceKey">The key of the space to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task DeleteSpaceAsync(string spaceKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

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

    /// <summary>
    /// Retrieves information about the current authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="User"/> object representing the current user, or <c>null</c> if not found.</returns>
    public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>("/rest/api/user/current", cancellationToken);
        return res.CastModel<User>();
    }

    #endregion

    // User Group

    #region Export

    /// <summary>
    /// Exports a Confluence page as a PDF file to the specified file path.
    /// </summary>
    /// <param name="pageId">The ID of the page to export.</param>
    /// <param name="filePath">The file path to save the exported PDF.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task ExportPdfAsync(int pageId, string filePath, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

        await DownloadLocationAsync($"spaces/flyingpdf/pdfpageexport.action?pageId={pageId}", filePath, cancellationToken);
    }

    /// <summary>
    /// Exports a Confluence page as a PDF and returns the result as a stream.
    /// </summary>
    /// <param name="pageId">The ID of the page to export.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Stream"/> containing the exported PDF data.</returns>
    public async Task<Stream> ExportPdfStreamAsync(int pageId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        return await GetFromStreamAsync($"spaces/flyingpdf/pdfpageexport.action?pageId={pageId}", cancellationToken);
    }

    public async Task ExportWordAsync(int pageId, string filePath, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));
        ArgumentNullException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

        await DownloadLocationAsync($"exportword?pageId={pageId}", filePath, cancellationToken);
    }

    public async Task<Stream> ExportWordStreamAsync(int pageId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageId, nameof(pageId));

        return await GetFromStreamAsync($"exportword?pageId={pageId}", cancellationToken);
    }

    #endregion

    #region private

    protected override string QueryEntry((string Name, object? Value) entry)
    {
        if (entry.Value?.GetType() == typeof(Expand))
        {
            if (entry.Value == null) return "";

            if (entry.Value is Expand expand)
            {
                return $"{entry.Name}={expand}";
            }
        }
        return base.QueryEntry(entry);
    }

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
}