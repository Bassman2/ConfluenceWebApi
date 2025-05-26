namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceContentResourceUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetContentAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentAsync(testSpace);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i!.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.FirstOrDefault();
        Assert.IsNotNull(item);

        Assert.AreEqual("209866578", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual("Abkürzungen", item.Title, "Title");


    }
}
