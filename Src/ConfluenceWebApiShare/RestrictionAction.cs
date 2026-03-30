namespace ConfluenceWebApi;

public class RestrictionAction
{
    internal RestrictionAction(RestrictionActionModel model)
    {
        Operation = model.Operation;
        Restrictions = model.Restrictions?.CastModel<RestrictionsRestrictions>();
        LastModificationDate = model.LastModificationDate;
    }

    public string? Operation { get; set; }

    public RestrictionsRestrictions? Restrictions { get; }

    public string? LastModificationDate { get; }
}
