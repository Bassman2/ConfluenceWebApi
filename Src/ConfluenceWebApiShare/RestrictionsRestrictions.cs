namespace ConfluenceWebApi;

public class RestrictionsRestrictions
{
    internal RestrictionsRestrictions(RestrictionsRestrictionsModel model)
    {
        Groups = model.Groups?.Results?.CastModel<RestrictionResult>();
        Users = model.Users?.Results?.CastModel<RestrictionResult>();
    }

    public List<RestrictionResult>? Groups { get; }

    public List<RestrictionResult>? Users { get; }
}
