import { EventEmitter, Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable(
    {
        providedIn: 'root',
    })

export class MessengerService {
    constructor(private http: HttpUtilsService) { }

    GetEmployeeList() {
        return this.http.get(`${UrlConfig.Messenger.GetEmployeeList}`);
    }

    SendMessage(obj) {
        return this.http.post(`${UrlConfig.Messenger.SendMessage}`, obj);
    }

    CreateGroup(grpObj) {
        this.http.post(`${UrlConfig.Messenger.CreateGroup}`, grpObj)
    }

    UpdateChatCommunication(commId) {
        const commObj =
        {
            EmployeeId: localStorage.getItem('empId'),
            CommunicationId: commId
        }
        console.log(commObj);
        return this.http.post(`${UrlConfig.Messenger.UpdateChatCommunicationDetail}`, commObj);
    }

    GetChatMessage(chatObj) {
        return this.http.post(`${UrlConfig.Messenger.GetChatMessage}`, chatObj);
    }

    ReceivePrivateMessage(msg) {
        console.log(msg.ConnectionId);
        
        console.log(msg);
    }
}