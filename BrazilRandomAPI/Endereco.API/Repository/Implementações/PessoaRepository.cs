using CepBrazilRandomAPI.Models;

namespace CepBrazilRandomAPI.Repository.Implementações
{
    public class PessoaRepository
    {
        #region ATRIBUTOS E CONSTRUTORES
        private string caminhoArquivo;
        private IConfiguration _configuration;
        public PessoaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminhoArquivo = _configuration.GetSection("FilePath").GetSection("Pessoas").Value;
        }
        #endregion

        public IEnumerable<Pessoa> GetPessoas(string sexo)
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                sr.ReadLine();

                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] partes = linha.Split(',');

                    Pessoa pessoa = new Pessoa
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
    }
}
