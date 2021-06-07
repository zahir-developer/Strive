import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import * as _ from 'underscore';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

declare var $: any;
@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html'
})
export class CreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  State: any;
  city: any;
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  sampleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  employeeRoles?: any = [];
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeId?: any;
  location?: any;
  documentDailog: boolean;
  documentForm: FormGroup;
  fileName: any = '';
  fileUploadformData: any;
  mulitselected: any;
  gender: any;
  Status: any;
  @Input() commissionType: any;
  @Input() actionType: string;
  employeeData: any;
  personalform: FormGroup;
  emplistform: FormGroup;
  dropdownSettings: IDropdownSettings = {};
  datePickerConfig = {
    format: 'DD-MM-YYYY',
    showTwentyFourHours: true
  };
  isPersonalCollapsed = false;
  isDetailCollapsed = false;
  isDocumentCollapsed = false;
  submitted: boolean;
  ctypeLabel: any;
  multipleFileUpload: any = [];
  fileType: any;
  isLoading: boolean;
  isAlien: boolean = false;
  isDate: boolean = false;
  imigirationStatus: any = [];
  isCitizen: boolean = true;
  isHourlyRate: boolean = false;
  isRequired: boolean = false;
  isChecked: boolean;
  locationId = '';
  locationRate = '';
  locationRateList = [];
  locationList = [];
  isHourEdit = 0;
  selectedLocationHour = '';
  isRateAllLocation: boolean;
  errorMessage: boolean;
  fileSize: number;
  isEmailExists: boolean;
  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private getCode: GetCodeService,
    private codeValueService: CodeValueService
  ) { }

  ngOnInit() {
    this.isLoading = false;
    this.ctypeLabel = 'none';
    this.Status = ['Active', 'Inactive'];
    this.isRateAllLocation = false;
    this.errorMessage = false;
    this.fileSize = ApplicationConfig.UploadSize.EmployeeDocument;
    this.isEmailExists = false;
    this.getGenderDropdownValue();
    this.getAllRoles();
    this.getLocation();
    this.getImmigrationStatus();
    this.documentDailog = false;
    this.submitted = false;
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: [''],
      address: ['', Validators.required],
      mobile: ['', Validators.required],
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
      status: ['Active'],
      exemptions: [''],
      roles: [[]],
      location: [[]]

    });
    this.emplistform.controls.status.disable();
    this.documentForm = this.fb.group({
      password: ['', Validators.required]
    });
  }

  getImmigrationStatus() {
    const imigirationStatusVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.immigrationStatus);
    console.log(imigirationStatusVaue, 'cached value ');
    if (imigirationStatusVaue.length > 0) {
      this.imigirationStatus = imigirationStatusVaue;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.immigrationStatus).subscribe(data => {
        if (data.status === "Success") {
          const cType = JSON.parse(data.resultData);
          this.imigirationStatus = cType.Codes;
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
        , (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
    }
  }

  employeeDetail() {
    const id = this.employeeId;
    this.employeeService.getEmployeeDetail(id).subscribe(res => {
      if (res.status === 'Success') {
        const employees = JSON.parse(res.resultData);
        if (employees.EmployeeDetail.length > 0) {
          this.employeeData = employees.EmployeeDetail[0];
        }
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  dropDownSetting() {
    this.dropdownSettings = ApplicationConfig.dropdownSettings;
  }

  selectCity(event) {
    this.city = event;
  }
  employeRole() {
    this.employeeRoles = this.employeeRoles.map(item => {
      return {
        item_id: item.CodeId,
        item_text: item.CodeValue
      };
    });
    this.dropDownSetting();
  }

  locationDropDown() {
    this.location = this.location.map(item => {
      return {
        item_id: item.LocationId,
        item_text: item.LocationName
      };
    });
    this.dropDownSetting();
  }
  change(data) {

    if (data === true) {
      this.isChecked = true;

    }
    else {
      this.isChecked = false;

    }
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

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
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
        this.dropDownSetting();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
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
        // this.locationList = this.location.map(x => Object.assign({}, x));
        this.dropDownSetting();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  selectedLocation(event) {
    this.locationList.push(event);
  }

  deselectLocation(event) {
    this.locationList = this.locationList.filter(item => item.item_id !== event.item_id);
  }

  upload() {
    this.documentDailog = true;
  }

  fileNameChanged(e: any) {
    this.isLoading = true;
    try {
      const file = e.target.files[0];
      const fileSize = + file.size;
      const localFileKbSize = fileSize / Math.pow(1024, 1);
      const localFileKbRoundSize = +localFileKbSize.toFixed();
      if (this.fileSize < localFileKbRoundSize) {
        this.toastr.warning(MessageConfig.Admin.SystemSetup.EmployeeHandBook.FileSize, 'Warning!');
        this.isLoading = false;
        return;
      }
      // const sizeFixed = (fileSize / 10240);
      // const sizeFixedValue = +sizeFixed.toFixed(1);
      // if (sizeFixedValue > 1) {
      //   this.toastr.warning(MessageConfig.Document.fileSize, 'Warning!');
      //   this.isLoading = false;
      //   return;
      // }
      const fReader = new FileReader();
      fReader.readAsDataURL(file);
      fReader.onloadend = (event: any) => {
        this.fileName = file.name;
        const fileExtension = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
        let fileTosaveName: any;
        fileTosaveName = event.target.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
        const fileObj = {
          fileName: this.fileName,
          fileUploadDate: this.fileUploadformData,
          fileType: fileExtension
        };
        this.multipleFileUpload.push(fileObj);
        this.isLoading = false;
      };
    } catch (error) {
      this.fileName = null;
      this.fileUploadformData = null;
      this.isLoading = false;
    }
  }

  clearDocument(i) {
    this.multipleFileUpload = this.multipleFileUpload.filter((item, index) => index !== i);
  }

  closeDocumentPopup() {
    this.documentDailog = false;
  }

  onBlurMethod() {
    $('.custom-file-input').on('change', function () {
      const fileName = $(this).val().split('\\').pop();
      $(this).siblings('.custom-file-label').addClass('selected').html(fileName);
    });
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.emplistform.controls;
  }

  saveEmployee() {
    this.emplistform.controls.status.enable();
    this.submitted = true;
    this.stateDropdownComponent.submitted = true;
    this.cityComponent.submitted = true;
    if (this.stateDropdownComponent.stateValueSelection === false) {
      return;
    }
    if (this.cityComponent.selectValueCity === false) {
      return;
    }

    if (this.personalform.invalid || this.emplistform.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }

    if (this.locationRateList.length === 0) {
      this.toastr.warning(MessageConfig.Employee.hourlyEmployeeLocation, 'Warning!');
      return;
    }

    if (this.isEmailExists) {
      return;
    }

    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRoles = [];
    const employeeAddressObj = {
      employeeAddressId: 0,
      employeeId: 0,
      address1: this.personalform.value.address,
      address2: null,
      phoneNumber: this.personalform.value.mobile,
      phoneNumber2: null,
      email: this.emplistform.value.emailId,
      city: this.city,
      state: this.State,
      zip: null,
      country: null
    };
    const employeeRoleObj = this.emplistform.value.roles.map(item => {
      return {
        employeeRoleId: 0,
        employeeId: 0,
        roleId: item.item_id,
        isActive: true,
        isDeleted: false,
        roleName: item.item_text
      };
    });
    const employeeDetailObj = {
      employeeDetailId: 0,
      employeeId: 0,
      employeeCode: null,
      hiredDate: moment(this.emplistform.value.dateOfHire).format('YYYY-MM-DD'),
      WashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : null,
      DetailRate: null,
      ComRate: +this.emplistform.value.comRate,
      ComType: +this.emplistform.value.comType,
      lrt: null,
      exemptions: +this.emplistform.value.exemptions,
      isActive: true,
      isDeleted: false,
    };
    const locationObj = this.emplistform.value.location.map(item => {
      return {
        employeeLocationId: 0,
        employeeId: 0,
        locationId: item.item_id,
        isActive: true,
        isDeleted: false,
        hourlyWashRate: +this.emplistform.value.hourlyRateWash ? +this.emplistform.value.hourlyRateWash : null,
        locationName: item.item_text
      };
    });
    const employeeObj = {
      employeeId: 0,
      firstName: this.personalform.value.firstName,
      middleName: null,
      lastName: this.personalform.value.lastName,
      gender: +this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: null,
      isCitizen: this.isCitizen,
      alienNo: this.isAlien ? this.personalform.value.alienNumber : '',
      birthDate: null,
      Tips: this.isChecked ? this.isChecked : null,
      workPermit: this.isDate ? this.personalform.value.permitDate : '',
      immigrationStatus: Number(this.personalform.value.immigrationStatus),
      isActive: true,
      isDeleted: false,
    };
    const documentObj = this.multipleFileUpload.map(item => {
      return {
        employeeDocumentId: 0,
        employeeId: 0,
        filename: item.fileName,
        filepath: null,
        base64: item.fileUploadDate,
        fileType: item.fileType,
        isPasswordProtected: false,
        password: null,
        comments: null,
        isActive: true,
        isDeleted: false,
        createdBy: +localStorage.getItem('empId'),
        createdDate: moment(new Date()).format('YYYY-MM-DD'),
        updatedBy: +localStorage.getItem('empId'),
        updatedDate: moment(new Date()).format('YYYY-MM-DD')
      };
    });
    const locHour = [];
    this.locationRateList.forEach(item => {
      locHour.push({
        employeeHourlyRateId: 0,
        employeeId: 0,
        roleId: null,
        locationId: item.locationId,
        hourlyRate: item.ratePerHour,
        isActive: true,
        isDeleted: false,
      });
    });
    const finalObj = {
      employee: employeeObj,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeeAddressObj,
      employeeRole: employeeRoleObj,
      employeeLocation: locationObj,
      employeeDocument: documentObj,
      employeeHourlyRate: locHour
    };
    this.spinner.show();
    this.employeeService.saveEmployee(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Employee.saved, 'Success');
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {

        if (res.status === 'Fail' && res.errorMessage !== null) {
          this.spinner.hide();

          this.toastr.error(res.errorMessage, 'Error!');
        }
        else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
    }, (error) => {
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

  documentCollapsed() {
    this.isDocumentCollapsed = !this.isDocumentCollapsed;
  }

  navigatePage() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  rateAllLocation(event) {
    this.isRateAllLocation = event.target.checked;
  }

  addRate() {
    if (+this.locationRate === 0) {
      this.errorMessage = true;
      // this.toastr.info(MessageConfig.Employee.hourlyRate , 'Information!');
      return;
    } else {
      this.errorMessage = false;
    }
    console.log(this.isRateAllLocation, 'isRate');
    if (this.isRateAllLocation) {
      this.locationList.forEach(item => {
        this.locationRateList.push({
          locationId: item.item_id,
          locationName: item.item_text,
          ratePerHour: this.locationRate
        });
      });
      this.locationList = [];
      this.locationRate = '';
      this.locationId = '';
    } else {
      const loc = _.where(this.locationList, { item_id: +this.locationId });
      if (loc.length > 0) {
        this.locationList = this.locationList.filter(item => item.item_id !== +this.locationId);
        const locName = loc[0].item_text;
        this.locationRateList.push({
          locationId: loc[0].item_id,
          locationName: loc[0].item_text,
          ratePerHour: this.locationRate
        });
        this.locationId = '';
        this.locationRate = '';
      }
    }
  }

  deleteLocationHour(loc) {
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
      // this.toastr.info(MessageConfig.Employee.hourlyRate , 'Information!');
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

  checkEmailExist() {
    const emilID = this.emplistform.value.emailId;
    this.employeeService.validateEmail(emilID).subscribe( res => {
      if (res.status === 'Success') {
        const emailExist = JSON.parse(res.resultData);
        if (emailExist.EmailIdExist) {
          this.isEmailExists = true;
        } else {
          this.isEmailExists = false;
        }
      }
    });
  }
}
