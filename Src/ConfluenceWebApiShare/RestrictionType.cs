namespace ConfluenceWebApi;

public class RestrictionType
{
    internal RestrictionType(RestrictionTypeModel model)
    {
        
        Results = model.Results.CastModel<RestrictionResult>();
    }

    public List<RestrictionResult>? Results { get; set; }

}
