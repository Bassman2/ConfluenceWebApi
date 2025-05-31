namespace ConfluenceWebApi.Xhtml;

//public class XhtmlLinkToPage(string pageTitle, string text) : XhtmlElement
//{
//    public override string ToString() 
//        => $"<ac:link><ri:page ri:content-title=\"{pageTitle}\" /><ac:plain-text-link-body><![CDATA[{link}]]></ac:plain-text-link-body></ac:link>";
//}

public class XhtmlLinkToExternal(string link, params XhtmlElement[] elements) : XhtmlElement(elements)
{
    public override string ToString()
        => $"<a href=\"{link}\">{ChildrenText}</a>";
}

public class XhtmlLinkToPage(string page, string text) : XhtmlElement
{
    public override string ToString()
        => $"<ac:link><ri:page ri:content-title=\"{page}\" /><ac:plain-text-link-body><![CDATA[{text}]]></ac:plain-text-link-body></ac:link>";
}

public class XhtmlLinkToAttachment(string attachment, string text) : XhtmlElement
{
    public override string ToString()
        => $"<ac:link><ri:attachment ri:filename=\"{attachment}\"/><ac:plain-text-link-body> <![CDATA[{text}]]></ac:plain-text-link-body></ac:link>";
}