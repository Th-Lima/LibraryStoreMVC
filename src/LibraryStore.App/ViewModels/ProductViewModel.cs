using System.ComponentModel.DataAnnotations;

namespace LibraryStore.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid ProviderId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo nome é descrução é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Imagem")]
        public string Image { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public decimal Price { get; set; }

        [Display(Name = "Data Cadastro")]
        [ScaffoldColumn(false)]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        [Display(Name = "Fornecedor")]
        public ProviderViewModel Provider { get; set; }

        public IEnumerable<ProviderViewModel> Providers { get; set; }
    }
}
