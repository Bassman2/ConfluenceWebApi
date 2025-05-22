namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceInstanceMetricsUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetInstanceMetricsAsync()
    {
        using var confluence = new Confluence(storeKey, appName);

        await Assert.ThrowsAsync<WebServiceException>(async () => await confluence.GetInstanceMetricsAsync("478097426"));
                
        //var item = await confluence.GetInstanceMetricsAsync("478097426");
                
        //Assert.IsNotNull(item);

        //Assert.AreEqual(1, item.Pages, "Pages");
        //Assert.AreEqual(1, item.Spaces, "Spaces");
        //Assert.AreEqual(1, item.Users, "Users");
    }
}