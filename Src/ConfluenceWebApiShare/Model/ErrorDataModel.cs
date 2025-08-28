namespace ConfluenceWebApi.Model;

internal class ErrorDataModel
{
    [JsonPropertyName("authorized")]
    public bool Authorized { get; set; }

    [JsonPropertyName("valid")]
    public bool Valid { get; set; }
    
    [JsonPropertyName("allowedInReadOnlyMode")]
    public bool AllowedInReadOnlyMode { get; set; }
    
    [JsonPropertyName("errors")]
    public List<ErrorItemModel>? Errors { get; set; }
    
    [JsonPropertyName("successful")]
    public bool Successful { get; set; }

    public override string ToString()
    {
        string res = Errors == null ? string.Empty : string.Join("\n    ", Errors.Select(e => e.ToString()));
        return $"\n  Authorized: {Authorized}\n  Valid: {Valid}\n  AllowedInReadOnlyMode: {AllowedInReadOnlyMode}\n  Errors: \n    {res}\n  Successful: {Successful}";
    }
}
