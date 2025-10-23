using Domain.Enums;

namespace Domain.Entities
{
    public class PropertyDocument
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }

        // Fayl MinIO ga yuklanadi, URL yoki nomi shu yerda saqlanadi
        public string FileUrl { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CheckedAt { get; set; }
        public string? RejectionReason { get; set; }

        // Navigation
        public Property Property { get; set; }
    }
}
