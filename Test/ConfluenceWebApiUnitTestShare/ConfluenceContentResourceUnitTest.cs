namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceContentResourceUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetContentAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.GetContentAsync(testSpaceKey);

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

    [TestMethod]
    public async Task TestMethodCreateContentAsync()
    {
        var create = new Content() 
        { 
            Type = Types.Page,
            Title = "Create Test Page",
            Space = new Space() { Key = testSpaceKey },
            Ancestors = [new() { Id = "3456" } ],
            Body = new Body() { Storage = new ValueRepresentation() { Value = "<p>This is my storage</p>", Representation = "storage" } }
        };

        using var confluence = new Confluence(storeKey, appName);
        var contents = confluence.CreateContentAsync(create);

    }
}
