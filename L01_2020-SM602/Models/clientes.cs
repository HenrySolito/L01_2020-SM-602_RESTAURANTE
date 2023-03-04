using System.ComponentModel.DataAnnotations;
namespace L01_2020_SM602.Models
{
    public class clientes
    {
        [Key]
        public int clienteId { get; set; }
        public string nombreCLiente { get; set; }
        public string direccion { get; set; }
    }
}
