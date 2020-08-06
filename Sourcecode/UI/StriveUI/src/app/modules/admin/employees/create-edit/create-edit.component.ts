import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
declare var $: any;
@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit {
  sampleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() employeeRoles?: any = [];
  @Input() gender?: any;
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeId?: any;
  @Input() location?: any;
  documentDailog: boolean;
  documentForm: FormGroup;
  fileName: any = '';
  fileUploadformData: any;
  mulitselected: any;
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
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private messageService: MessageServiceToastr) { }

  ngOnInit() {
    this.ctypeLabel = 'none';
    this.Status = ['Active', 'InActive'];
    this.documentDailog = false;
    this.submitted = false;
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
      roles: [''],
      location: ['']
    });
    this.documentForm = this.fb.group({
      password: ['', Validators.required]
    });
    this.employeRole();
    this.locationDropDown();
    // this.employeeDetail();
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
        }
      }
    });
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
    this.location = this.location.map(item => {
      return {
        item_id: item.LocationId,
        item_text: item.LocationName
      };
    });
  }

  checking() {
    console.log(this.emplistform, 'multi');
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      console.log(res, 'getAllRoles');
    });
  }

  upload() {
    this.documentDailog = true;
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('filepaths');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;
      let fileReader: any;
      fileReader = new FileReader();
      fileReader.onload = function (fileLoadedEventTigger) {
        let textAreaFileContents: any;
        textAreaFileContents = document.getElementById('filepaths');
        textAreaFileContents.innerHTML = fileLoadedEventTigger.target.result;
      };
      fileReader.readAsDataURL(fileToLoad);
      setTimeout(() => {
        let fileTosaveName: any;
        fileTosaveName = fileReader.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
      }, 5000);
    }
  }

  closeDocumentPopup() {
    this.documentDailog = false;
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.emplistform.controls;
  }

  saveEmployee() {
    console.log(this.emplistform, 'empdorm');
    this.submitted = true;
    if (this.personalform.invalid || this.emplistform.invalid) {
      return;
    }
    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRoles = [];
    const employeeAddressObj = {
      employeeAddressId: 0,
      relationshipId: 0,
      address1: this.personalform.value.address,
      address2: '',
      phoneNumber: +this.personalform.value.mobile,
      phoneNumber2: '',
      email: this.emplistform.value.emailId,
      state: 0,
      country: 0,
      zip: '',
      city: 1,
      isActive: this.emplistform.value.status === 'Active' ? true : false
    };
    const employeeRoleObj = this.emplistform.value.roles.map(item => {
      return {
        employeeRoleId: 0,
        employeeId: 0,
        roleId: item.item_id,
        isDefault: true,
        isActive: this.emplistform.value.status === 'Active' ? true : false
      };
    });
    const employeeDetailObj = this.emplistform.value.location.map(item => {
      return {
        employeeDetailId: 0,
        employeeId: 0,
        employeeCode: 'string',
        authId: 0,
        locationId: item.item_id,
        payRate: 'string',
        sickRate: 'string',
        vacRate: 'string',
        comRate: 'string',
        hiredDate: this.emplistform.value.dateOfHire,
        salary: 'string',
        tip: this.emplistform.value.tip,
        lrt: '2020 - 08 - 03T10: 00: 31.411Z',
        exemptions: +this.emplistform.value.exemptions,
        isActive: this.emplistform.value.status === 'Active' ? true : false
      };
    });
    employeAddress.push(employeeAddressObj);
    const employeeObj = {
      employeeId: 0,
      firstName: this.personalform.value.firstName,
      middleName: 'string',
      lastName: this.personalform.value.lastName,
      gender: this.personalform.value.gender,
      ssNo: this.personalform.value.ssn,
      maritalStatus: 0,
      isCitizen: true,
      alienNo: 'string',
      birthDate: '2020-08-03T10:00:31.412Z',
      immigrationStatus: this.personalform.value.immigrationStatus,
      createdDate: '2020-08-03T10:00:31.412Z',
      isActive: this.emplistform.value.status === 'Active' ? true : false,
      employeeDetail: employeeDetailObj,
      employeeAddress: employeAddress,
      employeeRole: employeeRoleObj
    };
    console.log(employeeObj, 'finalObj');
    this.employeeService.updateEmployee(employeeObj).subscribe( res => {
      if (res.status === 'Success') {
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
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

  getCtype(data) {
    const label = this.commissionType.filter(item => item.CodeId === Number(data));
    if (label.length !== 0) {
      this.ctypeLabel = label[0].CodeValue;
    } else {
      this.ctypeLabel = 'none';
    }
  }
}
