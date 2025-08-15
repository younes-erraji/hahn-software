namespace HahnSoftware.Domain.Events.RefreshToken;

public class RevokeTokenEvent : DomainEvent
{
    public Guid TokenId { get; set; }
    public string Token { get; set; }

    public RevokeTokenEvent(Guid tokenId, string token)
    {
        TokenId = tokenId;
        Token = token;
    }
}
