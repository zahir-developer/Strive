import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as _ from 'underscore';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-assign-detail',
  templateUrl: './assign-detail.component.html'
})
export class AssignDetailComponent implements OnInit {
  @Input() isView?: any;
  @Input() details?: any;
  @Input() employeeList?: any;
  @Input() detailsJobServiceEmployee?: any;
  detailService: any = [];
  assignForm: FormGroup;
  clonedServices: any = [];
  filteredEmployee: any = [];
  @Output() public storedService = new EventEmitter();
  @Output() public closeAssignModel = new EventEmitter();
  clonedEmployee: any = [];
  dropdownSettings: IDropdownSettings = {};
  deleteIds: any = [];
  page = 1;
  pageSize = 5;
  collectionSize: number;
  @Output() cancelAssignModel = new EventEmitter();
  serviceType: any;
  constructor(
    private fb: FormBuilder, private getCode: GetCodeService,
    private confirmationService: ConfirmationUXBDialogService,
    private detailServices: DetailService,
    private spinner : NgxSpinnerService,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.detailService = this.detailsJobServiceEmployee;
    this.getAllServiceType();
    if (this.detailService.length > 0) {
      this.detailService.forEach((item, index) => {
        // Adding Id to the grid
        item.detailServiceId = index + 1;
      });
    }
    this.collectionSize = Math.ceil(this.detailService.length / this.pageSize) * 10;
    this.assignForm = this.fb.group({
      employeeId: [''],
      serviceId: ['']
    });
  }

  employeeDetail() {
    this.employeeList = this.employeeList.map(item => {
      return {
        item_id: item.EmployeeId,
        item_text: item.FirstName + '\t' + item.LastName
      };
    });
    this.clonedEmployee = this.employeeList.map(x => Object.assign({}, x));
    this.dropdownSetting();
  }

  dropdownSetting() {
    this.dropdownSettings = {
      singleSelection: ApplicationConfig.dropdownSettings.singleSelection,
      defaultOpen: ApplicationConfig.dropdownSettings.defaultOpen,
      idField: ApplicationConfig.dropdownSettings.idField,
      textField: ApplicationConfig.dropdownSettings.textField,
      itemsShowLimit: ApplicationConfig.dropdownSettings.itemsShowLimit,
      enableCheckAll: ApplicationConfig.dropdownSettings.enableCheckAll,
      allowSearchFilter: ApplicationConfig.dropdownSettings.allowSearchFilter
    };
  }

  assignService() {
    const selectedService = [];
    this.details.forEach(item => {
      this.assignForm.value.serviceId.forEach(service => {
        if (item.ServiceId === service.item_id || item.item_id === service.item_id) {
          selectedService.push(item);
        }
      });
    });

    this.assignForm.value.serviceId.forEach(service => {
      this.clonedServices = this.clonedServices.filter(item => item.item_id !== service.item_id);
    });
    selectedService.forEach(service => {
      this.assignForm.value.employeeId.forEach(emp => {
        const employeeService = {
          ServiceId: service.ServiceId,
          ServiceName: service.ServiceName,
          EmployeeId: emp.item_id,
          EmployeeName: emp.item_text,
          Cost: service.Cost,
          JobItemId: service.JobItemId,
          CommissionAmount: 0,
          CommissionCost: service.CommissionCost,
          CommissionType: service.CommissionType
        };

        if (service.CommissionType === 'Flat Fee') {
          employeeService.CommissionAmount = service.CommissionCost / this.assignForm.value.employeeId.length;
        } else if (service.CommissionType === 'Percentage') {
          const percentage = service.CommissionCost / this.assignForm.value.employeeId.length;
          employeeService.CommissionAmount = (service.Cost * percentage) / 100;
        }

        this.detailService.push(employeeService);
      });
    });
    this.assignForm.patchValue({
      employeeId: '',
      serviceId: ''
    });
    this.detailService.forEach((item, index) => {  // Adding Id to the grid
      item.detailServiceId = index + 1;
    });
    this.collectionSize = Math.ceil(this.detailService.length / this.pageSize) * 10;
  }

  onItemSelect(item: any) {
    this.employeeList = this.clonedEmployee;
    if (this.detailService.length > 0) {
      const assignedEmployee = _.where(this.detailService, { ServiceId: +item.item_id });
      if (assignedEmployee.length > 0) {
        assignedEmployee.forEach(emp => {
          this.employeeList = this.employeeList.filter(elem => elem.item_id !== emp.EmployeeId);
        });
      } else {
        this.employeeList = this.clonedEmployee;
      }
    }
  }

