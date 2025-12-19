using ConfluenceWebApi.Xhtml;

public class XhtmlStrong(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<strong>{ChildrenText}</strong>";
}

public class XhtmlMark(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<mark>{ChildrenText}</mark>";
}