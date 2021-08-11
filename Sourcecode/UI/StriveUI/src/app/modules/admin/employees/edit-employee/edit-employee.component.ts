import { Component, OnInit, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import * as _ from 'underscore';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html'
})
export class EditEmployeeComponent implements OnInit {
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  employeeRoles?: any;
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeId?: any;
  @Input() employeeDetailId?: any;
  location?: any;
  Status: any;
  @Input() commissionType: any;
  @Input() actionType: string;
  gender: any;
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
  authId: any;
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  State: any;
  city: any;
  selectedStateId: any;
  selectedCityId: any;
  isChecked: boolean;
  hourlyLocationId = '';
  locationRate = '';
  locationRateList = [];
  locationList = [];
  isHourEdit = 0;
  selectedLocationHour = '';
  isRateAllLocation: boolean;
  errorMessage: boolean;
  dellocationRateList = [];
  constructor(
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private toastr: ToastrService,
    private getCode: GetCodeService,
    private codeValueService: CodeValueService
  ) { }

  ngOnInit(): void {
    this.ctypeLabel = 'none';
    this.isEditPersonalDetail = false;
    this.submitted = false;
    this.Status = ['Active', 'Inactive'];
    this.isRateAllLocation = false;
    this.errorMessage = false;
    this.getImmigrationStatus();
    this.getGenderDropdownValue();
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      address: ['', Validators.required],
      mobile: ['', Validators.required],
      Zip: [''],
      immigrationStatus: ['', Validators.required],
      ssn: [''],
      alienNumber: [''],
      permitDate: [''],
      Tips: ['']
    });
    this.emplistform = this.fb.group({
      emailId: ['', [Validators.required, Validators.email]],
      dateOfHire: ['', Validators.required],
      hourlyRateWash: [''],
      hourlyRateDetail: [''],
      comType: [''],
      comRate: [''],
      status: [''],
      tip: [''],
      exemptions: [''],
      roles: [[]],
      location: [[]],
      employeeCode: [''],
      salary: ['']
    });
    this.roleId = localStorage.getItem('roleId');
    this.locationId = localStorage.getItem('empLocationId');
    this.getAllRoles();
    this.getLocation();
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

  getImmigrationStatus() {
    const imigirationStatusVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.immigrationStatus);
    console.log(imigirationStatusVaue, 'cached value ');
    // if (imigirationStatusVaue.length > 0) {
    //   this.imigirationStatus = imigirationStatusVaue;
    // } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.immigrationStatus).subscribe(data => {
        if (data.status === "Success") {
          const cType = JSON.parse(data.resultData);
          this.imigirationStatus = cType.Codes;
          this.dropdownSetting();
          this.employeeDetail();
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    // }
  }

  getGenderDropdownValue() {
    const genderVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.gender);
    console.log(genderVaue, 'cached Value');
    if (genderVaue.length > 0) {
      this.gender = genderVaue;
    } else {
      this.employeeService.getDropdownValue('GENDER').subscribe(res => {
        if (res.status === 'Success') {
          const gender = JSON.parse(res.resultData);
          this.gender = gender.Codes;
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }
  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  selectCity(event) {
    this.city = event;
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
  change(data) {
    if (data === true) {
      this.isChecked = true;
    } else {
      this.isChecked = false;
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
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  reloadCollisionGrid() {
    this.employeeDetail();
  }

  setValue() {
    const employee = this.employeeData;
    this.dellocationRateList = [];
    this.dropdownSetting();
    const employeeInfo = employee.EmployeeInfo;
    this.selectedStateId = employeeInfo?.State;
    this.stateDropdownComponent.selectedStateId = this.selectedStateId;
    this.stateDropdownComponent.setValue();
    this.State = this.selectedStateId;
    this.selectedCityId = employeeInfo?.City;
    this.cityComponent.selectedCityId = this.selectedCityId;
    this.cityComponent.getCity(this.selectedStateId);
    this.cityComponent.isView = this.actionType === 'view' ? true : false;
    this.stateDropdownComponent.isView = this.actionType === 'view' ? true : false;
    this.city = this.selectedCityId;
    this.employeeAddressId = employee.EmployeeInfo.EmployeeAddressId;
    this.authId = employee.EmployeeInfo.AuthId;
    if (employee.EmployeeRoles !== null) {
      this.dropdownSetting();
      this.selectedRole = employee.EmployeeRoles;
      this.employeeRole = employee.EmployeeRoles?.map(item => {
        return {
          item_id: item.RoleId,
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
      this.locationList = this.employeeLocation.map(x => Object.assign({}, x));
    }
    
    if (employee.EmployeeHourlyRate !== null) {
      const locationHourlyWash = [];
      employee.EmployeeHourlyRate.forEach(item => {
        locationHourlyWash.push({
          locationId: item.LocationId,
          locationName: item.LocationName,
          ratePerHour: item.HourlyRate,
          roleName: item.RoleName,
          employeeId: item.EmployeeId,
          employeeHourlyRateId: item.EmployeeHourlyRateId
        });
      });
      this.locationRateList = locationHourlyWash;
    }
    if (this.locationRateList.length === this.employeeLocation.length) {
      this.locationList = [];
    } else {
      const unselectedLoc = [];
      this.locationList.forEach( loc => {
          const location = this.locationRateList.filter( item =>  item.locationId === loc.item_id );
          if (location.length === 0) {
            unselectedLoc.push(loc);
          }
      });
      this.locationList = unselectedLoc;
    }
    // locationId: loc[0].item_id,
    //     locationName: loc[0].item_text,
    //     ratePerHour: this.locationRate

    this.employeeDetailId = employeeInfo.EmployeeDetailId;
    this.selectedLocation = employee?.EmployeeLocations;
    this.immigrationChange(employeeInfo.ImmigrationStatus);
    this.personalform.patchValue({
      firstName: employeeInfo.Firstname ? employeeInfo.Firstname : '',
      lastName: employeeInfo.LastName ? employeeInfo.LastName : '',
      gender: employeeInfo.Gender ? employeeInfo.Gender : '',
      address: employeeInfo.Address1 ? employeeInfo.Address1 : '',
      mobile: employeeInfo.PhoneNumber ? employeeInfo.PhoneNumber : '',
      Zip: employeeInfo.Zip ? employeeInfo.Zip : '',
      immigrationStatus: employeeInfo.ImmigrationStatus ? employeeInfo.ImmigrationStatus : '',
      ssn: employeeInfo.SSNo ? employeeInfo.SSNo : '',
      Tips: employeeInfo?.Tips,
      alienNumber: employeeInfo.AlienNo ? employeeInfo.AlienNo : '',
      permitDate: employeeInfo.WorkPermit ? moment(employeeInfo.WorkPermit).toDate() : '',
    });
    this.emplistform.patchValue({
      emailId: employeeInfo.Email ? employeeInfo.Email : '',
      password: [''],
      dateOfHire: employeeInfo.HiredDate ? moment(employeeInfo.HiredDate).toDate() : '',
      hourlyRateWash: employeeInfo.WashRate ? Number(employeeInfo.WashRate).toFixed(2) : '',
      comType: employeeInfo.ComType ? employeeInfo.ComType : '',
      comRate: employeeInfo.ComRate ? Number(employeeInfo.ComRate).toFixed(2) : '',
      status: employeeInfo.Status ? 'Active' : 'Inactive',
      tip: employeeInfo.Tip ? employeeInfo.Tip : '',
      exemptions: employeeInfo.Exemptions ? employeeInfo.Exemptions : '',
      salary: employeeInfo.Salary ? employeeInfo.Salary : '',
      roles: this.employeeRole,
      location: this.employeeLocation
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
    this.dropdownSetting();
  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.employeeRoles = roles.EmployeeRoles.map(item => {
          return {
            item_id: item.RoleMasterId,
            item_text: item.RoleName
          };
        });
        this.dropdownSetting();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') { 
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
        this.location = this.location.map(item => {
          return {
            item_id: item.LocationId,
            item_text: item.LocationName
          };
        });
        this.dropdownSetting();
      }
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
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.emplistform.controls;
  }

  onRoleDeSelect(event) {
    if (event.item_id === +this.roleId && this.employeeId === +localStorage.getItem('empId')) {
      this.employeeRole = this.employeeRole.filter(item => item.item_id !== event.item_id);
      this.employeeRole.push(event);
      this.toastr.warning(MessageConfig.Employee.role, 'Warning!');
      this.emplistform.patchValue({
        roles: this.employeeRole
      });
    } else {
      this.deSelectRole.push(event);
    }

  }

  onLocationDeSelect(event) {
    if (event.item_id === +this.locationId && this.employeeId === +localStorage.getItem('empId')) {
      this.employeeLocation = this.employeeLocation.filter(item => item.item_id !== event.item_id);
      this.employeeLocation.push(event);
      this.toastr.warning(MessageConfig.Employee.location, 'Warning!');
      this.emplistform.patchValue({
        location: this.employeeLocation
      });
    } else {
      this.locationList = this.locationList.filter( item => item.item_id !== event.item_id);
      this.deSelectLocation.push(event);
    }

  }

  onSelectedLocation(event) {
    this.locationList.push(event);
  }

  updateEmployee() {
    this.submitted = true;
    this.stateDropdownComponent.submitted = true;
    this.cityComponent.submitted = true;
    if (this.stateDropdownComponent.stateValueSelection == false) {
      return;
    }
    if (this.cityComponent.selectValueCity == false) {
      return;
    }
    if (this.personalform.invalid || this.emplistform.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
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
      address2: null,
      phoneNumber: this.personalform.value.mobile,
      phoneNumber2: null,
      email: this.emplistform.value.emailId,
      city: this.city,
      state: this.State,
      zip: this.personalform.value.Zip,
      country: null
    };
    const newlyAddedRole = [];
    this.emplistform.value.roles.forEach(item => {
      const isData = _.where(this.selectedRole, { RoleId: item.item_id });
      if (isData.length === 0) {
        newlyAddedRole.push({
          employeeRoleId: 0,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: false,
          roleName: item.item_text
        });
      } else {
        newlyAddedRole.push({
          employeeRoleId: isData[0].EmployeeRoleId,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: false,
          roleName: item.item_text
        });
      }
    });
    this.deSelectRole.forEach(item => {
      const isData = _.where(this.selectedRole, { RoleId: item.item_id });
      if (isData.length !== 0) {
        newlyAddedRole.push({
          employeeRoleId: isData[0].EmployeeRoleId,
          employeeId: this.employeeId,
          roleId: item.item_id,
          isActive: true,
          isDeleted: true,
          roleName: item.item_text
        });
      }
    });
    const employeeDetailObj = {
      employeeDetailId: this.employeeDetailId,
      employeeId: this.employeeId,
      employeeCode: null,
      authId: this.authId,
      hiredDate: moment(this.emplistform.value.dateOfHire).format('YYYY-MM-DD'),
      WashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : 0,
      DetailRate: null,
      ComRate: +this.emplistform.value.comRate,
      ComType: +this.emplistform.value.comType,
      lrt: null,
      exemptions: +this.emplistform.value.exemptions,
      salary: +this.emplistform.value.salary,
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
          locationId: item.item_id,
          isActive: true,
          isDeleted: false,
          hourlyWashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : 0,
          locationName: item.item_text
        });
      } else {
        newlyAddedLocation.push({
          employeeLocationId: isData[0].EmployeeLocationId,
          employeeId: this.employeeId,
          locationId: item.item_id,
          isActive: true,
          isDeleted: false,
          hourlyWashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : 0,
          locationName: item.item_text
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
          isDeleted: true,
          hourlyWashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : 0,
          locationName: item.item_text
        });
      }
    });
    const employeeObj = {
      Tips: this.isChecked ? this.isChecked : null,
      employeeId: this.employeeId,
      firstName: this.personalform.value.firstName,
      middleName: null,
      lastName: this.personalform.value.lastName,
      gender: +this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: null,
      isCitizen: this.isCitizen,
      alienNo: this.isAlien ? this.personalform.value.alienNumber : '',
      birthDate: null,
      workPermit: this.isDate ? this.personalform.value.permitDate : '',
      immigrationStatus: +this.personalform.value.immigrationStatus,
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      isDeleted: false,
    };
    const locHour = [];
    this.locationRateList.forEach(item => {
      locHour.push({
        employeeHourlyRateId: item.employeeHourlyRateId,
        employeeId: item.employeeId,
        roleId: null,
        locationId: item.locationId,
        hourlyRate: item.ratePerHour,
        isActive: true,
        isDeleted: false,
      });
    });
    this.dellocationRateList.forEach( item => {
      locHour.push(item);
    });
    const finalObj = {
      employee: employeeObj,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeeAddressObj,
      employeeRole: newlyAddedRole,
      employeeLocation: newlyAddedLocation,
      employeeDocument: null,
      employeeHourlyRate: locHour
    };
    this.spinner.show();
    this.employeeService.updateEmployee(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Employee.Update, 'Success!');
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  pesonalCollapsed() {
    this.isPersonalCollapsed = !this.isPersonalCollapsed;
  }

  detailCollapsed() {
    this.isDetailCollapsed = !this.isDetailCollapsed;
  }

  deleteLocationHour(loc) {
    if (loc.employeeHourlyRate !== 0) {
      this.dellocationRateList.push({
        employeeHourlyRateId: loc.employeeHourlyRateId,
        employeeId: loc.employeeId,
        roleId: null,
        locationId: loc.locationId,
        hourlyRate: loc.ratePerHour,
        isActive: true,
        isDeleted: true,
      });
    }
    this.locationRateList = this.locationRateList.filter(item => item.locationId !== loc.locationId);
    this.locationList.unshift({
      item_id: loc.locationId,
      item_text: loc.locationName
    });
    this.locationList = _.sortBy(this.locationList, 'item_id');
  }

  editLocationHour(loc) {
    this.isHourEdit = loc.locationId;
    this.selectedLocationHour = loc.ratePerHour;
  }

  submit(loc) {
    if (+loc.ratePerHour === 0) {
      this.errorMessage = true;
      // this.toastr.info(MessageConfig.Employee.hourlyRate, 'Information!');
      return;
    } else {
      this.errorMessage = false;
    }
    this.isHourEdit = 0;
    this.selectedLocationHour = '';
  }

  cancelHour(loc) {
    this.isHourEdit = 0;
    loc.ratePerHour = this.selectedLocationHour;
  }

  addRate() {
    if (+this.locationRate === 0) {
      this.errorMessage = true;
      // this.toastr.info(MessageConfig.Employee.hourlyRate, 'Information!');
      return;
    } else {
      this.errorMessage = false;
    }
    if (this.isRateAllLocation) {
      this.locationList.forEach(item => {
        this.locationRateList.push({
          locationId: item.item_id,
          locationName: item.item_text,
          ratePerHour: this.locationRate,
          employeeId: this.employeeId,
          employeeHourlyRateId: 0
        });
      });
      this.locationList = [];
      this.locationRate = '';
      this.hourlyLocationId = '';
    } else {
      const loc = _.where(this.locationList, { item_id: +this.hourlyLocationId });
      if (loc.length > 0) {
        this.locationList = this.locationList.filter(item => item.item_id !== +this.hourlyLocationId);
        const locName = loc[0].item_text;
        this.locationRateList.push({
          locationId: loc[0].item_id,
          locationName: loc[0].item_text,
          ratePerHour: this.locationRate,
          employeeId: this.employeeId,
          employeeHourlyRateId: 0
        });
        this.hourlyLocationId = '';
        this.locationRate = '';
      }
    }
  }

  rateAllLocation(event) {
    this.isRateAllLocation = event.target.checked;
  }


}
