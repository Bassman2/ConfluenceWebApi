namespace ConfluenceWebApi.Xhtml;

public class XhtmlText(string text) : XhtmlElement
{
    public override string? ToString() => text;
}

public class XhtmlSpace() : XhtmlElement
{
    public override string? ToString() => "<p><br/></p>";
}

public class XhtmlNewline() : XhtmlElement
{
    public override string? ToString() => "<br/>";
}