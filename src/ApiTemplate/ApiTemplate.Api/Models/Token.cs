namespace ApiTemplate.Api.Models
{
    public class Token
    {
        public Token(string accessToken, int expiration, string type)
        {
            AccessToken = accessToken;
            Expiration = expiration;
            Type = type;
        }

        public string AccessToken { get; set; }
        public int Expiration { get; set; }
        public string Type { get; set; }
    }
}
