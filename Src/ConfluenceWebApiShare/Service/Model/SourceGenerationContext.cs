namespace ConfluenceWebApi.Service.Model;

[JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web, 
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]

[JsonSerializable(typeof(AccessModeStatusModel))]

[JsonSerializable(typeof(ChildrenModel))]
[JsonSerializable(typeof(FolderModel))]
[JsonSerializable(typeof(List<FolderModel>))]
[JsonSerializable(typeof(List<SpaceModel>))]

[JsonSerializable(typeof(PageListModel<SpaceModel>))]

[JsonSerializable(typeof(ContentModel))]
[JsonSerializable(typeof(PageListModel<ContentModel>))]

[JsonSerializable(typeof(SpaceModel))]

[JsonSerializable(typeof(UserModel))]



// start
[JsonSerializable(typeof(ResultListModel<LabelModel>))]


[JsonSerializable(typeof(ErrorModel))]

[JsonSerializable(typeof(ManifestModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
