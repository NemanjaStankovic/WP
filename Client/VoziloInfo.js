export class VoziloInfo{
    constructor(id, marka, model, godProizvodnje, imeVlasnika, btelefona){
        this.ID=id;
        this.Marka=marka;
        this.Model=model;
        this.GodinaProizvodnje=godProizvodnje;
        this.ImeVlasnika=imeVlasnika;
        this.BrojTelefona=btelefona;
        this.miniKontejner=null;
    }
    crtaj(host){
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className="auto";
        this.miniKontejner.innerHTML=this.Marka+" " +this.Model +", "+this.GodinaProizvodnje+ "<br />"+this.ImeVlasnika+"-"+this.BrojTelefona;
        host.appendChild( this.miniKontejner);
        var btnBrisanje = document.createElement("button");
        btnBrisanje.innerHTML="Izbrisi";
        btnBrisanje.className="izbrisi";
        this.miniKontejner.appendChild(btnBrisanje);
        btnBrisanje.onclick=(ev)=>this.obrisiVozilo();
    }
    obrisiVozilo(){
        fetch("https://localhost:5001/Vozilo/IzbrisiVozilo/"+this.ID,
        {
            method:"DELETE"
        }).then(s=>{
            if(s.ok){
               var roditelj=this.miniKontejner.parentNode;
               roditelj.removeChild(this.miniKontejner);
            }
        })


    }
}