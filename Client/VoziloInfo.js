export class VoziloInfo{
    constructor(marka, model, godProizvodnje, imeVlasnika, btelefona){
        this.Marka=marka;
        this.Model=model;
        this.GodinaProizvodnje=godProizvodnje;
        this.ImeVlasnika=imeVlasnika;
        this.BrojTelefona=btelefona;
    }
    crtaj(host){
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className="auto";
        this.miniKontejner.innerHTML=this.Marka+" " +this.Model +", "+this.GodinaProizvodnje+ "<br />"+this.ImeVlasnika+"-"+this.BrojTelefona;
        host.appendChild( this.miniKontejner);

    }
}