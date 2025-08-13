using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HahnSoftware.Infrastructure.Persistence.Converters;

internal class StringConverter : ValueConverter<string, string>
{
    public StringConverter() : base(
        x => x,
        x => x.Trim()
    )
    { }
}
