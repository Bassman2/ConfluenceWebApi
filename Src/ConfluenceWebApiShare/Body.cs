namespace ConfluenceWebApi;

public class Body
{
    public Body()
    { }

    internal Body(BodyModel model)
    {
        Storage = model.Storage.CastModel<ValueRepresentation>();
        Editor = model.Editor.CastModel<ValueRepresentation>();
        View = model.View.CastModel<ValueRepresentation>();
        ExportView = model.ExportView.CastModel<ValueRepresentation>();
        AnonymousExportView = model.AnonymousExportView.CastModel<ValueRepresentation>();
        StyledView = model.StyledView.CastModel<ValueRepresentation>();
    }

    internal BodyModel ToModel()
    {
        return new BodyModel()
        {
            Storage = Storage?.ToModel(),
            Editor = Editor?.ToModel(),
            View = View?.ToModel(),
            ExportView = ExportView?.ToModel(),
            AnonymousExportView = AnonymousExportView?.ToModel(),
            StyledView = StyledView?.ToModel()
        };
    }

    public ValueRepresentation? Storage { get; internal init; }

    public ValueRepresentation? Editor { get; internal init; }

    public ValueRepresentation? View { get; internal init; }

    public ValueRepresentation? ExportView { get; internal init; }

    public ValueRepresentation? AnonymousExportView { get; internal init; }

    public ValueRepresentation? StyledView { get; internal init; }
}
