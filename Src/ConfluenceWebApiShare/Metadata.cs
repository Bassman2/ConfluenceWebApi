namespace ConfluenceWebApi;

public class Metadata
{
    internal Metadata(MetadataModel model)
    {
        MediaType = model.MediaType;
    }
    public string? MediaType { get; set; }
}
