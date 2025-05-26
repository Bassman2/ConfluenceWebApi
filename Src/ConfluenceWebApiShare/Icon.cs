namespace ConfluenceWebApi;

public class Icon
{
    internal Icon(IconModel model)
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
