using System;
using System.Collections.Generic;
using System.Text;

namespace ConfluenceWebApi.Model;

internal class RestrictionsRestrictionsModel : BaseModel
{
    [JsonPropertyName("group")]
    public RestrictionTypeModel? Group { get; set; }

    [JsonPropertyName("user")]
    public RestrictionTypeModel? User { get; set; }
}
