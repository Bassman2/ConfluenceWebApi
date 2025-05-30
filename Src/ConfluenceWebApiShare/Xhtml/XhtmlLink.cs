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