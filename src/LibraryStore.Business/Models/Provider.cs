using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public class Provider : Entity
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Documento")]
        public string Document { get; set; }

        [Display(Name = "Tipo Fornecedor")]
        public TypeProvider TypeProvider { get; set; }

        [Display(Name = "Endereço")]
        public Address Address { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }

        /* EF Relations */
        IEnumerable<Product> Products { get; set; }
    }
}
