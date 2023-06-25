using Microsoft.AspNetCore.Mvc;
using Pessoas.API.Models;
using Pessoas.API.Repository;

namespace Pessoas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet(Name = "ListarPessoas")]
        public ActionResult ListarNomes()
        {
            try
            {
                IEnumerable<NomePessoa> nomes = _pessoaRepository.GetNomes();

                if (nomes is null || nomes.Count() == 0)
                    return BadRequest("Erro: não foi possível listar de nomes!");

                return Ok(nomes);
            }
            catch (Exception)
            {
                return BadRequest("Erro: não foi possível listar os nomes!");
            }
        }

        [HttpPost("Aleatorio")]
        public ActionResult BuscarNomeAleatorio(NomeFiltro? nomeFiltro)
        {
            try
            {
                IEnumerable<NomePessoa> nomes = _pessoaRepository.GetNomes(nomeFiltro);
                NomePessoa nomeAleatorio = new NomePessoa();

                if (nomes is null || nomes.Count() == 0)
                    return BadRequest("Não foi possível gerar o nome!");

                if (nomes.Any())
                {
                    Random random = new Random();
                    int index = random.Next(0, nomes.Count());
                    nomeAleatorio = nomes.ElementAt(index);
                }
                return Ok(nomeAleatorio);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível gerar o nome!");
            }
        }
    }
}