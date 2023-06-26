using System.ComponentModel;

namespace Enderecos.API.Models
{
    public class EnderecoFiltro
    {
        [DefaultValue("")]
        public string? Bairro { get; set; }
        
        [DefaultValue("")]
        public string? Cidade { get; set; }

        [DefaultValue("")]
        public string? Estado { get; set; }

        [DefaultValue(1)]
        public int Quantidade { get; set; } = 1;
    }
}
