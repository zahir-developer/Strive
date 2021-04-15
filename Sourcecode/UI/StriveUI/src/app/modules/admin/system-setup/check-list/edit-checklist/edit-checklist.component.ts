import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CheckListService } from 'src/app/shared/services/data-service/check-list.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-edit-checklist',
  templateUrl: './edit-checklist.component.html',
  styleUrls: ['./edit-checklist.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class EditChecklistComponent implements OnInit {
  notificationTime: any = '';
  roleId: any = '';
  checkListName: any;
  isNotificationTimeLimit: boolean;
  notificationTimeList = [];
  deletedTime = [];
  @Input() NotificationList?: any;
  @Input() selectedData?: any;
  @Input() rollList?: any = [];
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private toastr: ToastrService,
    private checkListSetup: CheckListService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.isNotificationTimeLimit = false;
    console.log(this.selectedData, 'selectedDate');
    this.bindValue();
  }

  closeModal() {
    this.activeModal.close();
  }

  bindValue() {
    this.checkListName = this.selectedData.ChecklistDetail.Name;
    this.roleId = this.selectedData.ChecklistDetail.RoleId;
  }

  editTime() {
    if (this.NotificationList.length >= 10) {
      this.isNotificationTimeLimit = true;
      this.notificationTime = '';
      return;
    }
    this.NotificationList.push({
      checkListNotificationId: 0,
      checkListId: this.selectedData.ChecklistDetail.ChecklistId,
      NotificationTime: this.notificationTime,
      isActive: true,
      isDeleted: false,
    });
    this.NotificationList.forEach((item, index) => {
      item.id = index;
    });
    this.notificationTime = '';
  }

  removeTime(time) {
    if (time.checkListNotificationId === 0) {
      this.NotificationList = this.NotificationList.filter(item => item.id !== time.id);
    } else {
      this.NotificationList = this.NotificationList.filter(item => item.ChecklistNotificationId !== time.ChecklistNotificationId);
      this.deletedTime.push(time);
    }
    if (this.NotificationList.length <= 10) {
      this.isNotificationTimeLimit = false;
    }
  }

  updateChecklist() {
    if (this.roleId === '') {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.CheckList.roleNameValidation, 'Warning!');
      return;
    }
    const pattern = /[a-zA-Z~`\d!@#$%^&*()-_=+][a-zA-Z~`\d!@#$%^&*()-_=+\d\\s]*/;

    if (!pattern.test(this.checkListName) || this.checkListName === undefined) {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.CheckList.CheckListNameValidation, 'Warning!');
      return;
    }

    const notificationTimeList = [];
    this.NotificationList.forEach(item => {
      notificationTimeList.push({
        checkListNotificationId: item.ChecklistNotificationId,
        checklistId: item.CheckListId,
        notificationTime: item.NotificationTime,
        isActive: true,
        isDeleted: false,
      });
    });

    this.deletedTime.forEach(item => {
      notificationTimeList.push({
        checkListNotificationId: item.ChecklistNotificationId,
        checklistId: item.CheckListId,
        notificationTime: item.NotificationTime,
        isActive: true,
        isDeleted: true,
      });
    });

    const checkListObj = {
      checklistId: this.selectedData.ChecklistDetail.ChecklistId,
      name: this.checkListName,
      roleId: this.roleId,
      isActive: true,
      isDeleted: false,
    };
    const finalObj = {
      checklist: checkListObj,
      checkListNotification: notificationTimeList
    };
    this.spinner.show();
    this.checkListSetup.addCheckListSetup(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        this.toastr.success(MessageConfig.Admin.SystemSetup.CheckList.Update, 'Success!');
        this.activeModal.close(true);
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
