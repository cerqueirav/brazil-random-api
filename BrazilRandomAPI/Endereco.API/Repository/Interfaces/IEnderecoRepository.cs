using CepBrazilRandomAPI.Models;

namespace CepBrazilRandomAPI.Repository.Interfaces
{
    public interface IEnderecoRepository
    {
        IEnumerable<Endereco> GetEnderecos(EnderecoFiltro enderecoFiltro);
    }
}
