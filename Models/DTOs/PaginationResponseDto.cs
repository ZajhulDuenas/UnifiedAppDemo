
namespace Models.DTOs
{
    public class PaginationResponseDto<T>
    where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int SizePage { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
