import { EventEmitter, Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { SignalRService } from './signal-r.service';

@Injectable(
    {
        providedIn: 'root',
    })

export class MessengerService {
    constructor(private http: HttpUtilsService, private signalRService: SignalRService) { }


    closeConnection() {

        const commObj = {
            EmployeeId: localStorage.getItem('empId'),
            CommunicationId: '0'
        };

        this.UpdateChatCommunication(commObj).subscribe(data => {
            console.log(data);
        });

        this.signalRService.stopConnection();
    }


    GetEmployeeList(employeeId) {
        return this.http.get(`${UrlConfig.Messenger.getEmployeeList}` + employeeId);
    }

    SendMessage(obj) {
        return this.http.post(`${UrlConfig.Messenger.sendMessage}`, obj);
    }

    CreateGroup(grpObj) {
        this.http.post(`${UrlConfig.Messenger.createGroup}`, grpObj)
    }

    UpdateChatCommunication(commObj) {
        return this.http.post(`${UrlConfig.Messenger.updateChatCommunicationDetail}`, commObj);
    }

    GetChatMessage(chatObj) {
        return this.http.post(`${UrlConfig.Messenger.getChatMessage}`, chatObj);
    }

    ReceivePrivateMessage(msg) {
        console.log("ReceivePrivateMessage");
        console.log(msg.ConnectionId);

        console.log(msg);
    }
    sendGroupMessage(groupMsgObj) {
        return this.http.post(`${UrlConfig.Messenger.sendGroupMessage}`, groupMsgObj);
    }

    getUnReadMessageCount(employeeId) {
        return this.http.get(`${UrlConfig.Messenger.getEmployeeList}` + employeeId);
    }

    getGroupMembers(groupId) {
        return this.http.get(`${UrlConfig.Messenger.getGroupMemberList}` + groupId)
    }
}
