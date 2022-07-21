using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool Active { get; set; }

        /* EF Relation */
        public Provider Provider { get; set; }
    }
}
