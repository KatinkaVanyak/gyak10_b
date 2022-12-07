fetch('/kerdesek/4')
    .then(response => response.json())
    .then(data => kérdésMegjelenites(data)
    );

function kérdésMegjelenítés(kérdés) {
    if (!kérdés) return;

    console.log(kérdés);
    document.getElementById("kérdés_szöveg").innerText = kérdés.questionText
    document.getElementById("válasz1").innerText = kérdés.answer1
    document.getElementById("válasz2").innerText = kérdés.answer2
    document.getElementById("válasz3").innerText = kérdés.answer3
    document.getElementById("kép").src = "https://szoft1.comeback.hu/hajo/" + kérdés.image;

    jóVálasz = kérdés.correctAnswer;
    document.getElementById("válasz1").classList.remove("jó", "rossz");
    document.getElementById("válasz2").classList.remove("jó", "rossz");
    document.getElementById("válasz3").classList.remove("jó", "rossz");
}

function kérdésBetöltés(id) {
    fetch(`/kerdesek/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                kékérdésMegjelenítés(response.json())
            }
        })
        //.then(data => kérdésMegjelenítés(data));
}    


var jóVálasz;
var questionId = 4;
