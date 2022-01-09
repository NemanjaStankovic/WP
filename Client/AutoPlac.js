import { VoziloInfo } from "./VoziloInfo.js";

export class AutoPlac{
    constructor(listaTipovaKaroserije)
    {
        this.listaTipovaKaroserije=listaTipovaKaroserije;
        this.konteiner=null;
    }
    crtaj(host){
        this.konteiner=document.createElement("div");   //cela forma
        this.konteiner.className="GlavniKonteiner";
        host.appendChild(this.konteiner);

        let kontForma=document.createElement("div");     //za formu
        kontForma.className="Forma";
        this.konteiner.appendChild(kontForma);

        this.crtajFormu(kontForma);         //poziv fje za crtanje forme
    }
    crtajRed(host)
    {
        let red=document.createElement("div");
        red.className="red";
        host.appendChild(red);
        return red;

    }
    crtajFormu(host){
        let red=this.crtajRed(host);
        let l=document.createElement("label");
        l.innerHTML="Tip karoserije: ";
        red.appendChild(l);                    //labela se lepi za div za formu (12)

        let se=document.createElement("select");  //kreira select element i lepi na div za formu
        red.appendChild(se);

        let op;
        this.listaTipovaKaroserije.forEach(tipKaroserije=>{ //objekat ima atribut listaTipovaKaroserije
            op=document.createElement("option");           //kreiraju se opcije
            op.innerHTML=tipKaroserije.naziv;
            op.value=tipKaroserije.id;
            se.appendChild(op);              //opcije se lepe za select element
        })

        let btnNadji=document.createElement("button");
        btnNadji.innerHTML="Pretrazi";
        btnNadji.onclick=(ev)=>this.pretraziPoOblikuKaroserije();
        red.appendChild(btnNadji);

        let prikazVozila=document.createElement("div");
        prikazVozila.className="PrikazVozila";
        this.konteiner.appendChild(prikazVozila);
        var info=["Marka: ", "Model: ","Registarska tablica: ","Cena: ","Godina proizvodnje: ","Kilometraza: ","Zapremina motora: ", "Snaga motora: ", "Naziv placa: ", "brLK: "];
        var pom=["Registarska tablica","Cena","Godina proizvodnje","Kilometraza","Zapremina motora","Snaga motora"];
        info.forEach(p=>{
            red=this.crtajRed(host);
            l=document.createElement("label");
            l.innerHTML=p;
            red.appendChild(l);
            var tb=document.createElement("input");
            tb.className=p.substr(0, p.indexOf(':'));
            pom.forEach(p=>{
                if(p==tb.className){
                    tb.type=Number;
                }
            })
            red.appendChild(tb);
            //console.log("ime klase je "+tb.className);

        })
        red=this.crtajRed(host);
        let btnDodaj=document.createElement("button");
        btnDodaj.innerHTML="Dodaj";
        red.appendChild(btnDodaj);
        var jaje=document.querySelector(".Marka").value;
        console.log(jaje);
        btnDodaj.onclick=(ev)=>this.dodajVozilo(jaje);

    }
    pretraziPoOblikuKaroserije(){
        let optionEl = this.konteiner.querySelector("select");
        var TipKaID=optionEl.options[optionEl.selectedIndex].value;
        //console.log(prikazVozila);
        fetch("https://localhost:5001/Vozilo/PrikazPoObKaroserije/"+TipKaID,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){
                var zaVozila=this.obrisiPrethodniSadrzaj();
                s.json().then(data=>{
                    data.forEach(v=>{// Marka=p.Marka, Model=p.Model, GodinaProizvodnje=p.GodinaProizvodnje, ImeVlasnika=p.Vlasnik.Ime, BrojTelefona=p.Vlasnik.Telefon,
                        let vozilo=new VoziloInfo(v.marka, v.model, v.godinaProizvodnje, v.imeVlasnika, v.brojTelefona);
                        vozilo.crtaj(zaVozila);
                    })
                })
            }
        })

    }
    obrisiPrethodniSadrzaj()
    {
        var deoZaVozila=document.querySelector(".PrikazVozila");
        var roditelj=deoZaVozila.parentNode;
        roditelj.removeChild(deoZaVozila);

        let prikazVozila=document.createElement("div");
        prikazVozila.className="PrikazVozila";
        roditelj.appendChild(prikazVozila);
        return prikazVozila;
    }
    dodajVozilo(marka)//,model,tablica,cena,godina_proiz,kilometraza,zap_motora,snaga_motora,karoserija,plac_naziv,vlasnik_brLK)
    {
        if(marka==""){
            alert("Marka automobila je obavezno polje!");
            return;
        }
        if(tablica=""){
            alert("Unesite broj registarskih tablica!");
            return;
        }
    }
}