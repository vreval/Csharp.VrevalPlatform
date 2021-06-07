using Newtonsoft.Json.Serialization;
using Vreval.Library;

namespace Vreval.Platform
{
    class VrevalDataObjectResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return base.ResolvePropertyName(propertyName.ToSnakeCase());
        }
    }
}