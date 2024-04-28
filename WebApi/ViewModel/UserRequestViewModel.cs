using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModel
{
    public record UserRequestViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A Senha é obrigatório")]
        [DataType(DataType.Password, ErrorMessage = "Senha inválida")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "A Role é obrigatório")]
        public string Role { get; set; }
    }
}
