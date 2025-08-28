namespace ConfluenceWebApi.Model;

internal class PageListModel<T> : BaseModel
{
    [JsonPropertyName("page")]
    public ResultListModel<T>? Page { get; set; }
}
