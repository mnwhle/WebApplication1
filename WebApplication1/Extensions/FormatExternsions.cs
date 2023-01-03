namespace WebApplication1.Extensions;

static class FormatExternsions
{
    private static string FormatCollection<T>(this ICollection<T> items, int maxcount, char open_bracket, char close_bracket)
    {
        Debug.Assert(maxcount > 2);
        int count = items.Count;
        if (count == 0)
        {
            return open_bracket + " " + close_bracket;
        }
        if (count > maxcount)
        {
            return open_bracket + " " + string.Join(" ", items.First(), $".. too many values ({count}) ..", items.Last()) + " " + close_bracket;
        }
        return open_bracket + " " + string.Join(" ", items) + " " + close_bracket;
    }

    public static string FormatCollectionArray<T>(this ICollection<T> items, int maxcount) => items.FormatCollection(maxcount, '[', ']');
    public static string FormatCollectionHash<T>(this ICollection<T> items, int maxcount) => items.FormatCollection(maxcount, '{', '}');
}
