namespace ConfluenceWebApi;

/// <summary>
/// Represents a user's profile picture, including its path, dimensions, and default status.
/// </summary>
[Model]
public class ProfilePicture
{
    internal ProfilePicture()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfilePicture"/> class using the specified <see cref="ProfilePictureModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="ProfilePictureModel"/> containing profile picture data.</param>
    internal ProfilePicture(ProfilePictureModel model)
    {
        Path = model.Path;
        Width = model.Width;
        Height = model.Height;
        IsDefault = model.IsDefault;
    }

    /// <summary>
    /// Gets or sets the path to the profile picture image.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Gets or sets the width of the profile picture in pixels.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the profile picture in pixels.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this profile picture is the default image.
    /// </summary>
    public bool IsDefault { get; set; }
}
