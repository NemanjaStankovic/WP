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
        [Range(0,999999999)]
        public int Telefon { get; set; } 
        public string Adresa { get; set; }

       // [JsonIgnore] 
        public List<Spoj> Veza{ get; set; }
    }
}