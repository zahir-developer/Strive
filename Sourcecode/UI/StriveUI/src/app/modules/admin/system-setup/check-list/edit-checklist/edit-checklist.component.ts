import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CheckListService } from 'src/app/shared/services/data-service/check-list.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-edit-checklist',
  templateUrl: './edit-checklist.component.html',
  styleUrls: ['./edit-checklist.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class EditChecklistComponent implements OnInit {
  notificationTime = '';
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
    if (this.notificationTime === '') {
      return;
    }
    if (this.NotificationList !== null) {
      if (this.NotificationList.length >= ApplicationConfig.ChecklistNotification.MaxLength) {
        this.isNotificationTimeLimit = true;
        this.notificationTime = '';
        return;
      }
    }
    this.NotificationList.push({
      ChecklistNotificationId: 0,
      CheckListId: this.selectedData.ChecklistDetail.ChecklistId,
      NotificationTime: this.notificationTime,
      isActive: true,
      isDeleted: false,
    });

    if (this.NotificationList !== null) {
      this.NotificationList.forEach((item, index) => {
        item.id = index;
      });
    }
    this.notificationTime = '';
  }

  removeTime(time) {
    if (time.checkListNotificationId === 0) {
      this.NotificationList = this.NotificationList.filter(item => item.id !== time.id);
    } else {
      this.NotificationList = this.NotificationList.filter(item => item.ChecklistNotificationId !== time.ChecklistNotificationId);
      this.deletedTime.push(time);
    }
    if (this.NotificationList.length <= ApplicationConfig.ChecklistNotification.MaxLength) {
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
    if (this.NotificationList !== null) {
      this.NotificationList.forEach(item => {
        notificationTimeList.push({
          checkListNotificationId: item.ChecklistNotificationId,
          checklistId: item.CheckListId,
          notificationTime: item.NotificationTime,
          isActive: true,
          isDeleted: false,
        });
      });
    }

    if (this.deletedTime !== null) {
      this.deletedTime.forEach(item => {
        notificationTimeList.push({
          checkListNotificationId: item.ChecklistNotificationId,
          checklistId: item.CheckListId,
          notificationTime: item.NotificationTime,
          isActive: true,
          isDeleted: true,
        });
      });
    }

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
    this.checkListSetup.updateChecklist(finalObj).subscribe(res => {
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
