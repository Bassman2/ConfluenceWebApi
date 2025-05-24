namespace ConfluenceWebApi;

public class Expand
{
    public static readonly Expand Ancestors                   /**/ = new("ancestors");

    public static readonly Expand Body                        /**/ = new("body");
    public static readonly Expand Body_Editor                 /**/ = new("body.editors");
    public static readonly Expand Body_View                   /**/ = new("body.view");
    public static readonly Expand Body_ExportView             /**/ = new("body.export_view");
    public static readonly Expand Body_StyledView             /**/ = new("body.styled_view");
    public static readonly Expand Body_Storage                /**/ = new("body.storage");
    public static readonly Expand Body_Storage_Content        /**/ = new("body.storage.content");
    public static readonly Expand Body_AnonymousExportView    /**/ = new("body.anonymousExportView");
        
    private readonly List<string> content = [];

    public Expand()
    { }

    private Expand(string name)
    {
        content.Add(name);
    }

    private Expand(Expand a, Expand b)
    {
        content.AddRange(a.content);
        content.AddRange(b.content);
    }

    public override string? ToString()
    {
        return content.Count > 0 ? string.Join(',', content) : null;
        //return content.Count > 0 ? content.Aggregate("", (a, b) => $"{a},{b}").Trim(',') : null;
    }


    public static Expand operator +(Expand a, Expand b) => new(a, b);
}
