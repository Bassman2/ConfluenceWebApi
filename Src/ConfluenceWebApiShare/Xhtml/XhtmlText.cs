namespace ConfluenceWebApi.Xhtml;

public class XhtmlText(string text) : XhtmlElement
{
    public override string? ToString() => text;
}
