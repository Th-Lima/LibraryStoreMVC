using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public class Address : Entity
    {
        public Guid ProviderId { get; set; }

        public string AddressPlace { get; set; }

        public string NumberAddress { get; set; }

        public string Complement { get; set; }

        public string ZipCode { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        /* EF Relation */
        public Provider Provider { get; set; }
    }
}
