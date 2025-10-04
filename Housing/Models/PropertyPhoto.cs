namespace Housing.Models
{
    public class PropertyPhoto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }

        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Property Property { get; set; }
    }
}
