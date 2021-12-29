using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class Instruktor
    {
        [Key]
        public int ID { get; set; }

        [RegularExpression("\\w+")]
        [Required]
        [MaxLength(15)]
        public string Ime { get; set; }

        [RegularExpression("\\w+")]
        [Required]
        [MaxLength(15)]
        public string Prezime { get; set; }

        [RegularExpression("\\d+")]
        [Range(1921,2021)]
        [Required]
        public int GodinaRodjena { get; set; }

        [RegularExpression("\\d+")]
        [MaxLength(9)]
        public int Telefon { get; set; } 
        public string Adresa { get; set; }
        
        [JsonIgnore] 
        public List<Polaznik> Polaznici{ get; set; }

        [JsonIgnore] 
        public List<InstruktorVozilo> Vozila{ get; set; }
    }
}