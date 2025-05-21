namespace ConfluenceWebApi;

public class Space
{
    internal Space(SpaceModel model)
    {
        Id = model.Id;
        Key = model.Key;
        Name = model.Name;
        Status = model.Status;
    }

    public string Id { get; set; }

    public string? Key { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }
}
