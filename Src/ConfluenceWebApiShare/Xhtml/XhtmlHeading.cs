namespace ConfluenceWebApi.Xhtml;

public class XhtmlHeading1(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h1>{ChildrenText}</h1>";
}

public class XhtmlHeading2(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h2>{ChildrenText}</h2>";
}

public class XhtmlHeading3(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h3>{ChildrenText}</h3>";
}

public class XhtmlHeading4(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h4>{ChildrenText}</h4>";
}

public class XhtmlHeading5(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h5>{ChildrenText}</h5>";
}

public class XhtmlHeading6(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<h6>{ChildrenText}</h6>";
}

