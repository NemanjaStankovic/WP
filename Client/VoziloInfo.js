export class VoziloInfo{
    constructor(id, marka, model, godProizvodnje, imeVlasnika, btelefona, tablice, cena){
        this.ID=id;
        this.Marka=marka;
        this.Model=model;
        this.GodinaProizvodnje=godProizvodnje;
        this.ImeVlasnika=imeVlasnika;
        this.BrojTelefona=btelefona;
        this.Tablice=tablice;
        this.Cena=cena;
        this.miniKontejner=null;
    }
    crtaj(host){
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className="auto";
        this.miniKontejner.innerHTML=this.Marka+" " +this.Model +", "+this.GodinaProizvodnje+"<br />"+this.Tablice+" Cena: "+this.Cena+"$"+ "<br />"+this.ImeVlasnika+"-"+this.BrojTelefona;
        host.appendChild( this.miniKontejner);
        var btnBrisanje = document.createElement("button");
        btnBrisanje.innerHTML="Izbrisi";
        btnBrisanje.className="izbrisi";
        this.miniKontejner.appendChild(btnBrisanje);
        btnBrisanje.onclick=(ev)=>this.obrisiVozilo();

        var btnPromeni = document.createElement("button");
        btnPromeni.innerHTML="Promeni";
        btnPromeni.className="promeni";
        this.miniKontejner.appendChild(btnPromeni);
        btnPromeni.onclick=(ev)=>this.promeniVozilo();
    }
    obrisiVozilo(){
        fetch("https://localhost:5001/Vozilo/IzbrisiVozilo/"+this.ID,
        {
            method:"DELETE"
        }).then(s=>{
            if(s.status==200){
               var roditelj=this.miniKontejner.parentNode;
               roditelj.removeChild(this.miniKontejner);
            }
            else{
                if(s.status==208){
                    alert("Vozilo ne postoji ili je izbrisano!");
                    return;
                }
            }
        })
    }
    promeniVozilo(){
        var formaTablica=this.miniKontejner.parentNode.parentNode;
        formaTablica.querySelector(".tablica").value=this.Tablice;
        formaTablica.querySelector(".Cena").value=this.Cena;
        var promena=formaTablica.querySelectorAll(".vidljivo");
        promena.forEach(element => {
            element.className="nevidljivo";
            
        });

        var obrisiDugme=formaTablica.querySelectorAll(".zaPromenuNaFormi");
        obrisiDugme.forEach(el=>{
            if(el!=null){
                (el.parentNode).removeChild(el);
            } 
        })
        obrisiDugme=document.createElement("button");
        obrisiDugme.innerHTML="Promeni";
        obrisiDugme.className="zaPromenuNaFormi"
        var otkaziDugme=document.createElement("button");
        otkaziDugme.innerHTML="Otkazi";
        otkaziDugme.className="zaPromenuNaFormi"
        obrisiDugme.onclick=(ev)=>this.promeniCenu();
        otkaziDugme.onclick=(ev)=>this.promeni();
        var redZaDugmice=formaTablica.querySelector(".Dugmici");
        redZaDugmice.appendChild(obrisiDugme);
        redZaDugmice.appendChild(otkaziDugme);
    }
    promeniCenu(){
        var numbers = /^[0-9]*$/;
        var formaTablica=this.miniKontejner.parentNode.parentNode;
        var novaCena=formaTablica.querySelector(".Cena").value;
        console.log("Nova cena: "+novaCena);
        if(novaCena=="" || !novaCena.match(numbers) || novaCena>200000 || novaCena<100)
            {
                alert("Unesite novu cenu!");
                return;
            }
            else{
            fetch("https://localhost:5001/Vozilo/PromeniCenu/"+novaCena+"/"+this.ID,
            {
                method:"PUT"
            }).then(s=>{
                if(s.status==200){
                    this.Cena=novaCena;
                    alert("Uspesno promenjen podatak!");

                }
                else{
                    if(s.status==206){
                        alert("Unesite cenu!");
                        return;
                    }
                    if(s.status==207){
                        alert("Vozilo je obrisano");
                        return;
                    }
                }
            })

        }
        this.Cena=novaCena;
        this.promeni();
        
    } 
    promeni()
    {
        var pom=this.miniKontejner;
        var roditelj=this.miniKontejner.parentNode;
        this.crtaj(roditelj);
        pom.replaceWith(this.miniKontejner);
        var roditelj=this.miniKontejner.parentNode.parentNode;
        var dugme=roditelj.querySelectorAll(".zaPromenuNaFormi");

        dugme.forEach(element => {
            element.parentNode.removeChild(element); 
        });
        var promena=roditelj.querySelectorAll(".nevidljivo");
        promena.forEach(element => {
            element.className="vidljivo";
            
        });
        var reset=roditelj.querySelectorAll(".Cena,.tablica");
        reset.forEach(element => {
            element.value="";   
        });

        
    }
}