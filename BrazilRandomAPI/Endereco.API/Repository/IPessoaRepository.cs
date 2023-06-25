using Pessoas.API.Models;

namespace Pessoas.API.Repository
{
    public interface IPessoaRepository
    {
        IEnumerable<NomePessoa> GetNomes(NomeFiltro nomeFiltro = null);
    }
}
