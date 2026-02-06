namespace ConfluenceWebApi.Xhtml;

public class XhtmlText(string text) : XhtmlElement
{
    public override string? ToString() => text;
}

public class XhtmlSpace() : XhtmlElement
{
    public override string? ToString() => "<p><br/></p>";
}

public class XhtmlNoneBreakableSpace() : XhtmlElement
{
    public override string? ToString() => "&nbsp;";
}

public class XhtmlNewline() : XhtmlElement
{
    public override string? ToString() => "<br/>";
}

public class XhtmlParagraph(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<p{Attributes}>{ChildrenText}</p>";
}

public class XhtmlDiv(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<div{Attributes}>{ChildrenText}</div>";
}

public class XhtmlU(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<u>{ChildrenText}</u>";
}

public class XhtmlDate(DateTime dateTime) : XhtmlElement
{
    public override string ToString() => $"<time datetime = \"{dateTime:yyyy-MM-dd}\" />";
}
