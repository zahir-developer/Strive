import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';
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
  @Output() emitFirstMessage = new EventEmitter();
  @Output() emitRefreshGroupUsers = new EventEmitter();
  @Input() selectedEmployee?: any;
  @Input() currentEmployeeId: any = 1;
  @Input() popupType: any = '';
  chatGroupId : number;
  constructor(private empService: EmployeeService, private messengerService: MessengerService,
    private toastr: ToastrService,
    private spinner : NgxSpinnerService) { }

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
    var selectAll = (document.getElementById('selectAll') as HTMLInputElement)
    
    if(selectAll !== null)
    {
      selectAll.checked = false;
    }
  }
  getAllEmployees() {
    this.clearSelectAllFlag();
    const empObj = {
      startDate: null,
      endDate: null,
      locationId: null,
      pageNo: null,
      pageSize: null,
      query: this.search,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.messengerService.getAllEmployeeName(empObj).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        if (empList.EmployeeList.Employee !== null) {
          this.empList = empList.EmployeeList.Employee;
        }
        else
        {
          this.empList = null;
        }

        this.setDefaultBoolean(false);
        this.setName();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
  showPopup() {
    $('#getGroupName').modal({ backdrop: 'static', keyboard: false });
  }
  addEmployees() {
    this.groupname = '';
    const selectedEmp = this.getSelectedEmp();
    if (selectedEmp.length === 0) {
      this.toastr.success(MessageConfig.Messenger.empselect, 'Success!');
      return;
    } else if (this.popupType === 'newChat') {
      if (selectedEmp.length === 1) {
        selectedEmp[0].IsGroup = false;
        this.emitNewChat.emit(selectedEmp);
        this.closeemp();
      } else {
        this.showPopup();
      }
      // this.emitFirstMessage.emit(selectedEmp);
    } else if (this.selectedEmployee?.IsGroup === false) {
      const emp = this.checkDuplicate();
      if (emp.length > 1) {
        this.showPopup();
      } else {
        this.setDefaultBoolean(false);
        this.clearSelectAllFlag();
        return;
      }
    } else {
      this.groupname = this.selectedEmployee?.FirstName;
      this.AddGroupName('create');
    }
  }
  searchFocus() {
    this.search = this.search.trim();
  }
  changeState() {
    this.clearSelectAllFlag();
  }
  checkDuplicate(): any {
    const selectedEmp = this.getSelectedEmp();
    if (this.popupType === 'newChat') {
      return selectedEmp;
    }
    const duplicateEmp = selectedEmp.filter(emp => +emp.EmployeeId === +this.selectedEmployee?.Id);
    if (duplicateEmp.length > 0) {
      return selectedEmp;
    } else {
      if (this.selectedEmployee?.IsGroup === false) {
        selectedEmp.push(this.selectedEmployee);
      }
      return selectedEmp;
    }
  }
  AddGroupName(event) {
    if (event === 'cancel') {
      $('#getGroupName').modal('hide');
      this.groupname = '';
    } else {
      const chatUserGroup = [];
      const selectedEmp = this.checkDuplicate();
      this.chatGroupId = (this.popupType === 'oldChat' && this.selectedEmployee?.IsGroup === true) ? this.selectedEmployee?.Id : 0;
      selectedEmp.forEach(item => {
        const userGroup = {
          chatGroupUserId: 0,
          userId: item.EmployeeId ? item.EmployeeId : item.Id,
          CommunicationId: item.CommunicationId,
          chatGroupId: this.chatGroupId,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date()
        };
        chatUserGroup.push(userGroup);

      });

      if (!this.selectedEmployee?.IsGroup) {
        const currentUser = {
          chatGroupUserId: 0,
          userId: localStorage.getItem('empId'),
          CommunicationId: '0',
          chatGroupId: this.chatGroupId,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date()
        };
        chatUserGroup.push(currentUser);
      }

      const groupObj = {
        chatGroup: {
          chatGroupId: this.chatGroupId,
          groupName: this.groupname,
          comments: null,
          isActive: true,
          isDeleted: false,
          createdBy: 0,
          createdDate: new Date(),
          updatedBy: 0,
          updatedDate: new Date()
        },
        chatUserGroup,
        GroupId: this.selectedEmployee?.CommunicationId
      };
      if (this.selectedEmployee?.IsGroup === true && this.popupType === 'oldChat') {
        groupObj.chatGroup = null;
      }
    this.spinner.show();
      this.messengerService.createGroup(groupObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          $('#getGroupName').modal('hide');
          const groupObject = JSON.parse(data.resultData);

          const createdGroupObj = [{
            EmployeeId: groupObject.Result.ChatGroupId,
            FirstName: this.groupname,
            Initial: '',
            CommunicationId: groupObject.Result.GroupId,
            LastName: null,
            IsGroup: true,
            isRead: true,
            type: 'new Group'
          }];
          if (groupObj.chatGroup !== null) {
            this.emitNewChat.emit(createdGroupObj);
            this.closeemp();
          } else {
            this.closeemp();
          }
          if (this.popupType === 'oldChat') {
            this.chatGroupId = this.chatGroupId === 0 ? groupObject.Result.ChatGroupId : this.chatGroupId;
            this.emitRefreshGroupUsers.emit(this.chatGroupId);
          }
          else {
            this.emitRefreshGroupUsers.emit(groupObject.Result.ChatGroupId);
          }
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });

    }
  }

  removeSelectedEmployee() {
    if (!this.selectedEmployee.IsGroup) {
      this.empList = this.empList.filter(emp => emp.EmployeeId !== this.selectedEmployee.Id);
    }
  }
}
