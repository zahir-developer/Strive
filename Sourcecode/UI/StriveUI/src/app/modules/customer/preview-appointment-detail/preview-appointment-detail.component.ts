import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import * as _ from 'underscore';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-preview-appointment-detail',
  templateUrl: './preview-appointment-detail.component.html'
})
export class PreviewAppointmentDetailComponent implements OnInit {
  @Output() confirmation = new EventEmitter();
  @Output() appointmentPage = new EventEmitter();
  @Output() dashboardPage = new EventEmitter();
  @Input() scheduleDetailObj?: any;
  @Input() selectedData?: any;
  ticketNumber: any;
  jobTypeId: any;
  jobStatus: any = [];
  constructor(
    private detailService: DetailService,
    private spinner: NgxSpinnerService,
    private datePipe: DatePipe,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.ticketNumber = Math.floor(100000 + Math.random() * 900000);
    this.getJobStatus();
    this.getJobType();
  }

  bookNow() {
    const jobstatus = _.where(this.jobStatus, { CodeValue: ApplicationConfig.CodeValue.Waiting });
    let jobStatusId;
    if (jobstatus.length > 0) {
      jobStatusId = jobstatus[0].CodeId;
    }
    const job = {
      jobId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.JobId : 0,
      ticketNumber: this.ticketNumber,
      locationId: this.scheduleDetailObj.locationObj.LocationId,
      clientId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.ClientId : this.scheduleDetailObj.vechicleDetail.ClientId,
      vehicleId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.VehicleId : this.scheduleDetailObj.vechicleDetail.VehicleId,
      make: this.scheduleDetailObj.isEdit ? this.selectedData.Details.Make : this.scheduleDetailObj.vechicleDetail.VehicleMakeId,
      model: this.scheduleDetailObj.isEdit ? this.selectedData.Details.Model : this.scheduleDetailObj.vechicleDetail.VehicleModelId,
      color: this.scheduleDetailObj.isEdit ? this.selectedData.Details.Color : this.scheduleDetailObj.vechicleDetail.VehicleColorId,
      jobType: this.jobTypeId,
      jobDate: this.datePipe.transform(this.scheduleDetailObj.InTime, 'yyyy-MM-dd'),
      jobStatus: jobStatusId,
      timeIn: moment(this.scheduleDetailObj.InTime).format(),
      estimatedTimeOut: moment(this.scheduleDetailObj.OutTime).format(),
      actualTimeOut: new Date(),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
      notes: ''
    };
    const jobDetail = {
      jobDetailId: 0,
      jobId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.JobId : 0,
      bayId: this.scheduleDetailObj.Slot.BayId,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0
    };
    const baySchedule = {
      bayScheduleId: 0,
      bayId: this.scheduleDetailObj.Slot.BayId,
      jobId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.JobId : 0,
      scheduleDate: this.datePipe.transform(this.scheduleDetailObj.InTime, 'yyyy-MM-dd'),
      scheduleInTime: this.datePipe.transform(this.scheduleDetailObj.InTime, 'HH:mm'),
      scheduleOutTime: this.datePipe.transform(this.scheduleDetailObj.OutTime, 'HH:mm'),
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      updatedBy: 0,
    };
    const jobItem = [];
    jobItem.push({
      jobItemId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.JobItemId : 0,
      jobId: this.scheduleDetailObj.isEdit ? this.selectedData.Details.JobId : 0,
      serviceId: this.scheduleDetailObj.serviceobj.ServiceId,
      isActive: true,
      isDeleted: false,
      commission: 0,
      price: this.scheduleDetailObj.serviceobj.Cost,
      quantity: 1,
      createdBy: 0,
      updatedBy: 0
    });
    const formObj = {
      job,
      jobItem,
      jobDetail,
      baySchedule
    };
    if (this.scheduleDetailObj.isEdit) {
      this.spinner.show();
      this.detailService.updateDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.confirmation.emit();
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.detailService.addDetail(formObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.confirmation.emit();
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    }
  }

  cancel() {
    this.appointmentPage.emit();
  }

  getJobType() {
    this.detailService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Detail') {
              this.jobTypeId = item.valueid;
            }
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getJobStatus() {
    this.detailService.getJobStatus('JOBSTATUS').subscribe(res => {
      if (res.status === 'Success') {
        const status = JSON.parse(res.resultData);
        this.jobStatus = status.Codes;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  backToDashboard() {
    this.dashboardPage.emit();
  }

}
