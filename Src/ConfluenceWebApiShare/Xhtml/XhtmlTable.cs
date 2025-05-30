namespace ConfluenceWebApi.Xhtml;

public class XhtmlTable(params XhtmlTableRow[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<table><tbody>{ChildrenText}</tbody></table>";
}

public class XhtmlTableRow(params XhtmlTableCell[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<tr>{ChildrenText}</tr>";
}

public class XhtmlTableHeadingCell(params XhtmlElement[] elements) : XhtmlTableCell(elements)
{
    public override string ToString() => $"<th>{ChildrenText}</th>";
}
public class XhtmlTableCell(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<td>{ChildrenText}</td>";
}