namespace ConfluenceWebApi.Xhtml;

public class XhtmlTable(XhtmlTableColGroup? colGroup, params XhtmlTableRow[] elements) : XhtmlElement(elements)
{
    //private XhtmlTableColGroup? colGroup = null;

    public XhtmlTable(params XhtmlTableRow[] elements) : this(null, elements)
    { }

    //public XhtmlTable(XhtmlTableColGroup colGroup, params XhtmlTableRow[] elements) : base(elements)
    //{
    //    this.colGroup = colGroup;
    //}

    public override string ToString() => $"<table{Attributes}>{colGroup?.ToString() ?? ""}<tbody>{ChildrenText}</tbody></table>";
}

public class XhtmlTableRow(params XhtmlElement[] elements) : XhtmlElement([.. elements.Select(e => e is XhtmlTableCell ? e : new XhtmlTableCell(e))])
{
    public override string ToString() => $"<tr>{ChildrenText}</tr>";
}


public class XhtmlTableHeadingCell(params XhtmlElement[] elements) : XhtmlTableCell(elements)
{
    public override string ToString() => $"<th{Attributes}>{ChildrenText}</th>";
}

public class XhtmlTableCell(params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public int Colspan = 0;

    protected override string Attributes => base.Attributes +
        (Colspan == 0 ? "" : $" colspan=\"{Colspan}\"");

    public override string ToString() => $"<td{Attributes}>{ChildrenText}</td>";
}

public class XhtmlTableCol() : XhtmlElement
{
    public override string ToString() => $"<col{Attributes}/>";
}

public class XhtmlTableColGroup(params XhtmlTableCol[] elements) : XhtmlElement(elements)
{
    public override string ToString() => $"<colgroup>{ChildrenText}</colgroup>";
}