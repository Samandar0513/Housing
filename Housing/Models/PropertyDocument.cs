namespace Housing.Models
{
    public class PropertyDocument
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }

        public string CadastralNumber { get; set; }
        public string Status { get; set; }
        public DateTime? CheckedAt { get; set; }

        // Navigation
        public Property Property { get; set; }
    }
}
