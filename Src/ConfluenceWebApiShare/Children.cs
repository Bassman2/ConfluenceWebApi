namespace ConfluenceWebApi;

public class Children
{
    internal Children(ChildrenModel model)
    {
        Attachments = model.Attachment?.Results.CastModel<Content>();
        Comments = model.Comment?.Results.CastModel<Comment>();
        Pages = model.Page?.Results.CastModel<Content>();
    }

    public List<Content>? Attachments { get; set; }

    public List<Comment>? Comments { get; set; }

    public List<Content>? Pages { get; set; }

}
