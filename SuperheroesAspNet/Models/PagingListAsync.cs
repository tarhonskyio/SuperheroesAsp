using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperheroesAspNet.Models
{
    public class PagingListAsync<T>
    {
        public IQueryable<T> Data { get; } 
        public int TotalItems { get; }
        public int TotalPages { get; }
        public int Page { get; }
        public int Size { get; }
        public bool IsFirst { get; }
        public bool IsLast { get; }
        public bool IsNext => !IsLast;
        public bool IsPrevious => !IsFirst;

        public PagingListAsync(IQueryable<T> data, int totalItems, int page, int size)
        {
            TotalItems = totalItems;
            Page = page;
            Size = size;
            TotalPages = (int)Math.Ceiling(totalItems / (double)size);
            IsFirst = Page <= 1;
            IsLast = Page >= TotalPages;
            Data = data;
        }

        public static async Task<PagingListAsync<T>> CreateAsync(
            Func<int, int, IQueryable<T>> dataGenerator,
            int totalItems,
            int page,
            int size)
        {
            int clippedPage = Math.Max(1, Math.Min(page, (int)Math.Ceiling(totalItems / (double)size)));

            var data = dataGenerator(clippedPage, size)
                .Skip((clippedPage - 1) * size)
                .Take(size);

            return new PagingListAsync<T>(data, totalItems, clippedPage, size);
        }
    }
}