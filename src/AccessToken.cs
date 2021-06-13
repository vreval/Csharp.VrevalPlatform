namespace Vreval.Platform
{
    public class AccessToken
    {
        public AccessToken(string rawToken)
        {
            var parts = rawToken.Split('.');
            ResourceId = parts[0];
            BearerToken = parts[1];
        }

        public string BearerToken { get; }

        public string ResourceId { get; }
    }
}