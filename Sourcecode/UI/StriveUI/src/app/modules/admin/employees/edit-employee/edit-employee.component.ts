import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import * as _ from 'underscore';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';

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
  @Input() employeeDetailId?: any;
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
  isPersonalCollapsed = false;
  isDetailCollapsed = false;
  submitted: boolean;
  selectedRole: any = [];
  selectedLocation: any = [];
  employeeAddressId: any;
  ctypeLabel: any;
  deSelectRole: any = [];
  deSelectLocation: any = [];
  imigirationStatus: any = [];
  isAlien: boolean = false;
  isDate: boolean = false;
  isCitizen: boolean = true;
  isHourlyRate: boolean = false;
  isRequired: boolean = false;
  employeeRole: any = [];
  employeeLocation: any = [];
  roleId: any;
  locationId: any;
  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private toastr: ToastrService,
    private getCode: GetCodeService
  ) { }

  ngOnInit(): void {
    this.ctypeLabel = 'none';
    this.isEditPersonalDetail = false;
    this.submitted = false;
    this.Status = ['Active', 'Inactive'];
    this.getImmigrationStatus();
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      address: ['', Validators.required],
      mobile: ['', Validators.required],
      immigrationStatus: ['', Validators.required],
      ssn: ['', Validators.required],
      alienNumber: [''],
      permitDate: ['']
    });
    this.emplistform = this.fb.group({
      emailId: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
      dateOfHire: ['', Validators.required],
      hourlyRateWash: ['', Validators.required],
      hourlyRateDetail: [''],
      comType: [''],
      comRate: [''],
      status: [''],
      tip: [''],
      exemptions: [''],
      roles: [[]],
      location: [[]],
      employeeCode: ['']
    });
    this.roleId = localStorage.getItem('roleId');
    this.locationId = localStorage.getItem('empLocationId');
    this.employeRole();
    this.locationDropDown();
    this.dropdownSetting();
    // this.getAllCollision();
    // this.getAllDocument();
  }

  dropdownSetting() {
    this.dropdownSettings = {
      singleSelection: false,
      defaultOpen: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 2,
      allowSearchFilter: false
    };
  }

  getImmigrationStatus() {
    this.getCode.getCodeByCategory("IMMIGRATIONSTATUS").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.imigirationStatus = cType.Codes;
        this.dropdownSetting();
        this.employeeDetail();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  immigrationChange(data) {
    const temp = this.imigirationStatus.filter(item => item.CodeId === +data);
    if (temp.length !== 0) {
      if (temp[0].CodeValue === 'A Lawful permanent Resident (Alien #) A') {
        this.isAlien = true;
        this.isCitizen = false;
      } else {
        this.isAlien = false;
      }
      if (temp[0].CodeValue === 'An alien authorized to work until') {
        this.isDate = true;
        this.isCitizen = false;
      } else {
        this.isDate = false;
      }
    }
  }

  employeeDetail() {
    this.dropdownSetting();
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        this.employeeData = employees.Employee;
        if (employees.Employee.EmployeeCollision !== null) {
          this.employeeCollision = employees.Employee.EmployeeCollision;
        }
        this.setValue();
      }
    });
  }

  reloadCollisionGrid() {
    this.employeeDetail();
  }

  setValue() {
    const employee = this.employeeData;
    this.dropdownSetting();
    console.log(employee, 'employe');
    const employeeInfo = employee.EmployeeInfo;
    this.employeeAddressId = employee.EmployeeInfo.EmployeeAddressId;
    if (employee.EmployeeRoles !== null) {
      this.dropdownSetting();
      this.selectedRole = employee.EmployeeRoles;
      this.employeeRole = employee.EmployeeRoles?.map(item => {
        return {
          item_id: item.Roleid,
          item_text: item.RoleName
        };
      });
    }
    if (employee.EmployeeLocations !== null) {
      this.employeeLocation = employee.EmployeeLocations.map(item => {
        return {
          item_id: item.LocationId,
          item_text: item.LocationName
        };
      });
    }
    this.employeeDetailId = employeeInfo.EmployeeDetailId;
    this.selectedLocation = employee.EmployeeLocations;
    this.immigrationChange(employeeInfo.ImmigrationStatus);
    this.personalform.patchValue({
      firstName: employeeInfo.Firstname ? employeeInfo.Firstname : '',
      lastName: employeeInfo.LastName ? employeeInfo.LastName : '',
      gender: employeeInfo.Gender ? employeeInfo.Gender : '',
      address: employeeInfo.Address1 ? employeeInfo.Address1 : '',
      mobile: employeeInfo.PhoneNumber ? employeeInfo.PhoneNumber : '',
      immigrationStatus: employeeInfo.ImmigrationStatus ? employeeInfo.ImmigrationStatus : '',
      ssn: employeeInfo.SSNo ? employeeInfo.SSNo : '',
      alienNumber: employeeInfo.AlienNo ? employeeInfo.AlienNo : '',
      permitDate: employeeInfo.WorkPermit ? moment(employeeInfo.WorkPermit).toDate() : '',
    });
    this.emplistform.patchValue({
      emailId: employeeInfo.Email ? employeeInfo.Email : '',
      password: [''],
      dateOfHire: employeeInfo.HiredDate ? moment(employeeInfo.HiredDate).toDate() : '',
      hourlyRateWash: employeeInfo.WashRate,
      hourlyRateDetail: employeeInfo.DetailRate ? employeeInfo.DetailRate : '',
      comType: employeeInfo.ComType ? employeeInfo.ComType : '',
      comRate: employeeInfo.ComRate ? employeeInfo.ComRate : '',
      status: employeeInfo.Status ? 'Active' : 'InActive',
      tip: employeeInfo.Tip ? employeeInfo.Tip : '',
      exemptions: employeeInfo.Exemptions ? employeeInfo.Exemptions : '',
      roles: this.employeeRole,
      location: this.employeeLocation
    });
    this.getCtype(employeeInfo.ComType);
    if (this.actionType === 'view') {
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
  }

  locationDropDown() {
    this.location = this.location.map(item => {
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
    this.employeeService.getAllCollision(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        if (employeesCollison.Collision.length > 0) {
          this.employeeCollision = employeesCollison.Collision;
        }
      }
    });
  }

  backToGrid() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  navigatePage() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
        this.documentList = document.GetAllDocuments;
      }
    });
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.emplistform.controls;
  }

  onRoleDeSelect(event) {
    if (event.item_id === +this.roleId) {
      this.employeeRole = this.employeeRole.filter(item => item.item_id !== event.item_id);
      this.employeeRole.push(event);
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Cuurent logged in role cannot be removed' });
      this.emplistform.patchValue({
        roles: this.employeeRole
      });
    } else {
      this.deSelectRole.push(event);
      if (event.item_text === 'Detailer') {
        this.isRequired = false;
        this.emplistform.get('comType').clearValidators();
        this.emplistform.get('comType').updateValueAndValidity();
      }
    }

  }

  onLocationDeSelect(event) {
    if (event.item_id === +this.locationId) {
      this.employeeLocation = this.employeeLocation.filter(item => item.item_id !== event.item_id);
      this.employeeLocation.push(event);
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Cuurent logged in location cannot be removed' });
      this.emplistform.patchValue({
        location: this.employeeLocation
      });
    } else {
      this.deSelectLocation.push(event);
    }

  }

  updateEmployee() {
    this.submitted = true;
    if (this.personalform.invalid || this.emplistform.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRoles = [];
    const employeeAddressObj = {
      employeeAddressId: this.employeeAddressId,
      employeeId: this.employeeId,
      address1: this.personalform.value.address,
      address2: 'string',
      phoneNumber: this.personalform.value.mobile,
      phoneNumber2: '',
      email: this.emplistform.value.emailId,
      city: 303,
      state: 48,
      zip: 'string',
      country: 38
    };
    const newlyAddedRole = [];
    this.emplistform.value.roles.forEach(item => {
      const isData = _.where(this.selectedRole, { Roleid: item.item_id });
      if (isData.length === 0) {
        newlyAddedRole.push({
          employeeRoleId: 0,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: false
        });
      } else {
        newlyAddedRole.push({
          employeeRoleId: isData[0].EmployeeRoleId,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: false
        });
      }
    });
    this.deSelectRole.forEach(item => {
      const isData = _.where(this.selectedRole, { Roleid: item.item_id });
      if (isData.length !== 0) {
        newlyAddedRole.push({
          employeeRoleId: isData[0].EmployeeRoleId,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: true
        });
      }
    });
    const employeeDetailObj = {
      employeeDetailId: this.employeeDetailId,
      employeeId: this.employeeId,
      employeeCode: 'string',
      hiredDate: moment(this.emplistform.value.dateOfHire).format('YYYY-MM-DD'),
      WashRate: +this.emplistform.value.hourlyRateWash,
      DetailRate: +this.emplistform.value.hourlyRateDetail,
      ComRate: +this.emplistform.value.comRate,
      ComType: +this.emplistform.value.comType,
      lrt: '2020 - 08 - 06T19: 24: 48.817Z',
      exemptions: +this.emplistform.value.exemptions,
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      isDeleted: false,
    };
    const newlyAddedLocation = [];
    this.emplistform.value.location.forEach(item => {
      const isData = _.where(this.selectedLocation, { LocationId: item.item_id });
      if (isData.length === 0) {
        newlyAddedLocation.push({
          employeeLocationId: 0,
          employeeId: this.employeeId,
          locationId: item.item_id,   // LocationId
          isActive: true,
          isDeleted: false,
        });
      } else {
        newlyAddedLocation.push({
          employeeLocationId: isData[0].EmployeeLocationId,
          employeeId: this.employeeId,
          locationId: item.item_id,   // LocationId
          isActive: true,
          isDeleted: false,
        });
      }
    });
    this.deSelectLocation.forEach(item => {
      const isData = _.where(this.selectedLocation, { LocationId: item.item_id });
      if (isData.length !== 0) {
        newlyAddedLocation.push({
          employeeLocationId: isData[0].EmployeeLocationId,
          employeeId: this.employeeId,
          locationId: item.item_id,
          isActive: true,
          isDeleted: true
        });
      }
    });
    const employeeObj = {
      employeeId: this.employeeId,
      firstName: this.personalform.value.firstName,
      middleName: 'string',
      lastName: this.personalform.value.lastName,
      gender: +this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: 117,
      isCitizen: this.isCitizen,
      alienNo: this.isAlien ? this.personalform.value.alienNumber : '',
      birthDate: '',
      workPermit: this.isDate ? this.personalform.value.permitDate : '',
      immigrationStatus: +this.personalform.value.immigrationStatus,
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      isDeleted: false,
    };
    const finalObj = {
      employee: employeeObj,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeeAddressObj,
      employeeRole: newlyAddedRole,
      employeeLocation: newlyAddedLocation,
      employeeDocument: null // this.employeeData.EmployeeDocument
    };
    this.employeeService.updateEmployee(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Employee Updated Successfully!' });
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  pesonalCollapsed() {
    this.isPersonalCollapsed = !this.isPersonalCollapsed;
  }

  detailCollapsed() {
    this.isDetailCollapsed = !this.isDetailCollapsed;
  }

  getCtype(data) {
    const label = this.commissionType.filter(item => item.CodeId === Number(data));
    if (label.length !== 0 && label[0].CodeValue !== 'Hourly Rate') {
      this.ctypeLabel = label[0].CodeValue;
      this.isHourlyRate = false;
    } else if (label.length !== 0 && label[0].CodeValue === 'Hourly Rate') {
      this.ctypeLabel = 'none';
      this.isHourlyRate = true;
    } else {
      this.ctypeLabel = 'none';
      this.isHourlyRate = false;
    }
  }

  onItemSelect(data) {
    if (data.item_text === "Detailer") {
      this.isRequired = true;
      this.emplistform.get('comType').setValidators(Validators.required);
      this.emplistform.get('comType').updateValueAndValidity();
    } else {
      this.isRequired = false;
      this.emplistform.get('comType').clearValidators();
      this.emplistform.get('comType').updateValueAndValidity();
    }
  }

}
