using Newtonsoft.Json.Serialization;

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