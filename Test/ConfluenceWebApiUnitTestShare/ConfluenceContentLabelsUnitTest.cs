namespace ConfluenceWebApiUnitTest;

[TestClass]
public class ConfluenceContentLabelsUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetLabelsAsync()
    {
        using var confluence = new Confluence(storeKey, appName);
        var labels = confluence.GetLabelsAsync("478097445");
        Assert.IsNotNull(labels);
        var list = await labels.ToListAsync();
        Assert.IsNotNull(list);
        Assert.AreEqual(4, list.Count, "count");

        var item0 = list[0];
        Assert.IsNotNull(item0);
        Assert.AreEqual("471531578", item0.Id, "item0.Id");
        Assert.AreEqual("global", item0.Prefix, "item0.Prefix");
        Assert.AreEqual("stichwort", item0.Name, "item0.Name");
        Assert.AreEqual("stichwort", item0.Text, "item0.Text");

        var item1 = list[1];
        Assert.IsNotNull(item1);
        Assert.AreEqual("45449243", item1.Id, "item1.Id");
        Assert.AreEqual("global", item1.Prefix, "item1.Prefix");
        Assert.AreEqual("demo", item1.Name, "item1.Name");
        Assert.AreEqual("demo", item1.Text, "item1.Text");

        var item2 = list[2];
        Assert.IsNotNull(item2);
        Assert.AreEqual("471531579", item2.Id, "item2.Id");
        Assert.AreEqual("global", item2.Prefix, "item2.Prefix");
        Assert.AreEqual("not_used", item2.Name, "item2.Name");
        Assert.AreEqual("not_used", item2.Text, "item2.Text");

        var item3 = list[3];
        Assert.IsNotNull(item3);
        Assert.AreEqual("11141237", item3.Id, "item3.Id");
        Assert.AreEqual("global", item3.Prefix, "item3.Prefix");
        Assert.AreEqual("external", item3.Name, "item3.Name");
        Assert.AreEqual("external", item3.Text, "item3.Text");
    }

}