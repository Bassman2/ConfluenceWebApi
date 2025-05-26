using WebServiceClient;

namespace ConfluenceWebApi;

public class Body
{
    internal Body(BodyModel model)
    {
        Storage = model.Storage.CastModel<ValueRepresentation>();
        Editor = model.Editor.CastModel<ValueRepresentation>();
        View = model.View.CastModel<ValueRepresentation>();
        ExportView = model.ExportView.CastModel<ValueRepresentation>();
        AnonymousExportView = model.AnonymousExportView.CastModel<ValueRepresentation>();
        StyledView = model.StyledView.CastModel<ValueRepresentation>();
    }

    public ValueRepresentation? Storage { get; set; }

    public ValueRepresentation? Editor { get; set; }

    public ValueRepresentation? View { get; set; }

    public ValueRepresentation? ExportView { get; set; }

    public ValueRepresentation? AnonymousExportView { get; set; }

    public ValueRepresentation? StyledView { get; set; }
}
