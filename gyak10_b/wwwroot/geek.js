window.onload = () => {
    console.log("Oldal beöltve");
}

var viccek;

function letöltés = function () {

    fetch('/jokes.json')
        .then(z => z.json())
        .then(data => letöltésBefejeződött(data));


}

function letöltésBefejeződött(d) {
    console.log("Sikeres letöltés")
    console.log(d)
    viccek = d;
    

    for (var i = 0; i < viccek.length; i++) {
        console.log(viccek[i].text);
        let elem = document.createElement("div");
        elem.innerHTML = viccek[i].text;
        bodydiv.appendChild(elem);
    }
}

