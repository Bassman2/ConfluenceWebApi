namespace ConfluenceWebApi.Html;

//  https://confluence.atlassian.com/doc/confluence-storage-format-790796544.html

public static class Class1
{
    public static string CreateLinkToPage(string title, string link)
        => $"<ac:link><ri:page ri:content-title=\"{title}Page Title\" /><ac:plain-text-link-body><![CDATA[{link}]]></ac:plain-text-link-body></ac:link>";

    public static string CreateLinkToAttachment(string title, string filename)
        => $"<ac:link><ri:attachment ri:filename=\"{filename}\" /><ac:plain-text-link-body><![CDATA[{link}]]></ac:plain-text-link-body></ac:link>";

    public static string CreateLinkToExternal(string title, string link)
        => $"<a href=\"{link}\">{titel}Atlassian</a>";
}
