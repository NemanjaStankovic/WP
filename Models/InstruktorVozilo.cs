using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class InstruktorVozilo
    {
        [Key]
        public int ID { get; set; }
        
        [JsonIgnore]
        public Instruktor Instruktor { get; set; }

        [JsonIgnore]
        public Vozilo Vozilo { get; set; }
    }
}