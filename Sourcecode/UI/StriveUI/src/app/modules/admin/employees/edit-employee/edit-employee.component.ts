import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import * as moment from 'moment';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() employeeRoles?: any;
  @Input() gender?: any;
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeId?: any;
  @Input() location?: any;
  Status: any;
  @Input() commissionType: any;
  @Input() actionType: string;
  employeeData: any;
  personalform: FormGroup;
  emplistform: FormGroup;
  isEditPersonalDetail: boolean;
  isEditEmployeeList: boolean;
  employeeCollision: any = [];
  dropdownSettings: IDropdownSettings = {};
  documentList: any = [];
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private messageService: MessageServiceToastr) { }

  ngOnInit(): void {
    this.isEditPersonalDetail = false;
    this.Status = ['Active', 'InActive'];
    this.personalform = this.fb.group({
      firstName: [''],
      lastName: [''],
      gender: [''],
      address: [''],
      mobile: [''],
      immigrationStatus: [''],
      ssn: ['']
    });
    this.emplistform = this.fb.group({
      loginId: [''],
      password: [''],
      dateOfHire: [''],
      hourlyRateWash: [''],
      hourlyRateDetail: [''],
      commission: [''],
      status: [''],
      tip: [''],
      exemptions: [''],
      roles: [''],
      location: ['']
    });
    this.employeRole();
    this.locationDropDown();
    this.employeeDetail();
    this.getAllCollision();
    this.getAllDocument();
  }

  employeeDetail() {
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      console.log(res, 'getEmployeById');
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        console.log(employees, 'employeDeatil');
        if (employees.EmployeeDetail.length > 0) {
          this.employeeData = employees.EmployeeDetail[0];
          this.setValue();
        }
      }
    });
  }

  setValue() {
    if (this.employeeData !== undefined && this.actionType === 'edit') {
      console.log(this.employeeData, 'data');
      const employee = this.employeeData;
      const employeeDetail = employee.EmployeeDetail[0];
      const employeeAddress = employee.EmployeeAddress[0];
      const employeeRole = employee.EmployeeRole !== null ? employee.EmployeeRole[0] : '';
      this.personalform.patchValue({
        firstName: employee.FirstName ? employee.FirstName : '',
        lastName: employee.LastName ? employee.LastName : '',
        gender: employee.Gender ? employee.Gender : '',
        address: employeeAddress.Address1 ? employeeAddress.Address1 : '',
        mobile: employeeAddress.PhoneNumber ? employeeAddress.PhoneNumber : '',
        immigrationStatus: employee.ImmigrationStatus ? employee.ImmigrationStatus : '',
        ssn: employee.SSNo ? employee.SSNo : '',
      });
      this.emplistform.patchValue({
        loginId: [''],
        password: [''],
        dateOfHire: employeeDetail.HiredDate ? moment(employeeDetail.HiredDate).toDate() : '',
        hourlyRateWash: [''],
        hourlyRateDetail: [''],
        commission: employeeDetail.ComRate ? employeeDetail.ComRate : '',
        status: employee.IsActive ? 'Active' : 'InActive',
        tip: employeeDetail.Tip ? employeeDetail.Tip : '',
        exemptions: employeeDetail.Exemptions ? employeeDetail.Exemptions : '',
        roles: employeeRole.RoleId ? employeeRole.RoleId : '',
        location: employeeDetail.LocationId ? employeeDetail.LocationId : ''
      });
      this.personalform.disable();
      this.emplistform.disable();
    }
  }

  employeRole() {
    this.employeeRoles = this.employeeRoles.map(item => {
      return {
        item_id: item.CodeId,
        item_text: item.CodeValue
      };
    });
    console.log(this.employeeRoles, 'employeerolesmuliti');
    this.dropdownSettings = {
      singleSelection: false,
      defaultOpen: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: false
    };
  }

  locationDropDown() {
    this.location = this.location.map( item => {
      return {
        item_id: item.LocationId,
        item_text: item.LocationName
      };
    });
  }

  editPersonalDetail() {
    this.personalform.enable();
    this.isEditPersonalDetail = true;
  }

  cancelPersonalDetail() {
    this.personalform.disable();
    this.isEditPersonalDetail = false;
  }

  editEmployeeList() {
    this.emplistform.enable();
    this.isEditEmployeeList = true;
  }

  cancelEmployeeList() {
    this.emplistform.disable();
    this.isEditEmployeeList = false;
  }

  getAllCollision() {
    this.employeeService.getAllCollision(this.employeeId).subscribe( res => {
      console.log(res, 'allcollistion');
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        console.log(employeesCollison, 'employeDeatil');
        if (employeesCollison.CollisionDetailOfEmployee.length > 0) {
          this.employeeCollision = employeesCollison.CollisionDetailOfEmployee;
        }
      }
    });
  }

  backToGrid() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
        this.documentList = document.GetAllDocuments;
        console.log(this.documentList, 'documentlst');
      }
    });
  }


}
