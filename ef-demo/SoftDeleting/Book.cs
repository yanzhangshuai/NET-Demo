namespace SoftDeleting
{
    public class Book:ISoftDelete
    {
         public int BookId { get; set; }
         public string Title { get; set; }
        public bool SoftDeleted { get; set; }
    }
}