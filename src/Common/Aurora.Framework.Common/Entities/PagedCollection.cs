namespace Aurora.Framework.Entities
{
    public class PagedCollection<T> where T : class
    {
        public IReadOnlyList<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string Description => ToString();
        public bool HasItems => Items != null && Items.Count > 0;
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public override string ToString()
        {
            return string.Format("Page {0} of {1}. Total records: {2}.", CurrentPage, TotalPages, TotalItems);
        }
    }
}