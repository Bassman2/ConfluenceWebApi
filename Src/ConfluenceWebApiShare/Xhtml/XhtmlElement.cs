namespace ConfluenceWebApi.Xhtml;

//  https://confluence.atlassian.com/doc/confluence-storage-format-790796544.html


// https://developer.atlassian.com/server/confluence/rest/v923/api-group-space/#api-group-space


public class XhtmlElement(params XhtmlElement[] elements)
{
    public string Class { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string DataHighlightColour { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    protected string Attributes =>
        (string.IsNullOrEmpty(Class) ? "" : $" class=\"{Class}\"") +
        (string.IsNullOrEmpty(Style) ? "" : $" style=\"{Style}\"") +
        (string.IsNullOrEmpty(Scope) ? "" : $" scope=\"{Scope}\"") +
        (string.IsNullOrEmpty(DataHighlightColour) ? "" : $" data-highlight-colour=\"{DataHighlightColour}\"") +
        (string.IsNullOrEmpty(Title) ? "" : $" title=\"{Title}\"");

    protected static string CreateGuid => Guid.NewGuid().ToString("D");
    protected string ChildrenText => string.Concat(Children);

    public List<XhtmlElement> Children { get; } = [.. elements];

    public override string? ToString() => ChildrenText;

    public static implicit operator string(XhtmlElement element) => element.ChildrenText;

    public static implicit operator XhtmlElement(string text) => new XhtmlText(text);


}
