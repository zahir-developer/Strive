import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';

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
  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    this.isEditPersonalDetail = false;
    this.submitted = false;
    this.Status = ['Active', 'InActive'];
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      address: ['', Validators.required],
      mobile: ['', Validators.required],
      immigrationStatus: ['', Validators.required],
      ssn: ['', Validators.required]
    });
    this.emplistform = this.fb.group({
      emailId: ['', Validators.required],
      password: ['', Validators.required],
      dateOfHire: ['', Validators.required],
      hourlyRateWash: ['', Validators.required],
      hourlyRateDetail: [''],
      commission: [''],
      status: [''],
      tip: [''],
      exemptions: [''],
      roles: [[]],
      location: [[]],
      employeeCode: ['']
    });
    this.employeRole();
    this.locationDropDown();
    this.employeeDetail();
    this.getAllCollision();
    // this.getAllDocument();
  }

  employeeDetail() {
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
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        this.employeeData = employees.Employee;
        this.setValue();
      }
    });
  }

  setValue() {
    let employeeRole = [];
    const employee = this.employeeData;
    this.employeRole();
    this.locationDropDown();
    console.log(employee, 'employe');
    const employeeInfo = employee.EmployeeInfo;
    this.selectedLocation = employee.EmployeeLocations;
    if (employee.EmployeeRoles !== null) {
      this.selectedRole = employee.EmployeeRoles;
      employeeRole = employee.EmployeeRoles?.map(item => {
        return {
          item_id: item.Roleid,
          item_text: item.RoleName
        };
      });
    }
    const locationId = employee.EmployeeLocations.map(item => {
      return {
        item_id: item.LocationId,
        item_text: item.LocationName
      };
    });
    this.employeeDetailId = employeeInfo.EmployeeDetailId;
    this.personalform.patchValue({
      firstName: employeeInfo.Firstname ? employeeInfo.Firstname : '',
      lastName: employeeInfo.LastName ? employeeInfo.LastName : '',
      gender: employeeInfo.Gender ? employeeInfo.Gender : '',
      address: employeeInfo.Address1 ? employeeInfo.Address1 : '',
      mobile: employeeInfo.PhoneNumber ? employeeInfo.PhoneNumber : '',
      immigrationStatus: employeeInfo.ImmigrationStatus ? employeeInfo.ImmigrationStatus : '',
      ssn: employeeInfo.SSNo ? employeeInfo.SSNo : '',
    });
    this.emplistform.patchValue({
      emailId: employeeInfo.Email ? employeeInfo.Email : '',
      password: [''],
      dateOfHire: employeeInfo.HiredDate ? moment(employeeInfo.HiredDate).toDate() : '',
      hourlyRateWash: employeeInfo.PayRate,
      hourlyRateDetail: employeeInfo.ComRate,
      commission: employeeInfo.ComRate ? employeeInfo.ComRate : '',
      status: employeeInfo.Status ? 'Active' : 'InActive',
      tip: employeeInfo.Tip ? employeeInfo.Tip : '',
      exemptions: employeeInfo.Exemptions ? employeeInfo.Exemptions : '',
      roles: employeeRole,
      location: locationId
    });
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
      employeeAddressId: 0,
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
    this.emplistform.value.roles.forEach( role => {
      this.selectedRole.forEach( item => {
        if (+item.Roleid !== role.item_id) {
          newlyAddedRole.push(role);
        }
      });
    });
    const employeeRoleObj = newlyAddedRole.map(item => {
      return {
        employeeRoleId: item.item_id,
        employeeId: this.employeeId,
        roleId: item.item_id
      };
    });
    const employeeDetailObj = {
      employeeDetailId: this.employeeDetailId,
      employeeId: this.employeeId,
      employeeCode: 'string',
      hiredDate: moment(this.emplistform.value.dateOfHire).format('YYYY-MM-DD'),
      PayRate: this.emplistform.value.hourlyRateWash,
      ComRate: this.emplistform.value.hourlyRateDetail,
      lrt: '2020 - 08 - 06T19: 24: 48.817Z',
      exemptions: +this.emplistform.value.exemptions,
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      isDeleted: false,
    };
    const locationObj = this.emplistform.value.location.map(item => {
      return {
        employeeLocationId: item.item_id,
        employeeId: this.employeeId,
        locationId: item.item_id
      };
    });
    const employeeObj = {
      employeeId: this.employeeId,
      firstName: this.personalform.value.firstName,
      middleName: 'string',
      lastName: this.personalform.value.lastName,
      gender: +this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: 117,
      isCitizen: true,
      alienNo: 'string',
      birthDate: '',
      immigrationStatus: this.personalform.value.immigrationStatus,
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      isDeleted: false,
    };
    const finalObj = {
      employee: employeeObj,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeeAddressObj,
      employeeRole: employeeRoleObj,
      employeeLocation: locationObj,
      employeeDocument: null // this.employeeData.EmployeeDocument
    };
    this.employeeService.updateEmployee(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: ' Employee Updated Successfull!' });
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


}
