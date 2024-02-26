namespace MyFinances.Common.Core.RequestContext;

public interface IRequestContext
{
    string User { get; set; }
    Guid CompanyId { get; set; }
}