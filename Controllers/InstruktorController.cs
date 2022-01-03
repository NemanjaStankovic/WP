using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace WEBPROJEKAT.Controllers
{
    public class InstruktorController : ControllerBase
    {
        public AutoSkolaContext Context{ get; set; }
        public InstruktorController(AutoSkolaContext context)
        {
            Context=context;

        }

        [Route("DodajInstruktora")]
        [HttpPost]
        public async Task<ActionResult> DodajInstruktora([FromBody] Instruktor instruktor)
        {
            if(string.IsNullOrWhiteSpace(instruktor.Ime) || instruktor.Ime.Length>15)
            {
                return BadRequest("Ime neispravno je!");
            }
            if(string.IsNullOrEmpty(instruktor.Prezime) || instruktor.Ime.Length>15)
            {
                return BadRequest("Prezime neispravno je!");
            }
            if(instruktor.GodinaRodjena>2021 || instruktor.GodinaRodjena<1921)
            {
                return BadRequest("Neispravna godina rodjenja!");
            }
            if(instruktor.Telefon>999999999)
            {
                return BadRequest("Broj telefona predug");
            }
            Context.Instruktori.Add(instruktor);
            await Context.SaveChangesAsync();
            return Ok($"Instruktor {instruktor.Ime} {instruktor.Prezime} uspesno dodat!");

        }

        [Route("Prikazi instruktora/{ime}/{prezime}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziInstruktora(string ime, string prezime)
        {
            /*var auta=Context.Vozila
                     .Include(p => p.ListaPolaznika.Where(p=>p.Vozilo.RegistarskaTablica==br_tablice))
                     .Include(p=>p.ListaInstruktora);
            var auto=await auta./Where(p=>p.RegistarskaTablica==br_tablice)./ToListAsync();
            return Ok(auto);*/
            
            var ins=Context.Instruktori
                    .Include(p=>p.Veza).Where(p=>p.Ime==ime && p.Prezime==prezime);
            var insi=await ins.ToListAsync();
            return Ok(insi);

        }
    }
}