using Weav_App.Models.HelpersModels;

public static class PaginationHelper
{
    public static PaginatedResult<T> Paginate<T>(List<T> items, int page, int pageSize)
    {
        int totalItems = items.Count;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var paginatedItems = items
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResult<T>
        {
            Items = paginatedItems,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }
}