namespace ConfluenceWebApi.Model;

/// <summary>
/// Represents a user's profile picture, including its path, dimensions, and default status.
/// </summary>
internal class ProfilePictureModel 
{
    /// <summary>
    /// Gets or sets the path to the profile picture image.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = null!;

    /// <summary>
    /// Gets or sets the width of the profile picture in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the profile picture in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this profile picture is the default image.
    /// </summary>
    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }
}

internal static class ProfilePictureCastX
{
    public static ProfilePicture CastToX(this ProfilePictureModel model)
    {
        return new ProfilePicture()
        {
            Path = model.Path,
            Width = model.Width,
            Height = model.Height,
            IsDefault = model.IsDefault,
        };
    }

    public static ProfilePictureModel CastFromX(this ProfilePicture item)
    {
        return new ProfilePictureModel()
        {
            Path = item.Path,
            Width = item.Width,
            Height = item.Height,
            IsDefault = item.IsDefault,
        };
    }
}