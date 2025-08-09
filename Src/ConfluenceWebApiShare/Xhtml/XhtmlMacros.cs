namespace ConfluenceWebApi.Xhtml;

public class XhtmlChildrenMacro() : XhtmlElement()
{
    public override string ToString() => $"<ac:structured-macro ac:name=\"children\" ac:schema-version=\"2\" ac:macro-id=\"{CreateGuid}\"/>";
}

public class XhtmlStatusMacro(XhtmlMacroColors color, string title) : XhtmlElement()
{
    public override string ToString() =>
        /**/    $"<ac:structured-macro ac:name=\"status\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" +
        /**/        $"<ac:parameter ac:name=\"colour\">{color}</ac:parameter>" +
        /**/        $"<ac:parameter ac:name=\"title\">{title}</ac:parameter>" +
        /**/    "</ac:structured-macro>"; 
}

public enum XhtmlMacroColors
{
    Green,
    Red,
    Yellow,
    Blue,
    Grey
}