namespace ConfluenceWebApi.Html;

public class Table
{
    private readonly StringBuilder table = new();

    public Table()
    {
        table.Append("<table>");
        table.Append("<tbody>");
    }

    public Table(params string[] items)
    { 
        table.Append("<table>");

        table.Append("<thead>");
        table.Append("<tr>"); 
        foreach (var item in items)
        {
            table.Append($"<th>{item}</th>");
        }
        table.Append("</tr>");
        table.Append("</thead>");

        table.Append("<tbody>");
    }

    public Table AddHead(params string[] items)
    {
        table.Append("<tr>");
        foreach (var item in items)
        {
            table.Append($"<th>{item}</th>");
        }
        table.Append("</tr>");
        return this;
    }

    public Table AddRow(params string[] items)
    {
        table.Append("<tr>");
        foreach (var item in items)
        {
            table.Append($"<td>{item}</td>");
        }
        table.Append("</tr>");
        return this;
    }

    public Table AddHeadRow(string head, params string[] items)
    {
        table.Append("<tr>");
        table.Append($"<th>{head}</th>");
        foreach (var item in items)
        {
            table.Append($"<td>{item}</td>");
        }
        table.Append("</tr>");
        return this;
    }

    public override string ToString()
    {
        return table.ToString() + "</tbody></table>";
    }
}
