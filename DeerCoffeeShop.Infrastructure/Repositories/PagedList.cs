﻿using DeerCoffeeShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeerCoffeeShop.Infrastructure.Repositories
{
    public class PagedList<T> : List<T>, IPagedResult<T>
    {
        public PagedList(IQueryable<T> source, int pageNo, int pageSize)
        {
            TotalCount = source.Count();
            PageCount = PagedList<T>.GetPageCount(pageSize, TotalCount);
            PageNo = pageNo;
            PageSize = pageSize;
            int skip = (PageNo - 1) * PageSize;

            AddRange(
                [.. source
                    .Skip(skip)
                    .Take(PageSize)]);
        }

        public PagedList(int totalCount, int pageNo, int pageSize, List<T> results)
        {
            TotalCount = totalCount;
            PageCount = PagedList<T>.GetPageCount(pageSize, TotalCount);
            PageNo = pageNo;
            PageSize = pageSize;
            AddRange(results);
        }

        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int PageNo { get; private set; }
        public int PageSize { get; private set; }

        private static int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
            {
                return 0;
            }
            int remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }

        public static async Task<IPagedResult<T>> CreateAsync(
            IQueryable<T> source,
            int pageNo,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            int count = await source.CountAsync(cancellationToken);
            int skip = (pageNo - 1) * pageSize;

            List<T> results = await source
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return new PagedList<T>(count, pageNo, pageSize, results);
        }
    }
}
