namespace ConfluenceWebApi;

public class Restrictions
{
    internal Restrictions(RestrictionsModel model)
    {
        Read = model.Read?.CastModel<RestrictionAction>();
        Update = model.Update?.CastModel<RestrictionAction>();
    }

    public RestrictionAction? Read { get; }

    public RestrictionAction? Update { get; }
}
