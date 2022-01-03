using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
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

        [Route("DodajPolaznika/{jmbg}/{brLicneKarte}/{PT}/{PV}/{ime}/{prezime}/{tablice}/{imeI}/{prezimeI}")]  //isto kao i arg fje mora da se zove (case senzitive)
        [HttpPost]
        public async Task<ActionResult> DodajPolaznika(long jmbg, int brLicneKarte, bool PT, bool PV, string ime, string prezime,string tablice,string imeI, string prezimeI) //da moze da vrati json, ok ili bad request
        {
            if(jmbg>9999999999999 || jmbg<100000000000)
            {
                return BadRequest("JMBG mora da sadrzi 13 cifara!");
            }
            if(brLicneKarte>999999999)
            {
                return BadRequest("Pogresan broj licne karte!");
            }
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>15)
            {
                return BadRequest("Ime je predugo ili nije uneseno!");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>15)
            {
                return BadRequest("Prezime je predugo ili nije uneseno!");
            }
            try
            {
                var auto=await Context.Vozila.Where(p=>p.RegistarskaTablica==tablice).FirstOrDefaultAsync();
                if(auto==null)
                {
                    return BadRequest($"Vozilo sa registarskom tablicom {tablice} ne postoji u bazi podataka!");
                }
                var instruktor=await Context.Instruktori.Where(p=>p.Ime==imeI && p.Prezime==prezimeI).FirstOrDefaultAsync();
                if(instruktor==null)
                {
                    return BadRequest($"Instruktor {imeI} {prezimeI} ne postoji u bazi podataka!");
                }
                Polaznik polaznik=new Polaznik
                {
                    JMBG=jmbg,
                    BrLicneKarte=brLicneKarte,
                    PolozioTest=PT,
                    PolozioVoznju=PV,
                    Ime=ime,
                    Prezime=prezime,
                };
                Spoj iv = new Spoj
                {
                    Vozilo=auto,
                    Instruktor=instruktor,
                    Polaznici=new List<Polaznik>()

                };
                iv.Polaznici.Add(polaznik);
                Context.Polaznici.Add(polaznik);
                Context.Veza.Add(iv);
                await Context.SaveChangesAsync();
                return Ok($"Korisnik {polaznik.Ime} {polaznik.Prezime} je uspesno dodat!");//upise u bazu a iz bazu prepisuje ID
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString()); //poruku iz baze vraca

            }
        }
        [Route("Prikazi polaznika/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziPolaznika(long jmbg)
        {
            var pol=await Context.Polaznici
                          .Include(p=>p.Veza).Where(p=>p.JMBG==jmbg).ToListAsync();      // ? ? ? ? ?
            return Ok(pol);
        }

        [Route("Izbrisati polaznika/{jmbg}")]
        [HttpDelete]

        public async Task<ActionResult> IzbrisiPolaznika(long jmbg)
        {
            if(jmbg>9999999999999)
            {
                return BadRequest("Nevalidan JMBG!");
            }
            try{
                var pol=await Context.Polaznici.Where(p=>p.JMBG==jmbg).FirstOrDefaultAsync();
                var spoj=await Context.Veza.Where(p=>p.Polaznici.Contains(pol)).FirstOrDefaultAsync();
                //var veza=await Context.Veza.Where(p=>p.Polaznici==pol).FirstOrDefaultAsync();
                /*InstruktorVozilo iv = new InstruktorVozilo
                {
                    ID=veza.ID,
                    Vozilo=auto,
                    Instruktor=instruktor
                };*/
                string pomIme=pol.Ime;
                string pomPrez=pol.Prezime;
               // Context.Veza.Remove(veza);
                Context.Polaznici.Remove(pol);
                Context.Veza.Remove(spoj);
                
                await Context.SaveChangesAsync();
                return Ok($"Polaznik sa JMBG-om {jmbg} {pomIme} {pomPrez} je uspesno obrisan!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        /*
        [Route("IzbrisatiStudenta/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            try
            {
                var student = await Context.Studenti.FindAsync(id);
                int indeks = student.Indeks;
                Context.Studenti.Remove(student);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan student sa Indeksom: {indeks}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        [Route("PromenitiStudenta/{indeks}/{ime}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> Promeni(int indeks, string ime, string prezime)
        {
            if (indeks < 10000 || indeks > 20000)
            {
                return BadRequest("Pogrešan indeks!");
            }

            try
            {
                var student = Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefault();

                if (student != null)
                {
                    student.Ime = ime;
                    student.Prezime = prezime;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspešno promenjen student! ID: {student.ID}");
                }
                else
                {
                    return BadRequest("Student nije pronađen!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenaFromBody")]
        [HttpPut]
        public async Task<ActionResult> PromeniBody([FromBody] Student student)
        {
            if (student.ID <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            // ... Ostale provere (Indeks, Ime, Prezime)

            try
            {
                //var studentZaPromenu = await Context.Studenti.FindAsync(student.ID);
                //studentZaPromenu.Indeks = student.Indeks;
                //studentZaPromenu.Ime = student.Ime;
                //studentZaPromenu.Prezime = student.Prezime;

                Context.Studenti.Update(student);

                await Context.SaveChangesAsync();
                return Ok($"Student sa ID: {student.ID} je uspešno izmenjen!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        }*/
    }
}