using Enderecos.API.Models;

namespace Enderecos.API.Repository
{
    public interface IEnderecoRepository
    {
        IEnumerable<Endereco> GetEnderecos(EnderecoFiltro enderecoFiltro);
    }
}
