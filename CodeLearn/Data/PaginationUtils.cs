using System.Linq;

namespace CodeLearn.Data
{
    //paging is one-indexed
    public static class PaginationUtils
    {
        public static int GetPageCount(int pageSize, int allItemsCount)
        {
            int pageCount = allItemsCount / pageSize;

            if (allItemsCount - (pageSize * pageCount) > 0)
                pageCount++;

            return pageCount;
        }

        public static IQueryable<T> TakeFromPage<T>(this IQueryable<T> query, int pageNumber, int pagesize)
        {
            return query.Skip(pagesize * (pageNumber - 1))
                        .Take(pagesize);
        }
    }
}
