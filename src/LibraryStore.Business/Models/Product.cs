using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Models
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public string Image { get; set; }

        [Display(Name = "Valor")]
        public decimal Price { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        /* EF Relation */
        [Display(Name = "Fornecedor")]
        public Provider Provider { get; set; }
    }
}
