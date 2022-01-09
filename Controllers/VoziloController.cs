using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace WEBPROJEKAT.Controllers // ?????????????
{
    [ApiController]
    [Route("[controller]")]
    public class VoziloController : ControllerBase
    {
        public AutoPlacContext Context { get; set; } //da se koristi context u kontroleru

        public VoziloController(AutoPlacContext context)
        {
            Context=context;
        }
        [Route("PrikazPoObKaroserije/{idKaroserije}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPoOblikuKaroserije(int idKaroserije)
        {
            var auta=Context.Vozila
                            .Include(p=>p.Vlasnik).Where(p=>p.Karoserija.ID==idKaroserije);
            var auto=await auta.ToListAsync();
            return Ok
            (
                auto.Select(p=>
                new
                {
                    Marka=p.Marka,
                    Model=p.Model,
                    GodinaProizvodnje=p.GodinaProizvodnje,
                    ImeVlasnika=p.Vlasnik.Ime,
                    BrojTelefona=p.Vlasnik.Telefon,

                })); 
        }

        [Route("Prikaz/{br_tablice}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(string br_tablice)
        {
            var auta=Context.Vozila
                            .Include(p=>p.Vlasnik).Where(p=>p.RegistarskaTablica==br_tablice)
                            .Include(p=>p.Karoserija)
                            .Include(p=>p.NazivPlaca);
            var auto=await auta./*Where(p=>p.RegistarskaTablica==br_tablice).*/ToListAsync();
            return Ok(auto); 
        }

        [Route("DodajVozilo/{Marka}/{Model}/{Registarska_tablica}/{Cena}/{God_Proizvodnje}/{Kilometraza}/{Zapremina_motora}/{Snaga_motora_u_ks}/{Karoserija}/{Plac_naziv}/{Vlasnik_brLicneKarte}")]
        [HttpPost]
        public async Task<ActionResult> DodajVozilo(string Marka, string Model, string Registarska_tablica, int Cena, int God_Proizvodnje, int Kilometraza, int Zapremina_motora, int Snaga_motora_u_ks, string Karoserija, string Plac_naziv, long Vlasnik_brLicneKarte) //da moze da vrati json, ok ili bad request
        {
            if(God_Proizvodnje<1960 || God_Proizvodnje>2021)
            {
                return BadRequest("Godina van preporucenog opsega (1960-2021)!");
            }
            if(string.IsNullOrWhiteSpace(Marka) || Marka.Length>15)
            {
                return BadRequest("Pogresna ili nevalidna marka automobila!");
            }
            if(Model.Length>15)
            {
                return BadRequest("Predug naziv modela!");
            }
            if(string.IsNullOrWhiteSpace(Karoserija))
            {
                return BadRequest("Unesite tip karoserije!");
            }
            if(Snaga_motora_u_ks>1000 || Snaga_motora_u_ks<20)
            {
                return BadRequest("Snaga motora van opsega! Preporucen opseg 20-1000ks");
            }
            if(Zapremina_motora>8000 || Zapremina_motora<20)
            {
                return BadRequest("Zapremina motora van preporucenog opsega!");
            }
            try
            {
                var vlasnik=await Context.Prodavci.Where(p=>p.BrLicneKarte==Vlasnik_brLicneKarte).FirstOrDefaultAsync();
                if(vlasnik==null)
                {
                    return BadRequest("Proverite da li je broj licne karte ispravan ili prvo unesite vlasnika sa datim brojem!");
                }
                var plac=await Context.Placevi.Where(p=>p.Naziv==Plac_naziv).FirstOrDefaultAsync();
                var karoserija= await Context.Karoserije.Where(p=>p.Naziv==Karoserija).FirstOrDefaultAsync();
                Vozilo novoVozilo=new Vozilo();
                novoVozilo.Marka=Marka;
                novoVozilo.Model=Model;
                novoVozilo.RegistarskaTablica=Registarska_tablica;
                novoVozilo.Cena=Cena;
                novoVozilo.GodinaProizvodnje=God_Proizvodnje;
                novoVozilo.Kilometraza=Kilometraza;
                novoVozilo.ZapreminaMotora=Zapremina_motora;
                novoVozilo.SnagaMotora=Snaga_motora_u_ks;
                novoVozilo.Marka=Marka;
                novoVozilo.Karoserija=karoserija;
                novoVozilo.Vlasnik=vlasnik;
                novoVozilo.NazivPlaca=plac;
                Context.Vozila.Add(novoVozilo);
                await Context.SaveChangesAsync();//moze da potraje upis u bazu; odvijace se u pozadinskoj niti
                //alternativno Context.SaveChangesAsync(); (bez await) ce da prebaci fju na novu nit a stara ce 
                //da nastavi da izvrsava glavni kod zbog cega ce se sledeca linija izvrsiti bez obzira na rezultat operacije
                //vraca broj uspesno dodatih entiteta
                return Ok($"Vozilo je dodato! ID je :{novoVozilo.ID}");//upise u bazu a iz bazu prepisuje ID
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); //poruku iz baze vraca

            }
        }

        [Route("PromeniVozilo")]
        [HttpPut]
        public async Task<ActionResult> Promeni([FromBody] Vozilo vozilo)
        {
            if(vozilo.GodinaProizvodnje<1960 || vozilo.GodinaProizvodnje>2021)
            {
                return BadRequest("Godina van preporucenog opsega (1960-2021)!");
            }
            if(string.IsNullOrWhiteSpace(vozilo.Marka) || vozilo.Marka.Length>15)
            {
                return BadRequest("Pogresna ili nevalidna marka automobila!");
            }
            if(vozilo.Model.Length>15)
            {
                return BadRequest("Predug naziv modela!");
            }
            if(vozilo.SnagaMotora>1000 || vozilo.SnagaMotora<20)
            {
                return BadRequest("Snaga motora van opsega!Preporucen opseg 20-1000ks");
            }
            if(vozilo.ZapreminaMotora>8000 || vozilo.ZapreminaMotora<20)
            {
                return BadRequest("Zapremina motora van preporucenog opsega!");
            }
            try
            {
                var voziloZaPromenu=await Context.Vozila.FindAsync(vozilo.ID);
                voziloZaPromenu.Marka=vozilo.Marka;
                voziloZaPromenu.Model=vozilo.Model;
                voziloZaPromenu.Karoserija=vozilo.Karoserija;
                voziloZaPromenu.Kilometraza=vozilo.Kilometraza;
                voziloZaPromenu.GodinaProizvodnje=vozilo.GodinaProizvodnje;
                voziloZaPromenu.ZapreminaMotora=vozilo.ZapreminaMotora;
                voziloZaPromenu.SnagaMotora=vozilo.SnagaMotora;

                await Context.SaveChangesAsync();
                return Ok($"Promenjeno vozilo sa ID={voziloZaPromenu.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiVozilo/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiVozilo(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var vozilo=await Context.Vozila.FindAsync(id);
                Context.Vozila.Remove(vozilo);
                await Context.SaveChangesAsync();
                return Ok($"Vozilo marke {vozilo.Marka}, model {vozilo.Model} iz {vozilo.GodinaProizvodnje}. je uspesno izbrisano");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }

}