import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as _ from 'underscore';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-assign-detail',
  templateUrl: './assign-detail.component.html',
  styleUrls: ['./assign-detail.component.css']
})
export class AssignDetailComponent implements OnInit {
  @Input() details?: any;
  @Input() employeeList?: any;
  detailService: any = [];
  assignForm: FormGroup;
  clonedServices: any = [];
  filteredEmployee: any = [];
  @Output() public storedService = new EventEmitter();
  @Input() assignedDetailService?: any;
  @Output() public closeAssignModel = new EventEmitter();
  clonedEmployee: any = [];
  constructor(
    private fb: FormBuilder,
    private confirmationService: ConfirmationUXBDialogService
  ) { }

  ngOnInit(): void {
    console.log(this.assignedDetailService, 'assignedDetailService');
    this.employeeDetail();
    this.detailService = this.assignedDetailService;
    this.clonedServices = this.details.map(x => Object.assign({}, x));
    this.assignForm = this.fb.group({
      employeeId: [''],
      serviceId: ['']
    });
  }

  employeeDetail() {
    this.employeeList = this.employeeList.map(item => {
      return {
        id: item.EmployeeId,
        name: item.LastName + '\t' + item.FirstName
      };
    });
    this.clonedEmployee = this.employeeList.map(x => Object.assign({}, x));
  }

  assignService() {
    const selectedService = _.where(this.details, { ServiceId: +this.assignForm.value.serviceId });
    const selectedEmployee = _.where(this.employeeList, { id: +this.assignForm.value.employeeId.id });
    const assignedService = [];
    selectedEmployee.forEach(employee => {
      selectedService.forEach(service => {
        this.detailService.push({
          ServiceId: service.ServiceId,
          ServiceName: service.ServiceName,
          Cost: service.Cost,
          EmployeeId: employee.id,
          EmployeeName: employee.name
        });
      });
    });
    // this.detailService = assignedService;
    this.assignForm.patchValue({
      employeeId: '',
      serviceId: ''
    });
    this.detailService.forEach((item, index) => {  // Adding Id to the grid
      item.detailServiceId = index + 1;
    });
    console.log(this.detailService, 'assignedservice');
  }

  delete(service) {
    this.detailService = this.detailService.filter(item => item.detailServiceId !== service.detailServiceId);
    this.serviceByEmployeeId(service.ServiceId);
  }

  confirmDelete(service) {
    this.detailService = this.detailService.filter(item => item.ServiceId !== service.ServiceId);
    this.serviceByEmployeeId(service.ServiceId);
  }

  serviceByEmployeeId(id) {
    this.details = this.clonedServices;
    if (this.detailService.length > 0) {
      const assignedEmployee = _.where(this.detailService, { EmployeeId: +id });
      if (assignedEmployee.length > 0) {
        assignedEmployee.forEach(item => {
          this.details = this.details.filter(elem => elem.ServiceId !== item.ServiceId);
        });
      } else {
        this.details = this.clonedServices;
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
    const assignServiceObj = {
      isSave: true,
      service: this.detailService
    };
    this.storedService.emit(assignServiceObj);
  }

  closeModel() {
    this.closeAssignModel.emit();
  }

  selectedEmployee(event) {

  }

  getDetailService() {
    this.details = this.details.map( item => {
      return {
        item_id: item.ServiceId,
        item_text: item.ServiceName
      };
    });
  }
}
