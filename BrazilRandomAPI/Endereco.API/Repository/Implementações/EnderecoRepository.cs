using CepBrazilRandomAPI.Models;
using CepBrazilRandomAPI.Repository.Interfaces;

namespace CepBrazilRandomAPI.Repository.Implementações
{
    public class EnderecoRepository : IEnderecoRepository
    {
        #region ATRIBUTOS E CONSTRUTORES
        private string caminhoArquivo;
        private IConfiguration _configuration;
        public EnderecoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            caminhoArquivo = _configuration.GetSection("FilePath").GetSection("Enderecos").Value;
        }
        #endregion

        #region OBTER ENDERECOS 
        
        public IEnumerable<Endereco> GetEnderecos(EnderecoFiltro enderecoFiltro)
        {
            try
            {
                IEnumerable<Endereco> enderecos = new List<Endereco>();

                if (enderecoFiltro is not null)
                {
                    enderecos = GetEnderecosByTxt()
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.cidade) ? true : enderecoFiltro.cidade.Equals(end.Cidade))
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.estado) ? true : enderecoFiltro.estado.Equals(end.Estado))
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.bairro) ? true : enderecoFiltro.estado.Equals(end.Bairro));
                }
                else
                    enderecos = GetEnderecosByTxt();
                
                if (enderecos is null || enderecos.Count() == 0)
                    throw new Exception("Não foi possível consultar os endereços!");

                return enderecos;
            }
            catch(Exception) 
            {
                throw new Exception("Não foi possível consultar os endereços!");
            }
           
        }
        #endregion

        #region MÉTODOS PRIVADOS
        private IEnumerable<Endereco> GetEnderecosByTxt()
        {
            List<Endereco> enderecos = new List<Endereco>();

            string[] linhas = GetLinhasArquivo();

            foreach (string linha in linhas)
                enderecos.Add(GetEnderecoPorLinha(linha));

            return enderecos;
        }

        private Endereco GetEnderecoPorLinha(string linha)
        {
            string[] partes = linha.Split('\t');

            Endereco endereco = new Endereco
            {
                CEP = partes[0],
                Bairro = partes[2],
                Logradouro = partes[3]
            };

            string cidadeEstado = partes[1];
            string[] cidadeEstadoParts = cidadeEstado.Split('/');

            if (cidadeEstadoParts.Length > 1)
            {
                endereco.Cidade = cidadeEstadoParts[0].Trim();
                endereco.Estado = cidadeEstadoParts[1].Trim();
            }

            return endereco;
        }
        private string[] GetLinhasArquivo()
        {
            return File.ReadAllLines(caminhoArquivo);
        }
        #endregion
    }
}
