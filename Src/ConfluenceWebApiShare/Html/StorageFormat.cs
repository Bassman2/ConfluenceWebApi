//namespace ConfluenceWebApi.Html;

////  https://confluence.atlassian.com/doc/confluence-storage-format-790796544.html

//public static class StorageFormat
//{
//    private static string CreateGuid => Guid.NewGuid().ToString("D");

//    public const string EmojiTick = "<ac:emoticon ac:name= \"tick\"/>";
//    public const string EmojiCross = "<ac:emoticon ac:name= \"cross\"/>";
//    public const string EmojiWarning = "<ac:emoticon ac:name= \"warning\"/>";
//    public const string EmojiQuestion = "<ac:emoticon ac:name= \"question\"/>";
//    public const string EmojiPlus = "<ac:emoticon ac:name= \"plus\"/>";
//    public const string EmojiMinus = "<ac:emoticon ac:name= \"minus\"/>";
//    public const string EmojiInformation = "<ac:emoticon ac:name= \"information\"/>";

//    public const string EmojiBlueStar = "<ac:emoticon ac:name= \"blue-star\"/>";
//    public const string EmojiRedStar = "<ac:emoticon ac:name= \"red-star\"/>";
//    public const string EmojiGreenStar = "<ac:emoticon ac:name= \"green-star\"/>";

//    public const string EmojiLightOn = "<ac:emoticon ac:name= \"light-on\"/>";
//    public const string EmojiLightOff = "<ac:emoticon ac:name= \"light-ff\"/>";

//    public const string EmojiRaisedHand = "<ac:emoticon ac:name= \"raised_hand\"/>";

//    public static string CreateStatus(MacroColors color, string title)
//    {
//        return
//        /**/    $"<ac:structured-macro ac:name=\"status\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" +
//        /**/        $"<ac:parameter ac:name=\"colour\">{color}</ac:parameter>" +
//        /**/        $"<ac:parameter ac:name=\"title\">{title}</ac:parameter>" +
//        /**/    "</ac:structured-macro>";
//    }

//    public static string CreateChildren()
//    {
//        return $"<ac:structured-macro ac:name=\"children\" ac:schema-version=\"2\" ac:macro-id=\"{CreateGuid}\" />";
//    }

//    public static string CreateAttachemntView(string fileName, int height = 250)
//    {
//        return
//        /**/    $"<ac:structured-macro ac:name=\"view-file\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" +
//        /**/        $"<ac:parameter ac:name=\"name\"><ri:attachment ri:filename=\"{fileName}\" /></ac:parameter>" +
//        /**/        $"<ac:parameter ac:name=\"height\">{height}</ac:parameter>" +
//        /**/    "</ac:structured-macro>";
//    }
//    public static string CreateLinkToPage(string title, string link)
//        => $"<ac:link><ri:page ri:content-title=\"{title}Page Title\" /><ac:plain-text-link-body><![CDATA[{link}]]></ac:plain-text-link-body></ac:link>";

//    public static string CreateLinkToAttachment(string title, string filename)
//        => $"<ac:link><ri:attachment ri:filename=\"{filename}\" /><ac:plain-text-link-body><![CDATA[{filename}]]></ac:plain-text-link-body></ac:link>";

//    public static string CreateLinkToExternal(string title, string link)
//        => $"<a href=\"{link}\">{title}Atlassian</a>";

//    public static string CreateLink(string fileName)
//    {
//        return $"<ac:link><ri:attachment ri:filename=\"{fileName}\" /></ac:link>";
//    }

//    public static string CreateImage(string fileName, int heigh = 250)
//    {
//        return $"<ac:image ac:height=\"{heigh}\"><ri:attachment ri:filename=\"{fileName}\" /></ac:image>";
//    }
//}
