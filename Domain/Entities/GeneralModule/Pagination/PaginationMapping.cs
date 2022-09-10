namespace Domain.Entities.GeneralModule.Pagination
{
    public static class PaginationMapping
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, int totalCount, string keyword)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, totalCount, keyword);

        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IEnumerable<TDestination> queryable, int pageNumber, int pageSize, int totalCount, string keyword)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, totalCount, keyword);

        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IEnumerable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

    }
}
