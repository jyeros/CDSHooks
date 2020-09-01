using CDSHooks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CDSHooks.Data.ModelsConfiguration
{
    public class HookConfiguration : IEntityTypeConfiguration<Hook>
    {
        public void Configure(EntityTypeBuilder<Hook> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Context).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IList<HookContext>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
