namespace WebAPI.DTOs.Admin_DTO
{
    public class GetListPhieuMuonPaging
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }
    public class PagingResult<T>
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }
        public List<T> Results { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }
    }


}
