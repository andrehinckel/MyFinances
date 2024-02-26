using Microsoft.EntityFrameworkCore;

namespace MyFinances.Common.Core.Persistence.Mappings;

public interface IBaseEntityMapping
{
    void Map(ModelBuilder modelBuilder);
}