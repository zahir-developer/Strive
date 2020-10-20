import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
declare var $: any;
@Component({
  selector: 'app-messenger-employee-search',
  templateUrl: './messenger-employee-search.component.html',
  styleUrls: ['./messenger-employee-search.component.css']
})
export class MessengerEmployeeSearchComponent implements OnInit {
  search = '';
  empList = [];
  selectAll = false;
  @Output() emitNewChat = new EventEmitter();
  constructor(private empService: EmployeeService, private messengerService: MessengerService) { }

  ngOnInit(): void {
    this.getAllEmployees();
  }
  closeemp() {
    $('#show-search-emp').hide();
    $('.internal-employee').addClass('col-xl-9');
    $('.internal-employee').removeClass('col-xl-6');
    $('.view-msg').addClass('Message-box-slide');
    $('.view-msg').removeClass('Message-box');
    $('.plus-icon').removeClass('opacity-16');
    this.clearValue();
  }
  clearValue() {
    this.search = '';
    this.clearSelectAllFlag();
    this.getAllEmployees();
  }
  clearSelectAllFlag() {
    (document.getElementById('selectAll') as HTMLInputElement).checked = false;
  }
  getAllEmployees() {
    this.clearSelectAllFlag();
    this.empService.searchEmployee(this.search).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList.EmployeeList;
        this.setDefaultBoolean(false);
        this.setName();
      }
    });
  }
  setName() {
    this.empList.map(item => {
      const intial = item.FirstName.charAt(0).toUpperCase() + item.LastName.charAt(0).toUpperCase();
      console.log(intial);
      item.Initial = intial;
    });
  }
  setDefaultBoolean(flag) {
    this.empList.forEach(item => {
      item.isSelected = flag;
    });
  }
  selectAllEmployees(event) {
    if (event.target.checked === true) {
      this.setDefaultBoolean(true);
    } else {
      this.setDefaultBoolean(false);
    }
  }
  addEmployees() {
    let chatUserGroup = [];
    const selectedEmp = this.empList.filter(item => item.isSelected === true);
    if (selectedEmp.length === 1) {
      this.emitNewChat.emit(selectedEmp);
      this.closeemp();
    } else {
      selectedEmp.map(item => {
        const userGroup = {
          chatGroupUserId: 0,
          userId: item.EmployeeId,
          chatGroupId: 0,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date()
        };
        chatUserGroup.push(userGroup);
      });
      console.log(chatUserGroup);
      const groupObj = {
        chatGroup: {
          chatGroupId: 0,
          groupName: 'Group Chat',
          comments: null,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date(),
          updatedBy: 0,
          updatedDate: new Date()
        },
        chatUserGroup
      };
      this.messengerService.sendGroupMessage(groupObj).subscribe(data => {
        console.log(data, 'groupChat');
        if (data.status === 'Success') {
          const groupId = JSON.parse(data.resultData);
          this.groupIdInsertion(selectedEmp, groupId.Status);
        }
      });
    }
  }
  groupIdInsertion(selectedEmp, groupId) {
    const groupChatEmployees = selectedEmp.map(item => {
      return {
        EmployeeId: item.EmployeeId,
        FirstName: item.FirstName,
        LastName: item.LastName,
        CommunicationId: '0',
        ChatCommunicationId: '0',
        GroupId: groupId
      };
    });
    this.emitNewChat.emit(groupChatEmployees);
  }
  searchFocus() {
    this.search = this.search.trim();
  }
  changeState() {
    this.clearSelectAllFlag();
  }
}
