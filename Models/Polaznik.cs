using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Polaznik
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression("\\d+")]
        [MaxLength(13)]
        public long JMBG { get; set; }

        [RegularExpression("\\d+")]
        [Required]
        [MaxLength(9)]
        public int BrLicneKarte { get; set; }
        public bool PolozioTest { get; set; }
        public bool PolozioVoznju{ get; set; }

        [RegularExpression("\\w+")]
        [MaxLength(15)]
        [Required]
        public string Ime { get; set; }

        [RegularExpression("\\w+")]
        [MaxLength(15)]
        [Required]
        public string Prezime { get; set; }
        
        [JsonIgnore]
        public Vozilo Vozilo { get; set; }
        [JsonIgnore]                      
        public Instruktor Instruktor { get; set; }
    }
}