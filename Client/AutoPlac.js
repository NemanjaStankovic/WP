import { VoziloInfo } from "./VoziloInfo.js";

export class AutoPlac{
    constructor(listaTipovaKaroserije,id, ime, brojTelefona, adresa, kapacitet)
    {
        this.id=id;
        this.naziv=ime;
        this.brojTelefona=brojTelefona;
        this.adresa=adresa;
        this.kapacitet=kapacitet;
        this.listaTipovaKaroserije=listaTipovaKaroserije;
        this.konteiner=null;
    }
    crtaj(host){
        this.konteiner=document.createElement("div");   //cela forma
        this.konteiner.className="GlavnijiKonteiner";
        host.appendChild(this.konteiner);
        console.log(this.konteiner);
        var glavniKonteiner=document.createElement("div");   //cela forma
        glavniKonteiner.className="GlavniKonteiner";
        this.konteiner.appendChild(glavniKonteiner);

        /*let celaForma=document.createElement("div");
        celaForma.className="CelaForma";
        this.konteiner.appendChild(celaForma);*/


        let kontForma=document.createElement("div");     //za formu
        kontForma.className="Forma";
        glavniKonteiner.appendChild(kontForma);

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
        var parent=this.konteiner.childNodes[0];
        parent.appendChild(prikazVozila);

        //var info=["Marka: ", "Model: ","Registarska tablica: ","Cena: ","Godina proizvodnje: ","Kilometraza: ","Zapremina motora: ", "Snaga motora: ", "Naziv placa: ", "brLK: "];
        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Marka: ";
        red.appendChild(l);
        var marka=document.createElement("input");
        red.appendChild(marka);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Model: ";
        red.appendChild(l);
        var model=document.createElement("input");
        red.appendChild(model);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Registarska tablica: ";
        red.appendChild(l);
        var reg_tablica=document.createElement("input");
        red.appendChild(reg_tablica);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Cena: ";
        red.appendChild(l);
        var cena=document.createElement("input");
        red.appendChild(cena);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Godina proizvodnje: ";
        red.appendChild(l);
        var godina_proiz=document.createElement("input");
        red.appendChild(godina_proiz);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Kilometraza: ";
        red.appendChild(l);
        var kilometraza=document.createElement("input");
        red.appendChild(kilometraza);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Zapremina motora: ";
        red.appendChild(l);
        var zap_motora=document.createElement("input");
        red.appendChild(zap_motora);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Snaga motora: ";
        red.appendChild(l);
        var snaga_motora=document.createElement("input");
        red.appendChild(snaga_motora);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Naziv placa: ";
        red.appendChild(l);
        var naziv_placa=document.createElement("input");
        red.appendChild(naziv_placa);

        red=this.crtajRed(host);
        l=document.createElement("label");
        l.innerHTML="Broj licne karte: ";
        red.appendChild(l);
        var vlasnik_brLK=document.createElement("input");
        red.appendChild(vlasnik_brLK);

        red=this.crtajRed(host);
        let btnDodaj=document.createElement("button");
        btnDodaj.innerHTML="Dodaj";
        red.appendChild(btnDodaj);
        btnDodaj.onclick=(ev)=>this.dodajVozilo(marka.value, model.value, reg_tablica.value, cena.value, godina_proiz.value, kilometraza.value, zap_motora.value, snaga_motora.value, naziv_placa.value, vlasnik_brLK.value);

        var opis=document.createElement("div");
        opis.innerHTML="";
        opis.className="Opis";
        this.konteiner.appendChild(opis);
    }
    pretraziPoOblikuKaroserije(){
        let optionEl = this.konteiner.querySelector("select");
        var TipKaID=optionEl.options[optionEl.selectedIndex].value;
        var info;
        this.listaTipovaKaroserije.forEach(el=>{
            if(el.id==TipKaID){
                info=el.naziv+" - "+el.opis;
            }
        })
        fetch("https://localhost:5001/Vozilo/PrikazPoObKaroserije/"+TipKaID+"/"+this.naziv,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){
                var zaVozila=this.obrisiPrethodniSadrzaj();
                s.json().then(data=>{
                    data.forEach(v=>{// Marka=p.Marka, Model=p.Model, GodinaProizvodnje=p.GodinaProizvodnje, ImeVlasnika=p.Vlasnik.Ime, BrojTelefona=p.Vlasnik.Telefon,
                        let vozilo=new VoziloInfo(v.id, v.marka, v.model, v.godinaProizvodnje, v.imeVlasnika, v.brojTelefona);
                        vozilo.crtaj(this.konteiner.querySelector(".PrikazVozila"));
                    })
                })
            }
            var opis=this.konteiner.querySelector(".Opis");
            opis.innerHTML=info;
        })

    }
    obrisiPrethodniSadrzaj()
    {
        var deoZaVozila=this.konteiner.querySelector(".PrikazVozila");
        var roditelj=deoZaVozila.parentNode;
        roditelj.removeChild(deoZaVozila);

        let prikazVozila=document.createElement("div");
        prikazVozila.className="PrikazVozila";
        roditelj.appendChild(prikazVozila);
        return prikazVozila;
    }
    dodajVozilo(marka, model, tablica, cena, godina_proiz, kilometraza, zap_motora, snaga_motora, plac_naziv, vlasnik_brLK)
    {
        var letters = /^[a-zA-Z\s]*$/;
        var numbers = /^[0-9]*$/;
        if(marka==""){
            alert("Marka automobila je obavezno polje!");
            return;
        }
        else{//
            if(marka.length>15 || !marka.match(letters)){
                alert("Marka ne sme da bude duza od 15 karaktera niti da sadrzi cifre ili specijalne znake!");
                return;
            }
        }
        if(model==""){
            alert("Unesite model automobila");
            return
        } 
        else{
            if(model.length>15){
                alert("Naziv modela ne sme da bude duzi od 15 karaktera")
            }
        }
        if(tablica==""){
            alert("Unesite broj registarskih tablica!");
            return;
        }
        else{
            if(tablica.length>8)
            {
                alert("Registarska oznaka vozila ne sme da bude duza od 8 karaktera!");
                return;
            }
        }
        if(cena=="")
        {
            alert("Unesite cenu!");
            return;
        }
        else{
            if(cena>200000 || cena<100 || !cena.match(numbers)){
                alert("Cena mora da bude u opsegu 100-200000! i sme da sadrzi samo brojeve");
                return;
            }
        }
        if(godina_proiz=="")
        {
            alert("Unesite godinu proizvodnje!");
            return;
        }
        else{
            if(godina_proiz<1960 || godina_proiz>2021 || !godina_proiz.match(numbers)){
            alert("Vozilo ne moze da bude proizvedeno pre 1960-te godine! Godina je cetvorocifreni broj bez specijalnih znakova");
            return;
            }
        }
        if(!kilometraza.match(numbers) || kilometraza=="" )
        {
            alert("Unesite broj predjenih kilometara!");
            return;
        }
        if(!zap_motora.match(numbers) || zap_motora=="")
        {
            alert("Zapremina motora sme da sadrzi samo cifre!");
            return;
        }
        else{
            if(zap_motora<20 || zap_motora>8000)
            {
                alert("Zapremina motora mora da bude u opsegu 20-8000ccm!");
                return;
            }
        }
        if(!snaga_motora.match(numbers) || snaga_motora=="")
        {
            alert("Snaga sme da sadrzi samo cifre!");
            return;
        }
        else{
            if(snaga_motora<20 || snaga_motora>1000)
            {
                alert("Snaga motora mora da bude u opsegu 20-1000ks!");
                return;
            }
        }
        //naziv placa ? ? ? ?
        if(!vlasnik_brLK.match(numbers) || vlasnik_brLK=="")
        {
            alert("BRLK vlasnika sme da sadrzi samo cifre!");
            return;
        }
        else{
            if(vlasnik_brLK.length>9){
            alert("Broj LK je predug!");
            return;
            }
        }
        let optionEl = this.konteiner.querySelector("select");
        var TipKaStr=optionEl.options[optionEl.selectedIndex].innerHTML;
        fetch("https://localhost:5001/Vozilo/DodajVozilo/"+marka+"/"+model+"/"+tablica+"/"+cena+"/"+godina_proiz+"/"+kilometraza+"/"
        +zap_motora+"/"+snaga_motora+"/"+TipKaStr+"/"+plac_naziv+"/"+vlasnik_brLK,
        {
            method:"POST"
        }).then(s=>{
            if(s.ok){
                var zaVozila=this.obrisiPrethodniSadrzaj();
                s.json().then(data=>{
                    data.forEach(voz=>{
                        const novoVozilo= new VoziloInfo(voz.id, voz.marka,voz.model,voz.godinaProizvodnje,voz.imeVlasnika,voz.brojTelefona);
                        novoVozilo.crtaj(this.konteiner.querySelector(".PrikazVozila"));
                    })

                })
            }
            else{
                console.log(s.Response);
            }
        })


    }
}