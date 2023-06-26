namespace Enderecos.API.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
    }
}
