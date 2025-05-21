namespace ConfluenceWebApi;

public class User
{
    internal User(UserModel model)
    {
        Type = model.Type;
        Username = model.Username;
        UserKey = model.UserKey;
        ProfilePicture = model.ProfilePicture.CastModel<ProfilePicture>();
        DisplayName = model.DisplayName;
    }

    public string? Type { get; set; }

    public string? Username { get; set; }

    public string? UserKey { get; set; }

    public ProfilePicture? ProfilePicture { get; set; }

    public string? DisplayName { get; set; }

}
