namespace ConfluenceWebApi.Model;

internal class BodyModel : BaseModel
{
    [JsonPropertyName("storage")]
    public ValueRepresentationModel? Storage { get; set; }

    [JsonPropertyName("editor")]
    public ValueRepresentationModel? Editor { get; set; }

    [JsonPropertyName("view")]
    public ValueRepresentationModel? View { get; set; }

    [JsonPropertyName("export_view")]
    public ValueRepresentationModel? ExportView { get; set; }

    [JsonPropertyName("anonymous_export_view")]
    public ValueRepresentationModel? AnonymousExportView { get; set; }

    [JsonPropertyName("styled_view")]
    public ValueRepresentationModel? StyledView { get; set; }
}
