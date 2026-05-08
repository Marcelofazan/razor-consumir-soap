using consumirSOAP.Models;
using PessoaServiceSoapReference;
using System.ServiceModel;
using Pessoa = consumirSOAP.Models.Pessoa;

namespace consumirSOAP.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PessoaService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Pessoa>> GetAll()
        {
            var pessoasArray = new List<Pessoa>();
            using (PessoaServiceSoapClient client = new PessoaServiceSoapClient(PessoaServiceSoapClient.EndpointConfiguration.PessoaServiceSoap))
            {
                try
                {
                    var request = await client.BuscaPessoasAsync();
                    var listaPessoas = request.Body.BuscaPessoasResult;

                    if (listaPessoas == null)
                    {
                        return Enumerable.Empty<Pessoa>();
                    }
                    foreach (var p in listaPessoas)
                    {
                        pessoasArray.Add(new Pessoa
                        {
                            Nome = p.Nome,
                            Email = p.Email,
                            Senha = p.Senha
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    client.Abort();
                }
                finally
                {
                    // Garante que o cliente seja fechado se não foi abortado
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
            }
            return pessoasArray ?? Enumerable.Empty<Pessoa>();
        }

        public async Task<Pessoa> GetByEmail(string email)
        {
            var objpessoa = new Pessoa();
            using (PessoaServiceSoapClient client = new PessoaServiceSoapClient(PessoaServiceSoapClient.EndpointConfiguration.PessoaServiceSoap))
            {
                try
                {
                    var request = await client.BuscaporEmailAsync(email);

                    var pessoa = request.Body.BuscaporEmailResult;

                    if (pessoa == null)
                    {
                        return new Pessoa();
                    }

                    objpessoa.Nome = pessoa.Nome;
                    objpessoa.Email = pessoa.Email;
                    objpessoa.Senha = pessoa.Senha;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    client.Abort();
                }
                finally
                {
                    // Garante que o cliente seja fechado se não foi abortado
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
            }
            return objpessoa;
        }

        public async Task<string> Create(string nome, string email, string senha)
        {
            string msg = string.Empty;
            using (PessoaServiceSoapClient client = new PessoaServiceSoapClient(PessoaServiceSoapClient.EndpointConfiguration.PessoaServiceSoap))
            {
                try
                {
                    var request = await client.InserirPessoaAsync(nome, email, senha);
                    msg = request.Body.InserirPessoaResult;

                    if (msg == null)
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    client.Abort();
                }
                finally
                {
                    // Garante que o cliente seja fechado se não foi abortado
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
            }
            return msg;
        }

        public async Task<string> Update(string nome, string email, string senha)
        {
            string msg = string.Empty;
            using (PessoaServiceSoapClient client = new PessoaServiceSoapClient(PessoaServiceSoapClient.EndpointConfiguration.PessoaServiceSoap))
            {
                try
                {
                    var request = await client.AlterarPessoaAsync(nome, email, senha);
                    msg = request.Body.AlterarPessoaResult;

                    if (msg == null)
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    client.Abort();
                }
                finally
                {
                    // Garante que o cliente seja fechado se não foi abortado
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
            }
            return msg;
        }

        public async Task<string> Delete(string email)
        {
            string msg = string.Empty;
            using (PessoaServiceSoapClient client = new PessoaServiceSoapClient(PessoaServiceSoapClient.EndpointConfiguration.PessoaServiceSoap))
            {
                try
                {
                    var request = await client.DeletarPessoaAsync(email);

                    msg = request.Body.DeletarPessoaResult;

                    if (msg == null)
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    client.Abort();
                }
                finally
                {
                    // Garante que o cliente seja fechado se não foi abortado
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
            }
            return msg;
        }
    }
}
