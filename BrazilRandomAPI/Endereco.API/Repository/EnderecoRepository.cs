using Enderecos.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Enderecos.API.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        #region ATRIBUTOS E CONSTRUTORES
        private IConfiguration _configuration;
        public EnderecoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region LISTAR ENDERECOS
        public IEnumerable<Endereco> GetEnderecos()
        {
            try
            {
                var enderecos = GetEnderecosFromDb();

                if (enderecos is null)
                    throw new Exception("Não foi possível consultar os endereços!");

                return enderecos;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível consultar os endereços!");
            }
        }
        #endregion

        #region LISTAR ENDERECOS C/ FILTRO
        public IEnumerable<Endereco> GetEnderecosComFiltro(EnderecoFiltro enderecoFiltro)
        {
            try
            {
                var enderecos = GetEnderecosFromDb()
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.Cidade) ? true : enderecoFiltro.Cidade.Equals(end.Cidade))
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.Estado) ? true : enderecoFiltro.Estado.Equals(end.Estado))
                                .Where(end => string.IsNullOrEmpty(enderecoFiltro.Bairro) ? true : enderecoFiltro.Bairro.Equals(end.Bairro));

                if (enderecos is null)
                    throw new Exception("Não foi possível consultar os endereços!");

                return enderecos;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível consultar os endereços!");
            }
        }
        #endregion

        #region LISTAR POR CEP
        public Endereco GetEnderecoByCep(string cep)
        {
            try
            {
                var endereco = GetEnderecoByCepFromDb(cep);

                if (endereco is null)
                    throw new Exception("O endereço não foi localizado!");

                return endereco;
            }
            catch (Exception)
            {
                throw new Exception("O endereço não foi localizado!");
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
        private IEnumerable<Endereco> GetEnderecosFromDb()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM ENDERECO";

                        try
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var enderecos = new List<Endereco>();

                                while (reader.Read())
                                {
                                    var endereco = new Endereco
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        CEP = reader["CEP"].ToString(),
                                        Cidade = reader["Cidade"].ToString(),
                                        Estado = reader["Estado"].ToString(),
                                        Bairro = reader["Bairro"].ToString(),
                                        Logradouro = reader["Logradouro"].ToString(),
                                    };

                                    enderecos.Add(endereco);
                                }

                                return enderecos;
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("Não foi possível listar os endereços!");
                        }
                    }
                }
            }
            catch
            {
                return Enumerable.Empty<Endereco>();
            }
        }
        private Endereco? GetEnderecoByCepFromDb(string cep)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM ENDERECO WHERE CEP = @Cep";
                        command.Parameters.AddWithValue("@Cep", cep);

                        try
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var endereco = new Endereco
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        CEP = reader["CEP"].ToString(),
                                        Cidade = reader["Cidade"].ToString(),
                                        Estado = reader["Estado"].ToString(),
                                        Bairro = reader["Bairro"].ToString(),
                                        Logradouro = reader["Logradouro"].ToString(),
                                    };

                                    return endereco;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw new Exception("Não foi possível localizar o endereço!");
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Não foi possível localizar o endereço!");
            }
        }
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

                            command.CommandText = "INSERT INTO Endereco (CEP, Cidade, Estado, Bairro, Logradouro) VALUES (@CEP, @Cidade, @Estado, @Bairro, @Logradouro)";

                            command.Parameters.Add("@CEP", SqlDbType.VarChar);
                            command.Parameters.Add("@Cidade", SqlDbType.VarChar);
                            command.Parameters.Add("@Estado", SqlDbType.VarChar);
                            command.Parameters.Add("@Bairro", SqlDbType.VarChar);
                            command.Parameters.Add("@Logradouro", SqlDbType.VarChar);

                            string[] linhas = GetLinhasArquivo();

                            foreach (string linha in linhas)
                            {
                                var endereco = GetEnderecoPorLinha(linha);

                                if (endereco is not null)
                                {
                                    command.Parameters["@CEP"].Value = endereco.CEP;
                                    command.Parameters["@Cidade"].Value = endereco.Cidade;
                                    command.Parameters["@Estado"].Value = endereco.Estado;
                                    command.Parameters["@Bairro"].Value = endereco.Bairro;
                                    command.Parameters["@Logradouro"].Value = endereco.Logradouro;
                                }

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o dataset!");
            }
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
            return File.ReadAllLines(_configuration.GetSection("FilePathEnderecos").Value);
        }
        #endregion
    }
}
