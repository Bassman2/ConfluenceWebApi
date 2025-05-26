namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceSpaceUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetRootContentInSpaceRootAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var item = await confluence.GetRootContentInSpaceAsync(testSpace, Expand.All);

        Assert.IsNotNull(item);
        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual(testSpaceTitle, item.Title, "Title");

        Assert.IsNotNull(item.Space);
        Assert.AreEqual(122683496, item.Space.Id, "Space.Id");
        Assert.AreEqual(testSpace, item.Space.Key, "Space.Key");
        Assert.AreEqual(testUser, item.Space.Name, "Space.Name");
        Assert.AreEqual(Statuses.Current, item.Space.Status, "Space.Status");
        Assert.AreEqual(Types.Personal, item.Space.Type, "Space.TypeName");

        Assert.AreEqual(-1, item.Position, "Position");
    }

    [TestMethod]
    public async Task TestMethodGetContentsInSpaceRootAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsInSpaceAsync(testSpace, Depth.Root);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault();
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual(testSpaceTitle, item.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsInSpaceAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsInSpaceAsync(testSpace, Depth.All);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual(testSpaceTitle, item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual(Types.Page, item2.Type, "Type");
        Assert.AreEqual(Statuses.Current, item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsByTypePageAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsByTypeAsync(testSpace, Types.Page);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual(testSpaceTitle, item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual(Types.Page, item2.Type, "Type");
        Assert.AreEqual(Statuses.Current, item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetContentsByTypeBlogpostAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentsByTypeAsync(testSpace, Types.Blogpost);

        Assert.IsNotNull(contents);
        var list = (await contents.ToListAsync()).OrderBy(i => i.Title).ToList();
        Assert.IsNotNull(list);

        var item = list.SingleOrDefault(i => i.Id == "127199830");
        Assert.IsNotNull(item);

        Assert.AreEqual("127199830", item.Id, "Id");
        Assert.AreEqual(Types.Page, item.Type, "Type");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
        Assert.AreEqual(testSpaceTitle, item.Title, "Title");

        var item2 = list.SingleOrDefault(i => i.Id == "386551074");
        Assert.IsNotNull(item2);

        Assert.AreEqual("386551074", item2.Id, "Id");
        Assert.AreEqual(Types.Page, item2.Type, "Type");
        Assert.AreEqual(Statuses.Current, item2.Status, "Status");
        Assert.AreEqual("Tickets", item2.Title, "Title");


    }

    [TestMethod]
    public async Task TestMethodGetSpaceAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var item = await confluence.GetSpaceAsync(testSpace);

        Assert.IsNotNull(item);
        Assert.AreEqual(122683496, item.Id, "Id");
        Assert.AreEqual(testSpace, item.Key, "Key");
        Assert.AreEqual(testUser, item.Name, "Name");
        Assert.AreEqual(Statuses.Current, item.Status, "Status");
    }
}
