namespace WebApplication1.Extensions;

static class HibernateExternsions
{
    public static string IndexName(this string stem) => $"I_{stem}";
    public static string UniqueIndexName(this string stem) => $"U_{stem}";
}
