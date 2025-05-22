using System.Security.AccessControl;

namespace ConfluenceWebApi;

public class Label
{
    public Label()
    { }

    internal Label(LabelModel model)
    {
        Prefix = model.Prefix;
        Name = model.Name;
        Id = model.Id;
        Text = model.Text;
    }
    
    public string Prefix { get; set; } = "";

    public string Name { get; set; } = "";

    public string Id { get; set; } = "";

    public string Text { get; set; } = "";
}
