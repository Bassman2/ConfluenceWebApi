using System.ComponentModel;
using WebServiceClient;

namespace ConfluenceWebApi;

/// <summary>
/// Represents a content item in Confluence, including its metadata and associated space.
/// </summary>
[DebuggerDisplay("{Id}: {Title} - {Status}")]
public class Content
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Content"/> class using the specified <see cref="ContentModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="ContentModel"/> containing content data.</param>
    internal Content(ContentModel model)
    {
        Id = model.Id;
        Type = model.Type;
        Status = model.Status;
        Title = model.Title;
        Space = model.Space.CastModel<Space>();
        History = model.History.CastModel<History>();
        Version = model.Version.CastModel<Version>();
        Ancestors = model.Ancestors.CastModel<Content>();
        Position = model.Position;
        Operations = model.Operations.CastModel<Operation>();
        Children = model.Children.CastModel<Children>();
        Descendants = model.Descendants.CastModel<Descendants>();
        Container = model.Container.CastModel<Container>();
        Body = model.Body.CastModel<Body>();
        Metadata = model.Metadata.CastModel<Metadata>();
        Extensions = model.Extensions.CastModel<Extensions>();
        Restrictions = model.Restrictions.CastModel<Restrictions>();
    }

    /// <summary>
    /// Gets or sets the unique identifier of the content item.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the content (e.g., "page", "blogpost").
    /// </summary>
    public Types Type { get; set; }

    /// <summary>
    /// Gets or sets the status of the content (e.g., "current", "archived").
    /// </summary>
    public Statuses Status { get; set; }

    /// <summary>
    /// Gets or sets the title of the content.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the space to which this content belongs.
    /// </summary>
    public Space? Space { get; set; }

    public History? History { get; set; }

    
    public Version? Version { get; set; }


    public List<Content>? Ancestors { get; set; }

    
    public int Position { get; set; }

    
    public List<Operation>? Operations { get; set; }

    public Children? Children { get; set; }

    public Descendants? Descendants { get; set; }

    
    public Container? Container { get; set; }

    
    public Body? Body { get; set; }

    
    public Metadata? Metadata { get; set; }

    
    public Extensions? Extensions { get; set; }

    public Restrictions? Restrictions { get; set; }
}
