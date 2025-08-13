using HahnSoftware.Domain.Entities.Primitives;

namespace HahnSoftware.Domain.Entities;

public sealed class RefreshToken : Entity
{
    public string Token { get; set; } = Guid.CreateVersion7().ToString();
    public DateTimeOffset ExpiresOn { get; set; }
    public DateTimeOffset? RevokationDate { get; set; }
    public bool Active => RevokationDate == null && Expiration == false;
    public string? ReplacementToken { get; set; }
    public string? RevokationReason { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public bool Expiration => DateTimeOffset.UtcNow >= ExpiresOn;
}
