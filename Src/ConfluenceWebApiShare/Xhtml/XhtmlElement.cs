using System.Collections.Generic;

namespace ConfluenceWebApi.Xhtml;

public class XhtmlElement(params XhtmlElement[] elements)
{
    public List<XhtmlElement> Children { get; } = [.. elements];

    protected string ChildrenText => string.Concat(Children);

    public override string? ToString() => ChildrenText;

    public static implicit operator string(XhtmlElement element) => element.ChildrenText;

    public static implicit operator XhtmlElement(string text) => new XhtmlText(text);


}
