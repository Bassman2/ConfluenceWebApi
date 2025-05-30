﻿namespace ConfluenceWebApi.Service.Model;

[DebuggerDisplay("{Id}: {Prefix} - {Name} - {Text}")]
internal class LabelModel : BaseModel
{
    public static implicit operator LabelModel(Label model)
    {
        return new LabelModel() { Prefix = model.Prefix, Name = model.Name, Id = model.Id, Text = model.Text };
    }


    [JsonPropertyName("prefix")]
    public string Prefix { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("label")]
    public string Text { get; set; } = null!;
}
