using CepBrazilRandomAPI.Models;
using CepBrazilRandomAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CepBrazilRandomAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private IEnderecoRepository _mapperCep;

        public EnderecoController(IEnderecoRepository mapperCep)
        {
            _mapperCep = mapperCep;
        }

        [HttpGet(Name = "ListarEnderecos")]
        public ActionResult ListarEnderecos()
        {
            try
            {
                IEnumerable<Endereco> enderecos = _mapperCep.GetEnderecos(new EnderecoFiltro());

                if (enderecos is null || enderecos.Count() == 0)
                    return BadRequest("Erro: n�o foi poss�vel listar os endere�os!");

                return Ok(enderecos);
            }
            catch (Exception)
            {
                return BadRequest("Erro: n�o foi poss�vel listar os endere�os!");
            }
        }

        [HttpGet("{cep}")]
        public ActionResult BuscarPorCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return BadRequest("O CEP informado � inv�lido!");

            try
            {
                var endereco = _mapperCep.GetEnderecos(new EnderecoFiltro()).Where(end => end.CEP.Equals(cep)).FirstOrDefault();
                return (endereco is null) ? NotFound("O endere�o n�o foi localizado!") : Ok(endereco);
            }
            catch(Exception)
            {
                return BadRequest("Ocorreu um erro ao pesquisar o CEP!");
            }
        }

        [HttpPost("Aleatorio")]
        public ActionResult BuscarEnderecoAleatorio([FromBody] EnderecoFiltro? enderecoFiltro)
        {
            try
            {
                IEnumerable<Endereco> enderecos = _mapperCep.GetEnderecos(enderecoFiltro);
                Endereco enderecoAleatorio = new Endereco();

                if (enderecos is null || enderecos.Count() == 0)
                    return BadRequest("N�o foi poss�vel gerar o endere�o!");

                if (enderecos.Any())
                {
                    Random random = new Random();
                    int index = random.Next(0, enderecos.Count());
                    enderecoAleatorio = enderecos.ElementAt(index);
                }
                return Ok(enderecoAleatorio);
            }
            catch (Exception)
            {
                return BadRequest("N�o foi poss�vel gerar o endere�o!");
            }
        }
    }
}