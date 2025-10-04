using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Housing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Property> Properties { get; set; }
    }
}
