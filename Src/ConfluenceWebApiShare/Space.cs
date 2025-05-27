namespace ConfluenceWebApi;

/// <summary>
/// Represents a Confluence space with its basic properties.
/// </summary>
public class Space
{
    public Space()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Space"/> class using the specified <see cref="SpaceModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="SpaceModel"/> containing space data.</param>
    internal Space(SpaceModel model)
    {
        Id = model.Id;
        Key = model.Key;
        Name = model.Name;
        Status = model.Status;
        Icon = model.Icon.CastModel<Icon>();
        Description = model.Description.CastModel<Description>();
        Homepage = model.Homepage.CastModel<Content>();
        Type = model.Type;
        Metadata = model.Metadata.CastModel<Metadata>(); 
    }

    internal SpaceModel ToModel()
    {
        return new SpaceModel
        {
            Key = Key
        };
    }


    /// <summary>
    /// Gets or sets the unique identifier of the space.
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// Gets or sets the key of the space.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the space.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the status of the space.
    /// </summary>
    public Statuses? Status { get; set; }

    public Icon? Icon { get; set; }

    public Description? Description { get; set; }

    public Content? Homepage { get; set; }

    public Types? Type { get; set; }

    public Metadata? Metadata { get; set; }
}
