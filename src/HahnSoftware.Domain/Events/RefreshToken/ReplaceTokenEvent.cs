namespace HahnSoftware.Domain.Events.RefreshToken;

public class ReplaceTokenEvent : DomainEvent
{
    public Guid TokenId { get; set; }
    public string Token { get; set; }
    public string ReplacementToken { get; set; }

    public ReplaceTokenEvent(Guid tokenId, string token, string replacementToken)
    {
        TokenId = tokenId;
        Token = token;
        ReplacementToken = replacementToken;
    }
}
