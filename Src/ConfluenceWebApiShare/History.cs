namespace ConfluenceWebApi;

public class History
{
    internal History(HistoryModel model)
    {
        LastUpdated = model.LastUpdated.CastModel<Version>();
        Latest = model.Latest;
        CreatedBy = model.CreatedBy.CastModel<User>();
        CreatedDate = model.CreatedDate;
        Contributors = model.Contributors.CastModel<Contributors>();
    }

    public Version? LastUpdated { get; set; }

    public bool Latest { get; set; }

    public User? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Contributors? Contributors { get; set; }
}
