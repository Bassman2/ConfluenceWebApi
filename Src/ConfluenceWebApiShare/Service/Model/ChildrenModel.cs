namespace ConfluenceWebApi.Service.Model;

internal class ChildrenModel : BaseModel
{
    [JsonPropertyName("attachment")]
    public ResultListModel<AttachmentModel>? Attachment { get; set; }

    [JsonPropertyName("comment")]
    public ResultListModel<CommentModel>? Comment { get; set; }

    [JsonPropertyName("page")]
    public ResultListModel<ContentModel>? Page { get; set; }
}
