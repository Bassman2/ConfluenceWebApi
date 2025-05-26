namespace ConfluenceWebApi;

public class Description
{
    internal Description(DescriptionModel model)
    {
        Plain = model.Plain.CastModel<ValueRepresentation>();
        View = model.View.CastModel<ValueRepresentation>();
    }

    public ValueRepresentation? Plain { get; set; }

    public ValueRepresentation? View { get; set; }
}
