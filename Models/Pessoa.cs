using System.ComponentModel.DataAnnotations;

namespace AspNetMVCeEF.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Nome da Pessoa")]
        [StringLength(50, MinimumLength = 8)]
        public string Nome { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
