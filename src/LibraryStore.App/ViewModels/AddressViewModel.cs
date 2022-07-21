using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryStore.App.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string AddressPlace { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string NumberAddress { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Complement { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string State { get; set; }

        [HiddenInput]
        public Guid ProviderId { get; set; }
    }
}
