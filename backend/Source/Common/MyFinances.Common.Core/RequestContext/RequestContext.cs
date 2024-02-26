namespace MyFinances.Common.Core.RequestContext;

public class RequestContext : IRequestContext
{
    public string User { get; set; }
    public Guid CompanyId { get; set; }
}