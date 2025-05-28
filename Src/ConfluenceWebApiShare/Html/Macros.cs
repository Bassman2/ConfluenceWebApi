namespace ConfluenceWebApi.Html;

public static class Macros
{
    private static string CreateGuid => Guid.NewGuid().ToString("D");
    public static string CreateStatus(MacroColors color, string title)
    {
        return
        /**/    $"<ac:structured-macro ac:name=\"status\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" +
        /**/        $"<ac:parameter ac:name=\"colour\">{color}</ac:parameter>" +
        /**/        $"<ac:parameter ac:name=\"title\">{title}</ac:parameter>" +
        /**/    "</ac:structured-macro>";
    }

    public static string CreateChildren()
    {
        return $"<ac:structured-macro ac:name=\"children\" ac:schema-version=\"2\" ac:macro-id=\"{CreateGuid}\" />";
    }

    public static string CreateAttachemntView(string fileName, int height = 250)
    {
        return
        /**/    $"<ac:structured-macro ac:name=\"view-file\" ac:schema-version=\"1\" ac:macro-id=\"{CreateGuid}\">" + 
        /**/        $"<ac:parameter ac:name=\"name\"><ri:attachment ri:filename=\"{fileName}\" /></ac:parameter>" + 
        /**/        $"<ac:parameter ac:name=\"height\">{height}</ac:parameter>" +
        /**/    "</ac:structured-macro>";
    }
}
