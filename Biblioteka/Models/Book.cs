namespace Biblioteka.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; } = true; // Domyślnie książka jest dostępna
    }
}
