window.onload = () => {
    console.log("betöltődött")
}

var faktoriális = function (n) {
    let er = 1;
    for (let i = 2; i <= n; i++) {
        er = er * i;
    }
    return er;
}


function sajatfun(){
    for (var i = 0; i < 10; i++) {
        var newSor = document.createElement("div");
        newSor.classList.add("sor");
        document.getElementById("pascal").appendChild(newSor);

        for (var j = 0; j <= i; j++) {

            var newElem = document.createElement("div");
            newElem.classList.add("elem");
            newElem.innerHTML = (faktoriális(i) / (faktoriális(j) * faktoriális(i - j)));
            newSor.appendChild(newElem);
        }
    }
}
sajatfun();
