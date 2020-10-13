import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';

@Injectable({
  providedIn: 'root'
})


export class SignalRService {

  connId: string;
  empId = localStorage.getItem('empId');

  constructor(public messengerService: MessengerService) { }

  public data: any;
  private hubConnection: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.api.striveUrl + 'ChatMessageHub',
        {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        }).build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public SubscribeChatEvents = () => {
    this.hubConnection.on('ReceiveCommunicationID', (id) => {
      console.log(id);
      this.connId = id
      this.messengerService.UpdateChatCommunication(id);

      this.hubConnection.invoke('SendEmployeeCommunicationId', this.empId, id).catch(function (err) {
        return console.error(err.toString());
      });

    });

    this.hubConnection.on('ReceivePrivateMessage', (data) => {
      console.log(data);
      console.log("ReceivePrivateMessage")
      this.messengerService.ReceivePrivateMessage(data);
    });


    this.hubConnection.on("SendPrivateMessage", function (obj) {
      console.log(obj);
      /*var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
      var encodedMsg = user + " says " + msg;
      var li = document.createElement("li");
      li.textContent = encodedMsg;
      console.log(connId + senderId + user + message);*/
      //document.getElementById("messagesList").appendChild(li);
    });

    this.hubConnection.on('ReceiveEmployeeCommunicationId', (data) => {
      console.log(data);
      console.log("ReceiveEmployeeCommunicationId");
      //Assign communicationId to employee in recent chat. 

    });


  }

  public SendMessageNotification = (msg) => {
    this.hubConnection.invoke("SendPrivateMessage", msg.connId, msg.senderId, msg.user, msg.message).catch(function (err) {
      return console.error(err.toString());
    });
  }
}
