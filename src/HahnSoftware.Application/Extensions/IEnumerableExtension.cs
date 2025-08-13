namespace HahnSoftware.Application.Extensions;

public static class IEnumerableExtension
{
    public static bool IsNotEmpty<T>(this IEnumerable<T> list)
    {
        return list is not null && list.Any();
    }

    public static bool IsEmpty<T>(this IEnumerable<T> list)
    {
        return list is null || list.Any() == false;
    }
}
