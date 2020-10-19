import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
declare var $: any;
@Component({
  selector: 'app-messenger-employee-search',
  templateUrl: './messenger-employee-search.component.html',
  styleUrls: ['./messenger-employee-search.component.css']
})
export class MessengerEmployeeSearchComponent implements OnInit {
  search = '';
  empList = [];
  groupname = '';
  selectAll = false;
  @Output() emitNewChat = new EventEmitter();
  @Input() selectedEmployee: any = [];
  constructor(private empService: EmployeeService, private messengerService: MessengerService,
    private messageService: MessageServiceToastr) { }

  ngOnInit(): void {
    $('#getGroupName').hide();
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
  getSelectedEmp() {
    return this.empList.filter(item => item.isSelected === true);
  }
  addEmployees() {
    this.groupname = '';
    const selectedEmp = this.getSelectedEmp();
    if (selectedEmp.length === 0) {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Please select the employees' });
      return;
    } else if (selectedEmp.length > 1) {
      $('#getGroupName').modal({ backdrop: 'static', keyboard: false });
    } else {
      this.emitNewChat.emit(selectedEmp);
      this.closeemp();
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
  AddGroupName(event) {
    if (event === 'cancel') {
      $('#getGroupName').modal('hide');
      this.groupname = '';
    } else {
      const chatUserGroup = [];
      const selectedEmp = this.getSelectedEmp();
      selectedEmp.push(this.selectedEmployee);
      selectedEmp.map(item => {
        const userGroup = {
          chatGroupUserId: 0,
          userId: item.EmployeeId ? item.EmployeeId : item.Id,
          chatGroupId: 0,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date()
        };
        chatUserGroup.push(userGroup);
      });
      const groupObj = {
        chatGroup: {
          chatGroupId: 0,
          groupName: this.groupname,
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
        if (data.status === 'Success') {
          $('#getGroupName').modal('hide');
          const groupId = JSON.parse(data.resultData);
          const createdGroupObj = [{
            EmployeeId: groupId?.Status,
            FirstName: this.groupname,
            Initial: '',
            LastName: null,
          }];
          this.emitNewChat.emit(createdGroupObj);
          this.closeemp();
          // this.groupIdInsertion(selectedEmp, groupId.Status);
        }
      });

    }
  }
}
