namespace BuildWise.DTO
{
    public class BasePagedSearchDTO<T>
    {
            public ICollection<T> Data { get; set; }
            public int TotalRecords { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }

            public int TotalPages => TotalRecords / PageSize + (TotalRecords % PageSize > 0 ? 1 : 0);
       
    }
}
