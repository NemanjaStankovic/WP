using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Vozilo")]
    public class Vozilo
    {
        [Key]
        public int ID { get; set; }

        [RegularExpression("\\w+")] //slova samo
        [Required]
        [MaxLength(15)]
        public string Marka { get; set; }

        [MaxLength(15)]
        public string Model { get; set; }

        [Required]
        [MaxLength(8)]
        public string RegistarskaTablica { get; set; }

        [RegularExpression("\\w+")]
        [Required]
        public string VrstaVozila { get; set; } //kamion,auto,motocikl

        [RegularExpression("\\d+")] //samo brojevi
        [Range(1980,2021)]
        public int GodinaProizvodnje { get; set; }

        [RegularExpression("\\d+")]
        [Range(50,2000)]
        public int ZapreminaMotora { get; set; }

        [RegularExpression("\\d+")]
        [Range(20,180)]
        public int SnagaMotora { get; set; }
        public List<InstruktorVozilo> ListaInstruktora { get; set;}

        public List<Polaznik> ListaPolaznika { get; set; }
    }
}