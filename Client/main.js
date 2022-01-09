import {TipKaroserije} from "./TipKaroserije.js";
import { AutoPlac} from "./AutoPlac.js";

var listaTipovaK=[];
fetch("https://localhost:5001/TipKaroserije/PreuzmiTipKaroserije")
.then(p=>{
    p.json().then(tipoviKaroserija=>{
        tipoviKaroserija.forEach(tipK => {
            var tK=new TipKaroserije(tipK.id, tipK.naziv, tipK.opis);
            listaTipovaK.push(tK); 
        });
        var autoPlac=new AutoPlac(listaTipovaK);
        autoPlac.crtaj(document.body);
    })
})
    console.log(listaTipovaK);
