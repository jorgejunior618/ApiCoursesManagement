using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "O login é obrigatório")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
