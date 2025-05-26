namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceChildContentUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetChildrenOfContentAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var item = await confluence.GetChildrenOfContentAsync("478101610", null, Expand.Children + Expand.Attachment + Expand.Comment + Expand.Page);
        Assert.IsNotNull(item);
        //var list = await children.ToListAsync();
        //Assert.IsNotNull(list);
        //Assert.AreEqual(4, list.Count, "count");

        //var item0 = list[0];
        //Assert.IsNotNull(item0);
        //Assert.AreEqual("471531578", item0.Id, "item0.Id");
    }
}
