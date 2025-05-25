namespace ConfluenceWebApi.Service.Model;

internal class PageListModel<T>
{
    [JsonPropertyName("page")]
    public ResultListModel<T>? Page { get; set; }


    //[JsonPropertyName("results")]
    //public List<T>? Results { get; set; }

    //[JsonPropertyName("start")]
    //public int Start { get; set; }

    //[JsonPropertyName("limit")]
    //public int Limit { get; set; }

    //[JsonPropertyName("size")]
    //public int Size { get; set; }

    //[JsonPropertyName("cqlQuery")]
    //public string? CqlQuery { get; set; }

    //[JsonPropertyName("searchDuration")]
    //public int SearchDuration { get; set; }

    //[JsonPropertyName("totalSize")]
    //public int TotalSize { get; set; }

    //[JsonPropertyName("_links")]
    //public UserLinksModel? Links { get; set; }

}
