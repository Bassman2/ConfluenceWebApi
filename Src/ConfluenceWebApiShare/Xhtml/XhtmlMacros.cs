namespace ConfluenceWebApi.Xhtml;

public class XhtmlMacro(string name, params (string Name, XhtmlElement Value)[] values) : XhtmlElement()
{
    public override string ToString()
    { 
        string parameters = string.Concat(values.Select(v => $"<ac:parameter ac:name=\"{v.Name}\">{v.Value}</ac:parameter>"));
        return values.Length > 0 
            ? $"<ac:structured-macro ac:name=\"{name}\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">{parameters}</ac:structured-macro>" 
            : $"<ac:structured-macro ac:name=\"{name}\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\"/>";
    }
}

public class XhtmlChildrenMacro() : XhtmlMacro("children")
{ }

public class XhtmlTocMacro() : XhtmlMacro("toc")
{ }

public class XhtmlJiraMacro(string server, string key) : XhtmlMacro("jira",
    ("server", server),
    ("columnIds", "issuekey,summary,issuetype,created,updated,duedate,assignee,reporter,priority,status,resolution"),
    ("columns", "key,summary,type,created,updated,due,assignee,reporter,priority,status,resolution"),
    ("serverId", XhtmlElement.CreateGuid),
    ("key", key))
{ }

   //<ac:parameter ac:name="server">EB external Jira</ac:parameter>
   // <ac:parameter ac:name="columnIds">issuekey,summary,issuetype,created,updated,duedate,assignee,reporter,priority,status,resolution</ac:parameter>
   // <ac:parameter ac:name="columns">key,summary,type,created,updated,due,assignee,reporter,priority,status,resolution</ac:parameter>
   // <ac:parameter ac:name="serverId">c5054140-590f-30e3-a0ee-af1abaf2aea4</ac:parameter>
   // <ac:parameter ac:name="key">SHM-24991</ac:parameter>

public class XhtmlStatusMacro(XhtmlMacroColors color, string title) : XhtmlMacro("status", ("colour", color.ToString()), ("title", title))
{
    //public override string ToString() =>
    //    /**/    $"<ac:structured-macro ac:name=\"status\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" +
    //    /**/        $"<ac:parameter ac:name=\"colour\">{color}</ac:parameter>" +
    //    /**/        $"<ac:parameter ac:name=\"title\">{title}</ac:parameter>" +
    //    /**/    "</ac:structured-macro>";
}

public enum XhtmlMacroColors
{
    Green,
    Red,
    Yellow,
    Blue,
    Grey
}