namespace ConfluenceWebApi;

public class Expand
{
    public static readonly Expand Ancestors                     /**/ = new("ancestors");

    public static readonly Expand Body                          /**/ = new("body");
    public static readonly Expand Body_Editor                   /**/ = new("body.editors");
    public static readonly Expand Body_View                     /**/ = new("body.view");
    public static readonly Expand Body_ExportView               /**/ = new("body.export_view");
    public static readonly Expand Body_StyledView               /**/ = new("body.styled_view");
    public static readonly Expand Body_Storage                  /**/ = new("body.storage");
    public static readonly Expand Body_Storage_Content          /**/ = new("body.storage.content");
    public static readonly Expand Body_AnonymousExportView      /**/ = new("body.anonymousExportView");
    public static readonly Expand BodyAll                       /**/ = Body + Body_Editor + Body_View + Body_ExportView + Body_StyledView + Body_Storage + Body_Storage_Content + Body_AnonymousExportView;

    public static readonly Expand Children                      /**/ = new("children");
    public static readonly Expand Children_Attachment           /**/ = new("children.attachment");
    public static readonly Expand Children_Comment              /**/ = new("children.comment");
    public static readonly Expand Children_Page                 /**/ = new("children.page");
    public static readonly Expand ChildrenAll                   /**/ = Children + Children_Attachment + Children_Comment + Children_Page;

    public static readonly Expand Container                     /**/ = new("container");

    public static readonly Expand Descendants                   /**/ = new("descendants");
    public static readonly Expand Descendants_Attachment        /**/ = new("descendants.attachment");
    public static readonly Expand Descendants_Comment           /**/ = new("descendants.comment");
    public static readonly Expand DescendantsAll                /**/ = Descendants + Descendants_Attachment + Descendants_Comment;

    public static readonly Expand History                       /**/ = new("history");
    public static readonly Expand History_LastUpdated           /**/ = new("history.lastUpdated");
    public static readonly Expand History_PreviousVersion       /**/ = new("history.previousVersion");
    public static readonly Expand History_Contributors          /**/ = new("history.contributors");
    public static readonly Expand History_NextVersion           /**/ = new("history.nextVersion");
    public static readonly Expand HistoryAll                    /**/ = History + History_LastUpdated + History_PreviousVersion + History_Contributors + History_NextVersion;

    public static readonly Expand Metadata                      /**/ = new("metadata");
    public static readonly Expand Metadata_Currentuser          /**/ = new("metadata.currentuser");
    public static readonly Expand Metadata_Properties           /**/ = new("metadata.properties");
    public static readonly Expand Metadata_Frontend             /**/ = new("metadata.frontend");
    public static readonly Expand Metadata_EditorHtml           /**/ = new("metadata.editorHtml");
    public static readonly Expand Metadata_Labels               /**/ = new("metadata.labels");
    public static readonly Expand MetadataAll                   /**/ = Metadata + Metadata_Currentuser + Metadata_Properties + Metadata_Frontend + Metadata_EditorHtml + Metadata_Labels;

    public static readonly Expand Operations                    /**/ = new("operations");

    public static readonly Expand Restrictions                  /**/ = new("restrictions");
    public static readonly Expand Restrictions_Read             /**/ = new("restrictions.read");
    public static readonly Expand Restrictions_Update           /**/ = new("restrictions.update");
    public static readonly Expand RestrictionsAll               /**/ = Restrictions + Restrictions_Read + Restrictions_Update;

    public static readonly Expand Space                         /**/ = new("space");
    public static readonly Expand Space_Description             /**/ = new("space.description");
    public static readonly Expand Space_Homepage                /**/ = new("space.homepage");
    public static readonly Expand Space_Icon                    /**/ = new("space.icon");
    public static readonly Expand Space_Metadata                /**/ = new("space.metadata");
    public static readonly Expand Space_RetentionPolicy         /**/ = new("space.retentionPolicy");
    public static readonly Expand SpaceAll                      /**/ = Space + Space_Description + Space_Homepage + Space_Icon + Space_Metadata + Space_RetentionPolicy;

    public static readonly Expand Storage                       /**/ = new("storage");

    public static readonly Expand Version                       /**/ = new("version");
    public static readonly Expand Version_Content               /**/ = new("version.content");
    public static readonly Expand VersionAll                    /**/ = Version + Version_Content;

    public static readonly Expand All                           /**/ = Ancestors + BodyAll + ChildrenAll + Container + DescendantsAll + HistoryAll + MetadataAll + Operations + RestrictionsAll + SpaceAll + Storage + VersionAll;


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
