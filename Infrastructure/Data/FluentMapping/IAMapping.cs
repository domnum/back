using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

public class IAMapping : IEntityTypeConfiguration<IA>
{
    public void Configure(EntityTypeBuilder<IA> builder)
    {
        // Table
        builder.ToTable("IAs");

        // Primary Key
        builder.HasKey(i => i.Id).HasName("PK_IAs");

        // Properties
        builder.Property(i => i.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(i => i.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(i => i.UpdatedDate)
            .HasColumnName("UpdatedDate")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(i => i.DeletedDate)
            .HasColumnName("DeletedDate")
            .HasColumnType("timestamp")
            .IsRequired(false);

        // Value Object: Name
        builder.OwnsOne(i => i.Name, name =>
        {
            name.Property(n => n.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        });

        // Relacionamento com Picture
        builder.HasOne(i => i.Picture)
            .WithMany()
            .HasForeignKey("PictureId")
            .OnDelete(DeleteBehavior.SetNull);

        // Relacionamento com Courses
        builder.HasMany(i => i.Courses)
            .WithOne(c => c.IA)
            .HasForeignKey("IAId")
            .OnDelete(DeleteBehavior.SetNull);
    }
}
