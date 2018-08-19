/// <reference path="../lib/signalr/signalr.js" />
$(function () {
    let baseUrl = "https://localhost:44342/";

    let connectionBuilder = new signalR.HubConnectionBuilder()
        .withUrl(baseUrl + "questions");
    let connection = connectionBuilder.build();

    console.log(connection);
    //connection.start();

    //connection.invoke("PostQuestion", "username", "How to run SignalR in your app?");
    //console.log(connection);
});