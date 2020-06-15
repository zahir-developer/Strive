The SignalR sample have Web API and UI projects.

1. API have SignalR and method to update the client pages connected via SignalR.

2. UI have Chat.js containing the script to subscribe to the signalR socket. 

Call the checkIn method from SignalR controler to update the UI dashboard page count.

Use below link to call the API and pass the count as you want to update.

https://localhost:44319/api/signalr?count=1