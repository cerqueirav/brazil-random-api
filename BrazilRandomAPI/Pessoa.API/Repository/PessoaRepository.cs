using Pessoas.API.Models;

namespace Pessoas.API.Repository
{
    public class PessoaRepository :IPessoaRepository
    {
        #region ATRIBUTOS E CONSTRUTORES
        private string caminhoArquivo;
        private IConfiguration _configuration;
        public PessoaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminhoArquivo = _configuration.GetSection("FilePathPessoas").Value;
        }
        #endregion

        #region OBTER NOMES 

        public IEnumerable<NomePessoa> GetNomes(NomeFiltro nomeFiltro)
        {
            try
            {
                IEnumerable<NomePessoa> nomes = new List<NomePessoa>();

                nomes = (nomeFiltro is null) ? GetNomesByCsv() : GetNomesByCsv().Where(end => string.IsNullOrEmpty(nomeFiltro.Sexo) ? true : nomeFiltro.Sexo.Equals(end.Sexo));
                
                if (nomes is null || nomes.Count() == 0)
                    throw new Exception("Não foi possível consultar os nomes!");

                return nomes;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível consultar os nomes!");
            }

        }
        #endregion

        #region MÉTODOS PRIVADOS 
        private IEnumerable<NomePessoa> GetNomesByCsv()
        {
            List<NomePessoa> pessoas = new List<NomePessoa>();

            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                sr.ReadLine();

                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] partes = linha.Split(',');

                    NomePessoa pessoa = new NomePessoa
                    {
                        Nome = partes[0].Trim('"'),
                        Regiao = int.Parse(partes[1]),
                        Frequencia = int.Parse(partes[2]),
                        Rank = int.Parse(partes[3]),
                        Sexo = partes[4].Trim('"')
                    };

                    pessoas.Add(pessoa);
                }
            }

            return pessoas;
        }
        #endregion
    }
}
