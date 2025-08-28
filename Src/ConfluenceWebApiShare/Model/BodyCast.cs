namespace ConfluenceWebApi.Model;

internal static class BodyCast
{
    public static Body CastTo(this BodyModel model)
    {
        return new Body()
        {
            Storage = model.Storage.CastModel<ValueRepresentation>(),
            Editor = model.Editor.CastModel<ValueRepresentation>(),
            View = model.View.CastModel<ValueRepresentation>(),
            ExportView = model.ExportView.CastModel<ValueRepresentation>(),
            AnonymousExportView = model.AnonymousExportView.CastModel<ValueRepresentation>(),
            StyledView = model.StyledView.CastModel<ValueRepresentation>()
        };
    }

    public static BodyModel CastFrom(this Body item)
    {
        return new BodyModel()
        {
            Storage = item.Storage?.ToModel(),
            Editor = item.Editor?.ToModel(),
            View = item.View?.ToModel(),
            ExportView = item.ExportView?.ToModel(),
            AnonymousExportView = item.AnonymousExportView?.ToModel(),
            StyledView = item.StyledView?.ToModel()
        };
    }
}
