using static System.Net.Mime.MediaTypeNames;

namespace ConfluenceWebApiUnitTest;

[TestClass]
public class BaselineUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodCreateBaselineAsync()
    {
        string pi = "PI20_25.22";

        string page = CreateStateTable(StatusReleased) + Space + CreateStateTable(StatusDraft) + Space + Macros.CreateChildren();

        using var confluence = new Confluence(storeKey, appName);
        var baseline = await confluence.CreatePageAsync(testSpaceKey, "Baseline EBMDUs_" + pi, page, "478111079");
               

        var releaseTypeTable = new Table().AddHeadRow("Release Type", "Development").ToString();


        var hardwareTable = new Table("CI Item", "Version", "Status")
            .AddRow("Hardware 1", "2025.02", StatusReleased)
            .AddRow("Hardware 2", "2025.02", StatusReleased)
            .AddRow("Hardware 3", "2025.02", StatusReleased)
            .AddRow("Hardware 4", Emoji.Question, StatusNotAvailable)
            .ToString();

        //var hardwareTable2 = new Table("CI Item", "Version", "Current Status" );
        //hardwareTable2.AddRow("Milestones", "2025.05", Macros.CreateStatus(MacroColors.Green, "Released"));
        //hardwareTable2.AddRow("HW Test Specification", "(?)", Macros.CreateStatus(MacroColors.Red, "Not Available"));
        string hardwarePage = releaseTypeTable + Space + hardwareTable + Space + "test1 "+ Emoji.Tick + Space + "test2 " + Emoji.Cross + Space + "test3 " + Emoji.Plus;


        var hardware = await confluence.CreatePageAsync(testSpaceKey, "CI Hardware - Baseline EBMDUs_" + pi, hardwarePage, baseline?.Id);
        var management = await confluence.CreatePageAsync(testSpaceKey, "CI Management - Baseline EBMDUs_" + pi, "Child", baseline?.Id);
        var openSource = await confluence.CreatePageAsync(testSpaceKey, "CI OSS - Baseline EBMDUs_" + pi, "Child", baseline?.Id);
        var components = await confluence.CreatePageAsync(testSpaceKey, "CI SW Components - Baseline EBMDUs_" + pi, "Child", baseline?.Id);
        var compositions = await confluence.CreatePageAsync(testSpaceKey, "CI SW Compositions - Baseline EBMDUs_" + pi, "Child", baseline?.Id);
        var tools = await confluence.CreatePageAsync(testSpaceKey, "CI Tools - Baseline EBMDUs_" + pi, "Child", baseline?.Id);

    }

    [TestMethod]
    public async Task TestMethodCheckBaselineAsync()
    {

        using var confluence = new Confluence(storeKey, appName);
        var baseline = await confluence.GetContentByIdAsync("478111079", expand: Expand.All);

    }

    [TestMethod]
    public async Task TestMethodCheckEmojiAsync()
    {

        using var confluence = new Confluence(storeKey, appName);
        var baseline = await confluence.GetContentByIdAsync("478114171", expand: Expand.All);

    }

    #region internal html

    private static readonly string Space = "<p><br /></p>";

    //private static string CreateStateTable(MacroColors color, string title)
    //{
    //    var status = Macros.CreateStatus(color, title);
    //    var table = new Table().AddHeadRow("State", status).ToString();
    //    return table;
    //}

    private static string CreateStateTable(string macro)
    {
        var table = new Table().AddHeadRow("State", macro).ToString();
        return table;
    }

    private static readonly string StatusNotAvailable = Macros.CreateStatus(MacroColors.Red, "Not Available");
    private static readonly string StatusDraft = Macros.CreateStatus(MacroColors.Grey, "Draft");
    private static readonly string StatusProposed = Macros.CreateStatus(MacroColors.Yellow, "Proposed");
    private static readonly string StatusPreviewed = Macros.CreateStatus(MacroColors.Blue, "Previewed OK");
    private static readonly string StatusReleased = Macros.CreateStatus(MacroColors.Green, "Released");


    #endregion
}
