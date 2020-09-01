using CDSHooks.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CDSHooks.Data.ModelsConfiguration
{
    public class CDSServiceConfiguration : IEntityTypeConfiguration<CDSService>
    {
        public void Configure(EntityTypeBuilder<CDSService> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Prefetch).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IDictionary<string, string>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
