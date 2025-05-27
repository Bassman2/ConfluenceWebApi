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

    /// <summary>
    /// Gets the current access mode status of the Confluence instance.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The access mode status as a string, or <c>null</c> if unavailable.</returns>
    public async Task<string?> GetAccessModeStatusAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetAccessModeStatusAsync(cancellationToken);
        return res?.Trim('"');
    }

    #endregion

    #region Attachments

    /// <summary>
    /// Retrieves attachments for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item whose attachments are to be retrieved.</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response (e.g., "version", "container").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous stream of <see cref="Attachment"/> objects associated with the specified content item.
    /// </returns>
    public async IAsyncEnumerable<Attachment> GetAttachmentAsync(string id, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetAttachmentAsync(id, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Attachment>()!;
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
    /// A task that represents the asynchronous operation. The task result contains an <see cref="Attachment"/> object representing the created attachment, or <c>null</c> if creation failed.
    /// </returns>
    public async Task<Attachment?> CreateAttachmentAsync(string id, IEnumerable<KeyValuePair<string, System.IO.Stream>> files, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.CreateAttachmentAsync(id, files, expand, cancellationToken);
        return res.CastModel<Attachment>();
    }

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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.MoveAttachmentAsync(id, attachmentId, newName, newContentId, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await RemoveAttachmentAsync(id, attachmentId, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await RemoveAttachmentVersionAsync(id, attachmentId, version, cancellationToken);
    }

    #endregion

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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetChildrenOfContentAsync(id, parentVersion, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetChildrenOfContentByTypeAsync(id, type, parentVersion, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetCommentsOfContentAsync(id, depth, location, parentVersion, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    #endregion

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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetDescendantsAsync(id, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetDescendantsOfTypeAsync(id, type, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetLabelsAsync(id, prefix, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.AddLabelsAsync(id, label, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteLabelAsync(id, name, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentAsync(spaceKey, postingDay, title, type, status, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    public async Task<Content?> CreateContentAsync(Content content, string? status = null, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.CreateContentAsync(content.ToModel(), status, expand, cancellationToken);
        return res.CastModel<Content>();
    }

    public async Task<Content?> CreatePageAsync(string spaceKey, string title, string htmlPage, string? ancestorsId = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var content = new Content()
        {
            Type = Types.page,
            Title = title,
            Ancestors = ancestorsId == null ? null : [new() { Id = ancestorsId }],
            Space = new Space() { Key = spaceKey },
            Body = new Body() 
            {  
                Storage = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
                Editor = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
                View = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
                ExportView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
                AnonymousExportView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
                StyledView = new ValueRepresentation() { Value = htmlPage, Representation = Representations.Storage },
            }

        };
        var res = await service.CreateContentAsync(content.ToModel(), null, null, cancellationToken);
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
    public async Task<Content?> GetContentByIdAsync(string id, string? version = null, string? status = null, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetContentByIdAsync(id, version, status, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteContentAsync(id, status, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.SearchContentAsync(cql, cqlcontext, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    #endregion

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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteContentHistoryAsync(id, versionNumber, cancellationToken);
    }

    #endregion

    #region Instance Metrics

    /// <summary>
    /// Retrieves instance metrics for a specified Confluence content item.
    /// </summary>
    /// <param name="id">The unique identifier of the content item for which to retrieve instance metrics.</param>
    /// <param name="version">Optional. The version of the content to retrieve metrics for. If <c>null</c>, the latest version is used.</param>
    /// <param name="status">Optional. The status of the content (e.g., "current", "draft").</param>
    /// <param name="expand">Optional. A comma-separated list of properties to expand in the response.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="InstanceMetrics"/> object with the instance metrics, or <c>null</c> if not found.
    /// </returns>
    public async Task<InstanceMetrics?> GetInstanceMetricsAsync(string id, string? version = null, string? status = null, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetInstanceMetricsAsync(id, version, status, expand, cancellationToken);
        return res.CastModel<InstanceMetrics>();
    }

    #endregion

    #region Space

    public async Task<Content?> GetRootContentInSpaceAsync(string spaceKey, Expand? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetRootContentInSpaceAsync(spaceKey, expand, cancellationToken);
        return res.CastModel<Content>();
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentsInSpaceAsync(spaceKey, depth, expand, cancellationToken);
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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentsByTypeAsync(spaceKey, type, expand, cancellationToken);
        await foreach (var item in res)
        {
            //if (cancellationToken.IsCancellationRequested) yield break;

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
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetSpaceAsync(spaceKey, expand, cancellationToken);
        return res.CastModel<Space>(); 
    }

    /// <summary>
    /// Deletes a Confluence space by its key.
    /// </summary>
    /// <param name="spaceKey">The key of the space to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task DeleteSpaceAsync(string spaceKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteSpaceAsync(spaceKey, cancellationToken);
    }

    #endregion

    #region User

    /// <summary>
    /// Retrieves information about the current authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="User"/> object representing the current user, or <c>null</c> if not found.</returns>
    public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetCurrentUserAsync(cancellationToken);
        return res.CastModel<User>();
    }

    #endregion
    
    #region Export

    /// <summary>
    /// Exports a Confluence page as a PDF file to the specified file path.
    /// </summary>
    /// <param name="pageId">The ID of the page to export.</param>
    /// <param name="filePath">The file path to save the exported PDF.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public async Task ExportPdfAsync(int pageId, string filePath, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.ExportPdfAsync(pageId, filePath, cancellationToken);
    }

    /// <summary>
    /// Exports a Confluence page as a PDF and returns the result as a stream.
    /// </summary>
    /// <param name="pageId">The ID of the page to export.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Stream"/> containing the exported PDF data.</returns>
    public async Task<Stream> ExportPdfStreamAsync(int pageId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        return await service.ExportPdfStreamAsync(pageId, cancellationToken);
    }

    #endregion
}