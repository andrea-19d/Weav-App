namespace Weav_App.Helpers.HelpersUI;

public class SearchHelper
{
    public static List<T> FilterByQuery<T>(List<T> source, string? query, Func<T, string[]> fieldsSelector)
    {
        if (string.IsNullOrWhiteSpace(query))
            return source;

        query = query.ToLowerInvariant();

        return source.Where(item =>
                fieldsSelector(item)
                    .Any(field => field?.ToLowerInvariant().Contains(query) ?? false))
            .ToList();
    }
}