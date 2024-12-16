namespace BuildWise.DTO
{
    public class SearchDTO<T>
    {
        public SearchDTO(ICollection<T> data, int totalrecords)
        {
            Data = data;
            TotalRecords = totalrecords;
        }

        public ICollection<T> Data { get; }
        public int TotalRecords { get; }
    }

    public class SearchDTO<T, TTotalizer>
    {
        public SearchDTO(ICollection<T> data, int totalrecords, TTotalizer totalizer)
        {
            Search = new(data, totalrecords);
            Totalizer = totalizer;
        }

        public SearchDTO<T> Search { get; }
        public TTotalizer Totalizer { get; }
    }
}
