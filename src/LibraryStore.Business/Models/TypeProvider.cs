using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public enum TypeProvider
    {
        [Display(Name = "Pessoa Física")]
        PhysicalPerson = 1,

        [Display(Name = "Pessoa Jurídica")]
        LegalPerson = 2
    }
}
