namespace ConfluenceWebApi;

/// <summary>
/// Represents a label associated with a Confluence content item, including its prefix, name, identifier, and display text.
/// </summary>
[DebuggerDisplay("{Id}: {Prefix} - {Name} - {Text}")]
public class Label
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Label"/> class.
    /// </summary>
    public Label()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Label"/> class using the specified model.
    /// </summary>
    /// <param name="model">The model containing label data.</param>
    internal Label(LabelModel model)
    {
        Prefix = model.Prefix;
        Name = model.Name;
        Id = model.Id;
        Text = model.Text;
    }
    
    /// <summary>
    /// Gets or sets the prefix of the label (e.g., "global", "my").
    /// </summary>
    public string Prefix { get; set; } = "";

    /// <summary>
    /// Gets or sets the name of the label.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the unique identifier of the label.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Gets or sets the display text of the label.
    /// </summary>
    public string Text { get; set; } = "";
}
