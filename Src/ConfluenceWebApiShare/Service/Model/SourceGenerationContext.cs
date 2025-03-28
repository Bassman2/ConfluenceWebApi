﻿namespace ConfluenceWebApi.Service.Model;

[JsonSourceGenerationOptions]
[JsonSerializable(typeof(ChildrenModel))]
[JsonSerializable(typeof(FolderModel))]
[JsonSerializable(typeof(List<FolderModel>))]
[JsonSerializable(typeof(List<SpaceModel>))]

[JsonSerializable(typeof(ListModel<SpaceModel>))]

[JsonSerializable(typeof(ContentModel))]
[JsonSerializable(typeof(ListModel<ContentModel>))]

[JsonSerializable(typeof(SpaceModel))]


[JsonSerializable(typeof(UserModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
