namespace ConfluenceWebApi;

public class RestrictionResult
{
    internal RestrictionResult(RestrictionResultModel model)
    {
        Type = model.Type;
        Name = model.Name;
    }

    public string? Type { get; set; }

    public string? Name { get; set; }
}
