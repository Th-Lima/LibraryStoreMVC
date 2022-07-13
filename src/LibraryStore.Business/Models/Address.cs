using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public class Address : Entity
    {
        public Guid ProviderId { get; set; }

        [Display(Name = "Logradouro")]
        public string AddressPlace { get; set; }

        [Display(Name = "Número")]
        public string NumberAddress { get; set; }

        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Display(Name = "CEP")]
        public string ZipCode { get; set; }

        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        public string State { get; set; }

        /* EF Relation */
        public Provider Provider { get; set; }
    }
}
