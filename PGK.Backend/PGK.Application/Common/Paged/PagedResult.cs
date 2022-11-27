namespace PGK.Application.Common.Paged
{
    public abstract class PagedResult<T>
    {
        public int CurrentPage
        {
            get
            {
                return Results.CurrentPage;
            }
        }
        public int TotalPages
        {
            get
            {
                return Results.TotalPages;
            }
        }
        public int PageSize
        {
            get
            {
                return Results.PageSize;
            }
        }
        public int TotalCount
        {
            get
            {
                return Results.TotalCount;
            }
        }

        public bool HasPrevious
        {
            get
            {
                return Results.HasPrevious;
            }
        }
        public bool HasNext
        {
            get
            {
                return Results.HasNext;
            }
        }

        public abstract PagedList<T> Results { get; set; }
    }
}
