"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/aviohub").build();


connection.on("ReceiveReservation", function (mestopolaska, mestodolaska,datumleta,statusleta,brojrezervacija,status,username,idrez) {
   
    console.log("agent radiiiii");
    var div = document.createElement("div");
    div.className += "d-flex justify-content-space-between";
    var input = '<input type= "submit" value="Odobri" formaction="/Rezervacije/AgentRezervacije?id='+ idrez +'" class="btn btn-success" />';
    div.innerHTML = input;
    console.log(div);
    var form = document.createElement("form");
    form.method = "post";
    form.appendChild(div);
    var td = document.createElement("td");
    td.appendChild(form);

    var row = document.createElement("tr");
    var html= '<td>' + mestopolaska +'</td><td>' + mestodolaska +'</td><td>'+ datumleta +'</td><td>'+ 
    statusleta+'</td><td>' + brojrezervacija +'</td><td>'+ status +'</td><td>'+ username +'</td><td>';
    row.innerHTML = html;
    row.appendChild(td);
    document.getElementById("tableBody").appendChild(row);
    
});
connection.start().then(function () {
    console.log("agent pocela konekcija");
}).catch(function (err) {
    return console.error(err.toString());
});