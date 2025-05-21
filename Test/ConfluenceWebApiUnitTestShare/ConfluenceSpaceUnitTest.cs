namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceSpaceUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetContentsInSpaceRootAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsInSpaceAsync("~bs", Depth.Root);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault();
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual("page", item.Type, "Type");
        Assert.AreEqual("current", item.Status, "Status");
        Assert.AreEqual("Beckers, Ralfs Startseite", item.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsInSpaceAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsInSpaceAsync("~bs", Depth.All);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual("page", item.Type, "Type");
        Assert.AreEqual("current", item.Status, "Status");
        Assert.AreEqual("Beckers, Ralfs Startseite", item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual("page", item2.Type, "Type");
        Assert.AreEqual("current", item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsByTypePageAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsByTypeAsync("~bs", Types.Page);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual("page", item.Type, "Type");
        Assert.AreEqual("current", item.Status, "Status");
        Assert.AreEqual("Beckers, Ralfs Startseite", item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual("page", item2.Type, "Type");
        Assert.AreEqual("current", item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsByTypeBlogpostAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsByTypeAsync("~bs", Types.Blogpost);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual("page", item.Type, "Type");
        Assert.AreEqual("current", item.Status, "Status");
        Assert.AreEqual("Beckers, Ralfs Startseite", item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual("page", item2.Type, "Type");
        Assert.AreEqual("current", item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }
}
