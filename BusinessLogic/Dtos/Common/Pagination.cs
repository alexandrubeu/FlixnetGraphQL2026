namespace BusinessLogic.Dtos
{
    public class Pagination<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public List<T> Items { get; set; } = new();
    }
}