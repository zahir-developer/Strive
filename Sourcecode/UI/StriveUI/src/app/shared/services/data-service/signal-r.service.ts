import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})


export class SignalRService {
  public data: any;
private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:60001/ChatMessageHub',
                            {
                              skipNegotiation: true,
                              transport: signalR.HttpTransportType.WebSockets
                            }).build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public UpdateCarCheckIn = () => {
    this.hubConnection.on('UpdateCarCheckIn', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
