"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/aviohub").build();

connection.start().then(function () {
  
    console.log("korisnik konektovan");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ConfirmReservation", function (idrez,status) {
   
   var td=document.getElementById(idrez).innerHTML=status;

   
    
});