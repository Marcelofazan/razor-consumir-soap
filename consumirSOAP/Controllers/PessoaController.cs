using consumirSOAP.Models;
using consumirSOAP.Service;
using Microsoft.AspNetCore.Mvc;

namespace consumirSOAP.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaService _soapService;

        // Injeção de dependência
        public PessoaController(IPessoaService soapService)
        {
            _soapService = soapService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pessoa = await _soapService.GetAll();

            if (pessoa == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(pessoa);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string email)
        {
            var pessoa = await _soapService.GetByEmail(email);

            if (pessoa == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(pessoa);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pessoa novapessoa)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Dados inválidos. Verifique os campos.";
                return View(novapessoa);
            }
            try
            {
                var objpessoa = new Pessoa();
                objpessoa.Nome = novapessoa.Nome;
                objpessoa.Email = novapessoa.Email;
                objpessoa.Senha = novapessoa.Senha;

                var pessoainclui = await _soapService.Create(objpessoa.Nome, objpessoa.Email, objpessoa.Senha);

                if (pessoainclui == null)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
                else
                {
                    TempData["Mensagem"] = pessoainclui;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível inserir a pessoa.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string email)
        {
            var pessoa = await _soapService.GetByEmail(email);

            if (pessoa == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string nome, string email,  string senha)
        {
            if (email == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            try
            {
                var pessoaaltera = await _soapService.Update(nome, email, senha);

                if (pessoaaltera == null)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
                else
                {
                    TempData["Mensagem"] = pessoaaltera;
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível alterar a pessoa.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string email)
        {
            var pessoa = await _soapService.GetByEmail(email);

            if (pessoa == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            var pessoa = await _soapService.GetByEmail(email);

            if (pessoa == null)
            {
                ViewBag.Erro = "Registro não encontrado.";
                return View("Error");
            }

            try
            {
                var pessoadeleta = await _soapService.Delete(email);

                if (pessoadeleta == null)
                {
                    ViewBag.Erro = "Registro não encontrado.";
                    return View("Error");
                }
                else
                {
                    TempData["Mensagem"] = pessoadeleta;
                }

            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Não foi possível deletar a pessoa.";
                Console.WriteLine($"Exceção: {ex.Message}");
                return View("Error");
            }

            return View();
        }
    }
}
