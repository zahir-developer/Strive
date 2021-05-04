import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CheckListService } from 'src/app/shared/services/data-service/check-list.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-add-checklist',
  templateUrl: './add-checklist.component.html',
  styleUrls: ['./add-checklist.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AddChecklistComponent implements OnInit {
  notificationTime = '';
  roleId: any = '';
  checkListName: any;
  isNotificationTimeLimit: boolean;
  notificationTimeList = [];
  @Input() rollList?: any = [];
  constructor(
    private activeModal: NgbActiveModal,
    private toastr: ToastrService,
    private checkListSetup: CheckListService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.isNotificationTimeLimit = false;
  }

  addTime() {
    if (this.notificationTime === '') {
      return;
    }
    if (this.notificationTimeList.length >= ApplicationConfig.ChecklistNotification.MaxLength) {
      this.isNotificationTimeLimit = true;
      this.notificationTime = '';
      return;
    }
    this.notificationTimeList.push({
      time: this.notificationTime
    });
    this.notificationTimeList.forEach((item, index) => {
      item.id = index;
    });
    this.notificationTime = '';
  }

  removeTime(time) {
    this.notificationTimeList = this.notificationTimeList.filter(item => item.id !== time.id);
    if (this.notificationTimeList.length < ApplicationConfig.ChecklistNotification.MaxLength) {
      this.isNotificationTimeLimit = false;
    }
  }

  closeModal() {
    this.activeModal.close(false);
  }

  addChecklist() {
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
    this.notificationTimeList.forEach(item => {
      notificationTimeList.push({
        checkListNotificationId: 0,
        checklistId: 0,
        notificationTime: item.time,
        isActive: true,
        isDeleted: false,
      });
    });

    const checkListObj = {
      checklistId: 0,
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
    this.checkListSetup.addCheckListSetup(finalObj).subscribe( res => {
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
