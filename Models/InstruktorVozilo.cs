using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class InstruktorVozilo
    {
        [Key]
        public int ID { get; set; }
        public Instruktor Instruktor { get; set; }
        public Vozilo Vozilo { get; set; }
    }
}