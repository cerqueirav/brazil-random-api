using Enderecos.API.Models;

namespace Enderecos.API.Repository
{
    public interface IEnderecoRepository
    {
        IEnumerable<Endereco> GetEnderecos();
        IEnumerable<Endereco> GetEnderecosComFiltro(EnderecoFiltro enderecoFiltro);
        Endereco GetEnderecoByCep(string cep);
        void CreateDataset();
    }
}
