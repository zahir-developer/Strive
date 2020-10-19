"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:55689/chatHub",
    {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    }).build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

var connID = document.getElementById("connectionId");

var connectionId;

connection.on("ReceiveConnID", function (connid) {
    connID.innerHTML = "ConnID: " + connid;
    connectionId = connid;
});

var connectionIds = "";

connection.on("UserLoggedIn", function (id) {

    if (connectionIds != "")
        connectionIds += ",";
    connectionIds += id;

    console.log(connectionIds);
});


connection.on("UpdateCarCheckIn", function (count) {
    document.getElementById("checkInCounts").innerHTML = count;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("createGroupChat").addEventListener("click", function (event) {
    var groupName = document.getElementById("createGroupChatName").value;
    var userName = document.getElementById("userName").value;

    connection.invoke("JoinGroup", connectionId, userName, groupName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("joinGroup").addEventListener("click", function (event) {
    var groupName = document.getElementById("groupChatName").value;
    var userName = document.getElementById("userName").value;

    connection.invoke("JoinGroup", connectionId, userName, groupName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("groupSend").addEventListener("click", function (event) {
    var userName = document.getElementById("userName").value;
    var groupName = document.getElementById("groupName").value;
    var groupMessage = document.getElementById("groupMessage").value;


    connection.invoke("SendMessageToGroup", groupName, userName, groupMessage ).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("GroupMessageReceive", function (obj) {
    DisplayMessage(obj[0], obj[1]);
});


document.getElementById("sendPrivateMessage").addEventListener("click", function (event) {
    var userName = document.getElementById("userName").value;
    var connectionId = document.getElementById("groupName").value;
    var groupMessage = document.getElementById("groupMessage").value;


    connection.invoke("SendMessageToGroup", groupName, userName, groupMessage).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


connection.on("ReceivePrivateMessage", function (obj) {
    DisplayMessage(obj[0], obj[1]);
});

function DisplayMessage(userName, message) {
    var liElement = document.createElement("LI");
    var boldElement = document.createTextNode(userName + ": " + message);
    liElement.appendChild(boldElement);
    document.getElementById("messagesList").append(liElement);
}
