namespace ConfluenceWebApiUnitTest;

[TestClass]
public class BaselineUnitTest : ConfluenceBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodCreateBaselineAsync()
    {
        string pi = "PI20_25.22";

        string page = CreateStateTable(MacroColors.Green, "Released") + Space + CreateStateTable(MacroColors.Red, "Draft") + Space + Macros.CreateChildren();

        using var confluence = new Confluence(storeKey, appName);
        var baseline = await confluence.CreatePageAsync(testSpaceKey, "Baseline EBMDUs_" + pi, page, "478111079");

        var hardware = await confluence.CreatePageAsync(testSpaceKey, "CI Hardware - Baseline EBMDUs_" + pi, "Child", baseline?.Id);
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


    #region internal html

    private static readonly string Space = "<p><br /></p>";

    private static string CreateStateTable(MacroColors color, string title)
    {
        return
             /**/   "<table>" +
         //    /**/       "<colgroup><col /><col /></colgroup>" +
             /**/       "<tbody>" + 
             /**/           "<tr>" + 
             /**/               "<th scope=\"row\">State</th>" + 
             /**/               "<td>" + 
         //    /**/                   "<div class=\"content-wrapper\">" + 
         //    /**/                       "<p>" +
             /**/                           Macros.CreateStatus(color, title) +
         //    /**/                       "</p>" + 
         //    /**/                   "</div>" + 
             /**/               "</td>" + 
             /**/           "</tr>" + "" +
             /**/       "</tbody>" + 
             /**/   "</table>";

    }


    #endregion
}
