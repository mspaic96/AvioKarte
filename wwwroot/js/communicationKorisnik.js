"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/aviohub").build();

//Disable send button until connection is established

document.getElementById("reserveButton").disabled = true;


/*document.getElementById("reserveButton").addEventListener("click", function (event) {
    var user = "milutin"
    var message ="proba signalr";
    console.log("kliknuto dugme na korisniku");
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});*/



connection.start().then(function () {
    document.getElementById("reserveButton").disabled = false;
    console.log("korisnik konektovan");
}).catch(function (err) {
    return console.error(err.toString());
});

