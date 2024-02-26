namespace MyFinances.Common.Core.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; }
    public string CreatedBy { get; private set; } = null!;
    public DateTime? LastUpdatedAt { get; private set; }
    public string? LastUpdatedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public void SetCreated(string requestUser, DateTime requestDate)
    {
        CreatedAt = requestDate;
        CreatedBy = requestUser;
    }

    public void SetUpdated(string requestUser, DateTime requestDate)
    {
        LastUpdatedAt = requestDate;
        LastUpdatedBy = requestUser;
    }

    public void SetDeleted(string requestUser, DateTime requestDate)
    {
        IsDeleted = true;
        SetUpdated(requestUser, requestDate);
    }
}