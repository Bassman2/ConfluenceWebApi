namespace ConfluenceWebApiUnitTest;



[TestClass]
public class ConfluenceAccessModeUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetAccessModeStatusAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var status = await confluence.GetAccessModeStatusAsync();
        Assert.AreEqual("READ_WRITE", status, "status");
    }
}
