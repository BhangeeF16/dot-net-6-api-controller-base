using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Domain.IServices.IMapperServices;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, int totalCount, string keyword)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, totalCount, keyword);

    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IEnumerable<TDestination> queryable, int pageNumber, int pageSize, int totalCount, string keyword)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, totalCount, keyword);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
        => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
}
