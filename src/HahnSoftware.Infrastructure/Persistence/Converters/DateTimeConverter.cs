using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System.Data.SqlTypes;

namespace HahnSoftware.Infrastructure.Persistence.Converters;

public class DateTimeConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeConverter() : base(
        x => x,
        x => x.Year < 1753 ? SqlDateTime.MinValue.Value : x > SqlDateTime.MaxValue.Value ? SqlDateTime.MaxValue.Value : x
    )
    { }
}
