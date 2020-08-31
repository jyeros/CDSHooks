using CDSHooks.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace CDSHooks.Data.ModelsConfiguration
{
    public class HookConfiguration : IEntityTypeConfiguration<Hook>
    {
        public void Configure(EntityTypeBuilder<Hook> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Context).HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { IgnoreNullValues = true }),
                v => JsonSerializer.Deserialize<IList<HookContext>>(v, new JsonSerializerOptions { IgnoreNullValues = true }));
        }
    }
}
