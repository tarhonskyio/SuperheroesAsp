namespace SuperheroesAspNet.Models
{

    public class PagingViewModel<T>
    {
        public IEnumerable<T> Items { get; set; } 
        public int CurrentPage { get; set; } 
        public int TotalPages { get; set; } 
    }
}