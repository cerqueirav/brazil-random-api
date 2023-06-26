using Pessoas.API.Models;
using System.Data.SqlClient;

namespace Pessoas.API.Repository
{
    public class PessoaRepository :IPessoaRepository
    {
        #region ATRIBUTOS E CONSTRUTORES
        private IConfiguration _configuration;
        public PessoaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region LISTAR NOMES
        public IEnumerable<NomePessoa> GetNomes()
        {
            try
            {
                var nomes = GetNomesFromDb();

                if (nomes is null)
                    throw new Exception("Não foi possível consultar os nomes!");

                return nomes;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível consultar os nomes!");
            }
        }
        #endregion

        #region CREATE DATASET DB
        public void CreateDataset()
        {
            try
            {
                InsertRangeDataset();
            }
            catch
            {
                throw new Exception("Não foi possível criar o dataset!");
            }
        }
        #endregion

        #region MÉTODOS PRIVADOS 
        private void InsertRangeDataset()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;

                            command.CommandText = "INSERT INTO Pessoa (Nome, Frequencia, Rank, Sexo) VALUES (@Nome, @Frequencia, @Rank, @Sexo)";

                            using (StreamReader sr = new StreamReader(_configuration.GetSection("FilePathPessoas").Value))
                            {
                                sr.ReadLine();

                                string linha;
                                while ((linha = sr.ReadLine()) != null)
                                {
                                    string[] partes = linha.Split(',');

                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@Nome", partes[0].Trim('"'));
                                    command.Parameters.AddWithValue("@Frequencia", partes[2]);
                                    command.Parameters.AddWithValue("@Rank", partes[3]);
                                    command.Parameters.AddWithValue("@Sexo", partes[4].Trim('"'));

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível salvar no banco de dados!", ex);
            }
        }

        private IEnumerable<NomePessoa> GetNomesFromDb()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM PESSOA";

                        try
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var nomes = new List<NomePessoa>();

                                while (reader.Read())
                                {
                                    var pessoa = new NomePessoa
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Nome = reader["Nome"].ToString(),
                                        Frequencia = reader["Frequencia"].ToString(),
                                        Rank = reader["Rank"].ToString(),
                                        Sexo = reader["Sexo"].ToString(),
                                    };

                                    nomes.Add(pessoa);
                                }

                                return nomes;
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("Não foi possível listar os nomes!");
                        }
                    }
                }
            }
            catch
            {
                return Enumerable.Empty<NomePessoa>();
            }
        }
        #endregion
    }
}
