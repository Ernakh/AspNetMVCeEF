using System.ComponentModel.DataAnnotations;

namespace AspNetMVCeEF.Models
{
    public class Email
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Email da Pessoa")]
        public string email { get; set; }

        public virtual Pessoa pessoa { get; set; }
    }
}