  delete(service) {
    const deleteService = _.where(this.detailService, { JobServiceEmployeeId: +service.JobServiceEmployeeId });
    if (deleteService.length > 0) {
      this.deleteIds.push(deleteService[0]);
    }
    this.detailService = this.detailService.filter(item => item.detailServiceId !== service.detailServiceId);
    const clonedDetailService = this.detailService.map(x => Object.assign({}, x));
    this.serviceByEmployeeId(service.ServiceId);
    this.detailService = [];
    this.clonedServices = [];
    this.details.forEach(item => {
      const selectedService = clonedDetailService.filter(elem => elem.ServiceId === item.ServiceId);
      if (selectedService.length > 0) {
        selectedService.forEach(emp => {
          if (service.CommissionType === 'Flat Fee') {
            emp.CommissionAmount = emp.CommissionCost / selectedService.length;
          } else if (emp.CommissionType === 'Percentage') {
            const percentage = emp.CommissionCost / selectedService.length;
            emp.CommissionAmount = (emp.Cost * percentage) / 100;
          }
          this.detailService.push(emp);
        });
      } else {
        this.clonedServices.push({
          item_id: item.ServiceId,
          item_text: item.ServiceName
        });
      }
    });
  }

  confirmDelete(service) {
    this.detailService = this.detailService.filter(item => item.ServiceId !== service.ServiceId);
    this.serviceByEmployeeId(service.ServiceId);
  }

  serviceByEmployeeId(id) {
    if (this.detailService.length > 0) {
      const assignedEmployee = _.where(this.detailService, { EmployeeId: +id });
      if (assignedEmployee.length > 0) {
        assignedEmployee.forEach(item => {
          this.details = this.details.filter(elem => elem.ServiceId !== item.ServiceId);
        });
      }
    }
  }

  employeeByServiceId(id) {
    this.employeeList = this.clonedEmployee;
    if (this.detailService.length > 0) {
      const assignedEmployee = _.where(this.detailService, { ServiceId: +id });
      if (assignedEmployee.length > 0) {
        assignedEmployee.forEach(item => {
          this.employeeList = this.employeeList.filter(elem => elem.id !== item.EmployeeId);
        });
      } else {
        this.employeeList = this.clonedEmployee;
      }
    }
  }

  filterEmployee(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.employeeList) {
      const employee = i;
      if (employee.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(employee);
      }
    }
    this.filteredEmployee = filtered;
  }

  saveAssignedService() {
    const assignServiceObj = [];
    this.detailService.forEach(item => {
      assignServiceObj.push({
        jobServiceEmployeeId: item.JobServiceEmployeeId ? item.JobServiceEmployeeId : 0,
        jobItemId: item.JobItemId,
        serviceId: item.ServiceId,
        employeeId: item.EmployeeId,
        commissionAmount: item.CommissionAmount,
        isActive: true,
        isDeleted: false,
        createdBy: 0,
        createdDate: new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      });
    });
    this.deleteIds.forEach(item => {
      assignServiceObj.push({
        jobServiceEmployeeId: item.JobServiceEmployeeId,
        jobItemId: item.JobItemId,
        serviceId: item.ServiceId,
        employeeId: item.EmployeeId,
        isActive: true,
        isDeleted: true,
        createdBy: 0,
        createdDate: new Date(),
        updatedBy: 0,
        updatedDate: new Date()
      });
    });
    const finalObj = {
      jobServiceEmployee: assignServiceObj
    };
    this.spinner.show();
    this.detailServices.saveEmployeeWithService(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.cancelAssignModel.emit();
      }
      else{
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  closeModel() {
    this.closeAssignModel.emit();
  }

  selectedEmployee(event) {

  }

  getAllServiceType() {
    this.getCode.getCodeByCategory(ApplicationConfig.Category.serviceType).subscribe(data => {
      if (data.status === 'Success') {
        const cType = JSON.parse(data.resultData);
        this.serviceType = cType.Codes.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.DetailUpcharge)[0];
        this.getDetailService();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getDetailService() {
    this.details = this.details.filter(i => i.ServiceTypeId !== this.serviceType.CodeId);
    this.clonedServices = this.details.map(x => Object.assign({}, x));
    this.clonedServices = this.clonedServices.map(item => {
      return {
        item_id: item.ServiceId,
        item_text: item.ServiceName
      };
    });
    if (this.detailService.length > 0) {
      this.clonedServices = [];
      this.details.forEach(item => {
        const selectedService = this.detailService.filter(elem => elem.ServiceId === item.ServiceId);
        if (selectedService.length === 0) {
          this.clonedServices.push({
            item_id: item.ServiceId,
            item_text: item.ServiceName
          });
        }
      });
    }
    this.employeeDetail();
    this.dropdownSetting();
  }
}
