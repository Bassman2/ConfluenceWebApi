﻿namespace ConfluenceWebApi;

public class ValueRepresentation
{
    public ValueRepresentation()
    { }

    internal ValueRepresentation(ValueRepresentationModel model)
    {
        Content = model.Content.CastModel<Content>();
        Value = model.Value;
        Representation = model.Representation;
        Webresource = model.Webresource;
    }

    internal ValueRepresentationModel ToModel()
    {
        return new ValueRepresentationModel()
        {
            Value = Value,
            Representation = Representation
        };
    }
    public Content? Content { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("representation")]
    public Representations Representation { get; set; }

    [JsonPropertyName("webresource")]
    public string? Webresource { get; set; }

}