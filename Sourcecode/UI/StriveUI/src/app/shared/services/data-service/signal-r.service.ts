import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { ThemeService } from '../../common-service/theme.service';

@Injectable({
  providedIn: 'root'
})


export class SignalRService {

  connId: string;
  empId = localStorage.getItem('empId');
private commId: BehaviorSubject<any> = new BehaviorSubject<any>(null);
public communicationId: Observable<any> = this.commId.asObservable();
private recMsg: BehaviorSubject<any> = new BehaviorSubject<any>(null);
public ReceivedMsg: Observable<any> = this.recMsg.asObservable();
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
      this.connId = id;
      // this.messengerService.UpdateChatCommunication(id);
console.log('ReceiveCommunicationID: '+ id);
      this.hubConnection.invoke('SendEmployeeCommunicationId', this.empId, id).catch(function (err) {
        return console.error(err.toString());
      });

    });

    this.hubConnection.on('ReceivePrivateMessage', (data) => {
      console.log('Messager Received');
      console.log(data);
      this.setReceivedMsg(data);
      
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
      if (data !== null) {
        console.log('ReceiveEmployeeCommunicationId' + data); 
        this.setname(data);
        // this.messengerService.UpdateChatCommunication(data[0], data[1]);
      }
    });
  }
  setname(data) {
this.commId.next(data);
  }
  setReceivedMsg(data) {
    this.recMsg.next(data);
  }

}
