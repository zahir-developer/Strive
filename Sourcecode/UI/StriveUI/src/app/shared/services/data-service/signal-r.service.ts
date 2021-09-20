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

private recGrpMsg: BehaviorSubject<any> = new BehaviorSubject<any>(null);
public ReceiveGrpMsg: Observable<any> = this.recGrpMsg.asObservable();

  constructor() { }

  public data: any;
  private hubConnection: signalR.HubConnection;
  public startConnection = () => {
  this.hubConnection = new signalR.HubConnectionBuilder()
  .withUrl(environment.api.striveUrl + 'ChatMessageHub').build();


    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public stopConnection = () => {
    console.log('stopConnection');
    this.hubConnection?.invoke('SendEmployeeCommunicationId', localStorage.getItem('empId'), '0').catch(function (err) {
      return console.error(err.toString());
    });
    this.hubConnection?.stop();
  }

  public SubscribeChatEvents = () => {
    this.hubConnection?.on('OnDisconnected', (data) => {
      console.log('onDisconnected');
      console.log(data);
    });

    this.hubConnection.on('ReceiveCommunicationID', (id) => {
      this.connId = id;
      console.log('ReceiveCommunicationID: '+ id);
      this.hubConnection?.invoke('SendEmployeeCommunicationId', this.empId, id).catch(function (err) {
        return console.error(err.toString());
      });
    });

    this.hubConnection.on('ReceivePrivateMessage', (data) => {
      this.setReceivedMsg(data);
    });

    this.hubConnection.on('ReceiveGroupMessage', (data) => {
      this.setGrpReceivedMsg(data);
    });

    this.hubConnection.on("SendPrivateMessage", function (obj) {

    });

    

    this.hubConnection.on('ReceiveEmployeeCommunicationId', (data) => {
      if (data !== null) {
        console.log('ReceiveEmployeeCommunicationId' + data); 
        this.setname(data);
      }
    });
    this.hubConnection.on('UserAddedtoGroup', (data) => {
      if (data !== null) {
        console.log('UserAddedtoGroup' + data); 
      }
    });

    this.hubConnection.on('GroupMessageReceive', (data) => {
      if (data !== null) {
        this.setGrpReceivedMsg(data);
      }
    });
  }
  setname(data) {
this.commId.next(data);
  }
  setReceivedMsg(data) {
    this.recMsg.next(data);
  }

  setGrpReceivedMsg(data) {
    this.recGrpMsg.next(data);
  }

}
