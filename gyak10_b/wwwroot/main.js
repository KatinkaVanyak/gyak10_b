fetch('/kerdesek/4')
    .then(response => response.json())
    .then(data => kérdésMegjelenites(data)
    );

function kérdésMegjelenites(kérdés) {
    console.log(kérdés);
    document.getElementById("kérdés_szöveg").innerHTML = kérdés.questionText
    document.getElementById("válasz1").innerText = kérdés.answer1
    document.getElementById("válasz2").innerText = kérdés.answer2
    document.getElementById("válasz3").innerText = kérdés.answer3

}
function kérdésBetölt(id) {
    fetch(`/kerdesek/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                return response.json()
            }
        })
        .then(data => kérdésMegjelenites(data));

}
