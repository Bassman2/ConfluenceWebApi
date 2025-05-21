namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceUserUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var item = await confluence.GetCurrentUserAsync();

        Assert.IsNotNull(item);
        Assert.AreEqual("known", item.Type, "Type");
        Assert.AreEqual("bs", item.Username, "Username");
        Assert.AreEqual("55df232345d0a0850145d0bccde005c1", item.UserKey, "UserKey");

        Assert.IsNotNull(item.ProfilePicture);
        Assert.AreEqual("/download/attachments/361920/user-avatar", item.ProfilePicture.Path, "ProfilePicture.Path");
        Assert.AreEqual(48, item.ProfilePicture.Width, "ProfilePicture.Width");
        Assert.AreEqual(48, item.ProfilePicture.Height, "ProfilePicture.Height");
        Assert.IsFalse(item.ProfilePicture.IsDefault, "ProfilePicture.IsDefault");

        Assert.AreEqual("Beckers, Ralf", item.DisplayName, "DisplayName");
    }
}
