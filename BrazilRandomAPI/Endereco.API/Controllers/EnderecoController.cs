using Enderecos.API.Models;
using Enderecos.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Enderecos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private IEnderecoRepository _enderecoRepository;

        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet("{cep}")]
        public ActionResult BuscarPorCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return BadRequest("O CEP informado � inv�lido!");

            try
            {
                var endereco = _enderecoRepository.GetEnderecoByCep(cep);
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
                IEnumerable<Endereco> enderecos = (enderecoFiltro is null)
                    ? _enderecoRepository.GetEnderecos()
                    : _enderecoRepository.GetEnderecosComFiltro(enderecoFiltro);
                        
                if (enderecos == null || enderecos.Count() == 0)
                    return BadRequest("N�o foi poss�vel gerar o endere�o!");

                int qtdEnderecos = enderecoFiltro?.Quantidade ?? 1;

                Random random = new Random();
                List<Endereco> enderecosAleatorios = enderecos.OrderBy(_ => random.Next()).Take(qtdEnderecos).ToList();

                return Ok(enderecosAleatorios);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao gerar o endere�o: " + ex.Message);
            }
        }

        [HttpGet("PopularBanco")]
        public ActionResult PopularBanco()
        {
            try
            {
                _enderecoRepository.CreateDataset();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("N�o foi poss�vel popular o banco de dados!");
            }
        }
    }
}