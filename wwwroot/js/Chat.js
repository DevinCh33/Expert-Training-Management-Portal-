"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `System says ${message}`;
});
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = "";
    var pElements = document.getElementsByTagName("p");
    for (var i = 0; i < pElements.length; i++) {
        message += pElements[i].innerHTML + "\n";
    }
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});