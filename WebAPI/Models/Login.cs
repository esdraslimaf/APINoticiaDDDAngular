namespace WebAPI.Models
{
    //DTO para facilitar ao passar o objeto na controller usuário
    public class Login
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Idade { get; set; }
        public string? Celular { get; set; }

    }
}
