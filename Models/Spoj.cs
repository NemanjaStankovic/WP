using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Models
{
    public class Spoj
    {
        [Key]
        public int ID { get; set; }
        
        [JsonIgnore]
        public Instruktor Instruktor { get; set; }

        [JsonIgnore]
        public Vozilo Vozilo { get; set; }

         [JsonIgnore]
        public List<Polaznik> Polaznici { get; set; }
    }
}