namespace ConfluenceWebApi;

public class Extensions
{
    internal Extensions(ExtensionsModel model)
    {
        MediaType = model.MediaType;
        FileSize = model.FileSize;
        Comment = model.Comment;
    }

    public string? MediaType { get; set; }

    public long? FileSize { get; set; }

    public string? Comment { get; set; }
}
