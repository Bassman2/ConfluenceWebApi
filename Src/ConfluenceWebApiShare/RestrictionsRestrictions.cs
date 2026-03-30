namespace ConfluenceWebApi;

public class RestrictionsRestrictions
{
    internal RestrictionsRestrictions(RestrictionsRestrictionsModel model)
    {
        Group = model.Group?.CastModel<RestrictionType>();
        User = model.User?.CastModel<RestrictionType>();
    }

    public RestrictionType? Group { get; }

    public RestrictionType? User { get; }
}
