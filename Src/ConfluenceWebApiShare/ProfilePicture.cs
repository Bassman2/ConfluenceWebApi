namespace ConfluenceWebApi;

public class ProfilePicture
{
    internal ProfilePicture(ProfilePictureModel model)
    {
        Path = model.Path;
        Width = model.Width;
        Height = model.Height;
        IsDefault = model.IsDefault;
    }

    public string Path { get; set; } = null!;

    public int Width { get; set; }

    public int Height { get; set; }

    public bool IsDefault { get; set; }
}
