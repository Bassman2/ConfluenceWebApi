namespace ConfluenceWebApi;

public class Version
{
    internal Version(VersionModel model)
    {
        By = model.By.CastModel<User>();
        When = model.When;
        Message = model.Message;
        Number = model.Number;
        MinorEdit = model.MinorEdit;
        Hidden = model.Hidden;
        Content = model.Content.CastModel<Content>();
    }

    public User? By { get; set; }

    public DateTime? When { get; set; }

    public string? Message { get; set; }

    public int Number { get; set; }

    public bool MinorEdit { get; set; }

    public bool Hidden { get; set; }

    public Content? Content { get; set; }
}
