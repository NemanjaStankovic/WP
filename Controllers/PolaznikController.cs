using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace WEBPROJEKAT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolaznikController : ControllerBase
    {
        public AutoSkolaContext Context{ get; set; }

        public PolaznikController(AutoSkolaContext context)
        {
            Context=context;
        }

        [Route("DodajPolaznika/{}/{}/{}/{}/{}/{}/{}")]
        [HttpPost]
        public async Task<ActionResult> DodajPolaznika(long jmbg, int brLK, bool PT, bool PV, string ime, string prez,string tablice,string imeI, string prezimeI) //da moze da vrati json, ok ili bad request
        {
            if(jmbg>9999999999999 || jmbg<100000000000)
            {
                return BadRequest("JMBG mora da sadrzi 13 cifara!");
            }
            if(brLK>999999999)
            {
                return BadRequest("Pogresan broj licne karte!");
            }
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>15)
            {
                return BadRequest("Ime je predugo ili nije uneseno!");
            }
            if(string.IsNullOrWhiteSpace(prez) || prez.Length>15)
            {
                return BadRequest("Prezime je predugo ili nije uneseno!");
            }
            try
            {
                var auto=await Context.Vozila.Where(p=>p.RegistarskaTablica==tablice).FirstOrDefaultAsync();
                var instruktor=await Context.Instruktori.Where(p=>p.Ime==imeI && p.Prezime==prezimeI).FirstOrDefaultAsync();
                Polaznik polaznik=new Polaznik
                {
                    JMBG=jmbg,
                    BrLicneKarte=brLK,
                    PolozioTest=PT,
                    PolozioVoznju=PV,
                    Ime=ime,
                    Prezime=prez,
                    Vozilo=auto,
                    Instruktor=instruktor
                };
                Context.Polaznici.Add(polaznik);//ako dugo traje metoda moze i AddAsync
                await Context.SaveChangesAsync();//moze da potraje upis u bazu; odvijace se u pozadinskoj niti
                //alternativno Context.SaveChangesAsync(); (bez await) ce da prebaci fju na novu nit a stara ce 
                //da nastavi da izvrsava glavni kod zbog cega ce se sledeca linija izvrsiti bez obzira na rezultat operacije
                //vraca broj uspesno dodatih entiteta
                return Ok($"Korisnik {polaznik.Ime} {polaznik.Prezime} je uspesno dodat!");//upise u bazu a iz bazu prepisuje ID
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); //poruku iz baze vraca

            }
        }

    }
}