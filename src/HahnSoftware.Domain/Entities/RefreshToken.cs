using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Events.RefreshToken;
using System.Security.Cryptography;

namespace HahnSoftware.Domain.Entities;

public sealed class RefreshToken : Entity
{
    public string Token { get; private set; } = Guid.CreateVersion7().ToString();
    public DateTimeOffset ExpiresOn { get; private set; }
    public DateTimeOffset? RevokationDate { get; private set; }
    public string? ReplacementToken { get; private set; }
    public string? RevokationReason { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public bool Active => RevokationDate == null && Expiration == false;
    public bool Expiration => DateTimeOffset.Now >= ExpiresOn;

    public RefreshToken(string token, DateTimeOffset expiresOn)
    {
        Token = token;
        ExpiresOn = expiresOn;
    }

    public void ReplaceToken(string token)
    {
        ReplacementToken = token;
        RevokationDate = DateTimeOffset.Now;
        AddDomainEvent(new ReplaceTokenEvent(Id, Token, ReplacementToken));
    }

    public void Revoke(string reason)
    {
        RevokationReason = reason;
        RevokationDate = DateTimeOffset.Now;
        AddDomainEvent(new RevokeTokenEvent(Id, Token));
    }
}
