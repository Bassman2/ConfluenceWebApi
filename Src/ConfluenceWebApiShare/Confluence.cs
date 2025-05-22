using System.Reflection.Emit;

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

    public async IAsyncEnumerable<Attachment> GetAttachmentAsync(string id, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetAttachmentAsync(id, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Attachment>()!;
        }
    }

    public async Task<Attachment?> CreateAttachmentAsync(string id, IEnumerable<KeyValuePair<string, System.IO.Stream>> files, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.CreateAttachmentAsync(id, files, expand, cancellationToken);
        return res.CastModel<Attachment>();
    }

    public async Task MoveAttachmentAsync(string id, string attachmentId, string newName, string newContentId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.MoveAttachmentAsync(id, attachmentId, newName, newContentId, cancellationToken);
    }

    public async Task RemoveAttachmentAsync(string id, string attachmentId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await RemoveAttachmentAsync(id, attachmentId, cancellationToken);
    }

    public async Task RemoveAttachmentVersionAsync(string id, string attachmentId, string version, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await RemoveAttachmentVersionAsync(id, attachmentId, version, cancellationToken);
    }

    #endregion

    #region Child Content

    public async IAsyncEnumerable<Content> GetChildrenOfContentAsync(string id, int parentVersion, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetChildrenOfContentAsync(id, parentVersion, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    public async IAsyncEnumerable<Content> GetChildrenOfContentByTypeAsync(string id, string type, int parentVersion, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetChildrenOfContentByTypeAsync(id, type, parentVersion, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

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

    public async IAsyncEnumerable<Content> GetDescendantsAsync(string id, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetDescendantsAsync(id, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

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

    public async IAsyncEnumerable<Label> GetLabelsAsync(string id, string? prefix = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetLabelsAsync(id, prefix, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Label>()!;
        }
    }

    public async IAsyncEnumerable<Label> AddLabelsAsync(string id, Label label, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.AddLabelsAsync(id, label, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Label>()!;
        }
    }

    public async Task DeleteLabelAsync(string id, string name, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteLabelAsync(id, name, cancellationToken);
    }

    #endregion

    #region Content Resource

    public async IAsyncEnumerable<Content?> GetContentAsync(string spaceKey, DateTime? postingDay = null, string? title = null, string? type = null, string? status = null, string? expand = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = service.GetContentAsync(spaceKey, postingDay, title, type, status, expand, cancellationToken);
        await foreach (var item in res)
        {
            yield return item.CastModel<Content>()!;
        }
    }

    public async Task<Content?> GetContentByIdAsync(string id, string? version = null, string? status = null, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetContentByIdAsync(id, version, status, expand, cancellationToken);
        return res.CastModel<Content>();
    }

    public async Task DeleteContentAsync(string id, string? status = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteContentAsync(id, status, cancellationToken);
    }

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

    public async Task DeleteContentHistoryAsync(string id, string versionNumber, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        await service.DeleteContentHistoryAsync(id, versionNumber, cancellationToken);
    }

    #endregion

    #region Instance Metrics

    public async Task<InstanceMetrics?> GetInstanceMetricsAsync(string id, string? version = null, string? status = null, string? expand = null, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(service);

        var res = await service.GetInstanceMetricsAsync(id, version, status, expand, cancellationToken);
        return res.CastModel<InstanceMetrics>();
    }

    #endregion

    #region Space

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