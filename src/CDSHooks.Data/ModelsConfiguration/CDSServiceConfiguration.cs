using CDSHooks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace CDSHooks.Data.ModelsConfiguration
{
    public class CDSServiceConfiguration : IEntityTypeConfiguration<CDSService>
    {
        public void Configure(EntityTypeBuilder<CDSService> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Prefetch).HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true }),
                v => JsonSerializer.Deserialize<IDictionary<string, string>>(v, new JsonSerializerOptions { IgnoreNullValues = true }));
        }
    }
}
