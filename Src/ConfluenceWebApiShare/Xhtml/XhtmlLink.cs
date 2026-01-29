namespace ConfluenceWebApi.Xhtml;

public class XhtmlLinkToExternal(string link, params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString()
        => $"<a href=\"{link.Replace("&", "&amp;")}\">{ChildrenText}</a>";
}

public class XhtmlLinkToPage(string page, string text = "") : XhtmlElement
{
    public override string ToString()
        => string.IsNullOrEmpty(text)
            ? $"<ac:link><ri:page ri:content-title=\"{page}\"/></ac:link>"
            : $"<ac:link><ri:page ri:content-title=\"{page}\"/><ac:plain-text-link-body><![CDATA[{text}]]></ac:plain-text-link-body></ac:link>";
}

public class XhtmlLinkToAttachment(string attachment, string text) : XhtmlElement
{
    public override string ToString()
        => $"<ac:link><ri:attachment ri:filename=\"{attachment}\"/><ac:plain-text-link-body> <![CDATA[{text}]]></ac:plain-text-link-body></ac:link>";
}