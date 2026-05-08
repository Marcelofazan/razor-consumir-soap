using consumirSOAP.Models;

namespace consumirSOAP.Service
{
    public interface IPessoaService
    {
        Task<Pessoa> GetByEmail(string email);
        Task<IEnumerable<Pessoa>> GetAll();
        Task<string> Create(string Nome, string Email, string Senha);
        Task<string> Update(string Nome, string Email, string Senha);
        Task<string> Delete(string email);
    }
}
