namespace ConfluenceWebApi.Html;

public static class Attachment
{
    public static string CreateLink(string fileName)
    {
        return $"<ac:link><ri:attachment ri:filename=\"{fileName}\" /></ac:link>";
    }

    public static string CreateImage(string fileName, int heigh = 250)
    {
        return $"<ac:image ac:height=\"{heigh}\"><ri:attachment ri:filename=\"{fileName}\" /></ac:image>";
    }
}
