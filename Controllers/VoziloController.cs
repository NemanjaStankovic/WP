using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace WEBPROJEKAT.Controllers // ?????????????
{
    [ApiController]
    [Route("[controller]")]
    public class VoziloController : ControllerBase
    {
        public AutoSkolaContext Context { get; set; } //da se koristi context u kontroleru

        public VoziloController(AutoSkolaContext context)
        {
            Context=context;
        }

        [Route("Prikaz")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            return Ok("Dobro"); // I M P L E M E N T I R A J
            
        }

        [Route("DodajVozilo")]
        [HttpPost]
        public async Task<ActionResult> DodajStudenta([FromBody] Vozilo vozilo) //da moze da vrati json, ok ili bad request
        {
            if(vozilo.GodinaProizvodnje<1980 || vozilo.GodinaProizvodnje>2021)
            {
                return BadRequest("Godina van preporucenog opsega (1980-2021)!");
            }
            if(string.IsNullOrWhiteSpace(vozilo.Marka) || vozilo.Marka.Length>15)
            {
                return BadRequest("Pogresna ili nevalidna marka automobila!");
            }
            if(vozilo.Model.Length>15)
            {
                return BadRequest("Predug naziv modela!");
            }
            if(string.IsNullOrWhiteSpace(vozilo.VrstaVozila))
            {
                return BadRequest("Unesite vrstu vozila");
            }
            if(vozilo.SnagaMotora>180 || vozilo.SnagaMotora<20)
            {
                return BadRequest("Snaga motora van opsega!Preporucen opseg 20-180ks");
            }
            if(vozilo.ZapreminaMotora>2000 || vozilo.ZapreminaMotora<20)
            {
                return BadRequest("Zapremina motora van preporucenog opsega!");
            }
            try
            {
                Context.Vozila.Add(vozilo);//ako dugo traje metoda moze i AddAsync
                await Context.SaveChangesAsync();//moze da potraje upis u bazu; odvijace se u pozadinskoj niti
                //alternativno Context.SaveChangesAsync(); (bez await) ce da prebaci fju na novu nit a stara ce 
                //da nastavi da izvrsava glavni kod zbog cega ce se sledeca linija izvrsiti bez obzira na rezultat operacije
                //vraca broj uspesno dodatih entiteta
                return Ok($"Vozilo je dodato! ID je :{vozilo.ID}");//upise u bazu a iz bazu prepisuje ID
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
            if(vozilo.GodinaProizvodnje<1980 || vozilo.GodinaProizvodnje>2021)
            {
                return BadRequest("Godina van preporucenog opsega (1980-2021)!");
            }
            if(string.IsNullOrWhiteSpace(vozilo.Marka) || vozilo.Marka.Length>15)
            {
                return BadRequest("Pogresna ili nevalidna marka automobila!");
            }
            if(vozilo.Model.Length>15)
            {
                return BadRequest("Predug naziv modela!");
            }
            if(string.IsNullOrWhiteSpace(vozilo.VrstaVozila))
            {
                return BadRequest("Unesite vrstu vozila");
            }
            if(vozilo.SnagaMotora>180 || vozilo.SnagaMotora<20)
            {
                return BadRequest("Snaga motora van opsega!Preporucen opseg 20-180ks");
            }
            if(vozilo.ZapreminaMotora>2000 || vozilo.ZapreminaMotora<20)
            {
                return BadRequest("Zapremina motora van preporucenog opsega!");
            }
            try
            {
                var voziloZaPromenu=await Context.Vozila.FindAsync(vozilo.ID);
                voziloZaPromenu.Marka=vozilo.Marka;
                voziloZaPromenu.Model=vozilo.Model;
                voziloZaPromenu.VrstaVozila=vozilo.VrstaVozila;
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