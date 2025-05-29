using EtlProject.Core.Enums;
using EtlProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EtlProject.DataAccess.Configurations;

public class ProcessingLogConfiguration : IEntityTypeConfiguration<ProcessingLog>
{
    public void Configure(EntityTypeBuilder<ProcessingLog> builder)
    {
        builder.HasKey(pl => pl.Id);

        builder
            .HasOne(pl => pl.JsonMessage)
            .WithOne()
            .HasForeignKey<ProcessingLog>(pl => pl.JsonId);

        builder
            .Property(pl => pl.Status)
            .HasConversion(new EnumToStringConverter<ProcessingStatus>());
    }
}