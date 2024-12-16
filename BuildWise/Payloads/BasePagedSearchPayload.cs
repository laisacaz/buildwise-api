namespace BuildWise.Payload
{
    public class BasePagedSearchPayload
    {
        private int? _pageNumber;
        private int? _pageSize;
        /// <summary>
        /// Default is 1
        /// </summary>
        public int? PageNumber
        {
            get => _pageNumber > 0 ? _pageNumber : 1;
            set => _pageNumber = value;
        }
        /// <summary>
        /// Default is 10
        /// </summary>
        public int? PageSize
        {
            get => _pageSize > 0 ? _pageSize : 10;
            set => _pageSize = value;
        }
        //quantos registros pular
        public int GetOffSet()
        {
            return (PageNumber.GetValueOrDefault() - 1) * PageSize.GetValueOrDefault();
        }
        //limite de registros na pagina
        public int GetLimit() => PageSize.GetValueOrDefault();
    }
}
