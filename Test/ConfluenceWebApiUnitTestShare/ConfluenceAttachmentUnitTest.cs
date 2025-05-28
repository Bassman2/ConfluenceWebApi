namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceAttachmentUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetAttachmentAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var items = confluence.GetAttachmentAsync("478114423");
        Assert.IsNotNull(items);
        var list = await items.ToListAsync();
        Assert.IsNotNull(list);
    }

    [TestMethod]
    public async Task TestMethodCreateAttachmentAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var items = await confluence.CreateAttachmentAsync("478115004", "Demo.pdf", @"D:\_Data\Confluence\Demo.pdf");
        Assert.IsNotNull(items);
        var list = items.ToList();
        Assert.IsNotNull(list);
       
    }
}


