namespace ConfluenceWebApi.Xhtml;

public class XhtmlList(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<ul{Attributes}>{ChildrenText}</ul>";
}

public class XhtmlListItem(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<li{Attributes}>{ChildrenText}</li>";
}
