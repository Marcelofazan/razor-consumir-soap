using System.ComponentModel;

namespace consumirSOAP.Models
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; } = string.Empty;

        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Senha")]
        public string Senha { get; set; } = string.Empty;
    }
}