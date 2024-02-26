using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinances.Common.Core.Domain;

namespace MyFinances.Common.Core.Persistence.Mappings;

public abstract class BaseEntityMapping<T> : IBaseEntityMapping where T : BaseEntity
{
    protected EntityTypeBuilder<T> Builder = null!;
    protected abstract void MapProperties();
    protected abstract void MapIndexes();
    protected abstract void MapForeignKeys();
    protected abstract string TableName { get; }

    public void Map(ModelBuilder modelBuilder)
    {
        Builder = modelBuilder.Entity<T>();
        MapBaseProperties();
        MapProperties();
        MapForeignKeys();
        MapIndexes();
        Builder.ToTable(TableName);
    }

    private void MapBaseProperties()
    {
        Builder.HasKey(x => x.Id);
        Builder.Property(x => x.CreatedAt).IsRequired();
        Builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(100);
        Builder.Property(x => x.LastUpdatedBy).HasMaxLength(100);
        Builder.Property(x => x.IsDeleted).IsRequired();
    }
}