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

        [HttpPost("Aleatorio")]
        public ActionResult BuscarNomeAleatorio()
        {
            try
            {
                IEnumerable<NomePessoa> nomes = _pessoaRepository.GetNomes();
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

        [HttpGet("PopularBanco")]
        public ActionResult PopularBanco()
        {
            try
            {
                _pessoaRepository.CreateDataset();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível popular o banco de dados!");
            }
        }
    }
}