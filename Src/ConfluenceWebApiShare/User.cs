namespace ConfluenceWebApi;

/// <summary>
/// Represents a Confluence user with basic profile information.
/// </summary>
[Model]
public class User
{
    internal User() 
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class from a <see cref="UserModel"/>.
    /// </summary>
    /// <param name="model">The user model containing user data.</param>
    internal User(UserModel model)
    {
        Type = model.Type;
        Username = model.Username;
        UserKey = model.UserKey;
        ProfilePicture = model.ProfilePicture.CastModel<ProfilePicture>();
        DisplayName = model.DisplayName;
    }

    /// <summary>
    /// Gets or sets the type of the user (e.g., "known", "anonymous").
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the unique key identifying the user.
    /// </summary>
    public string? UserKey { get; set; }

    /// <summary>
    /// Gets or sets the profile picture of the user.
    /// </summary>
    public ProfilePicture? ProfilePicture { get; set; }

    /// <summary>
    /// Gets or sets the display name of the user.
    /// </summary>
    public string? DisplayName { get; set; }

}
