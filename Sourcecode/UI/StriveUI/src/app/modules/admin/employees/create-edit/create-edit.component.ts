import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { threadId } from 'worker_threads';
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
  @Input() employeeRoles?: any;
  @Input() gender?: any;
  @Input() maritalStatus?: any;
  @Input() state?: any;
  @Input() country?: any;
  @Input() employeeData?: any;
  documentDailog: boolean;
  documentForm: FormGroup;
  fileName: any = '';
  fileUploadformData: any;
  mulitselected: any;
  Status: any;
  @Input() commissionType: any;
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private messageService: MessageServiceToastr) { }

  ngOnInit() {
    this.Status = ['Active', 'InActive'];
    this.documentDailog = false;
    this.sampleForm = this.fb.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      role: ['', Validators.required],
      gender: [''],
      marital: [''],
      ssn: [''],
      dob: [''],
      status: [''],
      exemption: [''],
      hireDate: [''],
      lrtDate: [''],
      tip: [''],
      sick: [''],
      vacation: [''],
      hourly: [''],
      salary: [''],
      commission: [''],
      phone: [''],
      cell: [''],
      email: [''],
      address: [''],
      state: [''],
      country: [''],
      zipcode: ['']
    });
    this.documentForm = this.fb.group({
      password: ['', Validators.required]
    });
    // console.log(this.selectedData);
    console.log(this.gender, 'gender');
    this.employeRole();
    this.setValue();
    this.getAllDocument();
    this.getDocumentById();
    // if (this.selectedData !== undefined && this.selectedData.length !== 0) {
    //   this.sampleForm.reset();
    //   this.sampleForm.setValue({
    //     firstName: this.selectedData.FirstName,
    //     lastName: this.selectedData.LastName, role: this.selectedData.Role
    //   });
    // }
  }

  setValue() {
    if (this.employeeData !== undefined && this.isEdit === true) {
      console.log(this.employeeData, 'data');
      const employee = this.employeeData;
      const employeeDetail = employee.EmployeeDetail[0];
      const employeeAddress = employee.EmployeeAddress[0];
      const employeeRole = employee.EmployeeRoles[0];
      this.sampleForm.patchValue({
        firstName: employee.FirstName ? employee.FirstName : '',
        middleName: employee.MiddleName ? employee.MiddleName : '',
        lastName: employee.LastName ? employee.LastName : '',
        role: employeeRole.RoleId ? employeeRole.RoleId : '',
        gender: employee.Gender ? employee.Gender : '',
        marital: employee.MaritalStatus ? employee.MaritalStatus : '',
        ssn: employee.SSNo ? employee.SSNo : '',
        dob: employee.BirthDate ? employee.BirthDate : '',
        status: employee.IsActive ? 'true' : 'false',
        exemption: employeeDetail.Exemptions ? employeeDetail.Exemptions : '',
        hireDate: employeeDetail.HiredDate ? employeeDetail.HiredDate : '',
        lrtDate: employeeDetail.LRT ? employeeDetail.LRT : '',
        tip: employeeDetail.Tip ? employeeDetail.Tip : '',
        sick: employeeDetail.SickRate ? employeeDetail.SickRate : '',
        vacation: employeeDetail.VacRate ? employeeDetail.VacRate : '',
        hourly: employeeDetail.PayRate ? employeeDetail.PayRate : '',
        salary: employeeDetail.Salary ? employeeDetail.Salary : '',
        commission: employeeDetail.ComRate ? employeeDetail.ComRate : '',
        phone: employeeAddress.PhoneNumber ? employeeAddress.PhoneNumber : '',
        cell: employeeAddress.PhoneNumber2 ? employeeAddress.PhoneNumber2 : '',
        email: employeeAddress.Email ? employeeAddress.Email : '',
        address: employeeAddress.Address1 ? employeeAddress.Address1 : '',
        state: employeeAddress.State ? employeeAddress.State : '',
        country: employeeAddress.Country ? employeeAddress.Country : '',
        zipcode: employeeAddress.Zip ? employeeAddress.Zip : ''
      });
    }
  }

  employeRole() {
    this.employeeRoles = this.employeeRoles.map(item => {
      return {
        label: item.CodeValue,
        value: item.CodeId
      };
    });
    console.log(this.employeeRoles, 'employeerolesmuliti');
  }

  checking() {
    console.log(this.mulitselected, 'multi');
  }

  submit(sampleForm) {
    console.log(sampleForm);
    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRole = [];
    const employeeDetailObj = {
      employeeId: this.employeeData !== undefined ? this.employeeData.EmployeeId : 0,
      employeeDetailId: this.employeeData !== undefined ? this.employeeData.EmployeeDetail[0].EmployeeDetailId : 0,
      exemptions: sampleForm.value.exemption,
      hiredDate: sampleForm.value.hireDate,
      lrt: sampleForm.value.lrtDate,
      tip: sampleForm.value.tip,
      sickRate: sampleForm.value.sick,
      vacRate: sampleForm.value.vacation,
      payRate: sampleForm.value.hourly,
      salary: sampleForm.value.salary,
      comRate: sampleForm.value.commission,
      authId: 0,
      locationId: 0,
      isActive: sampleForm.value.status === 'true' ? true : false
    };
    const employeeAddressObj = {
      employeeAddressId: this.employeeData !== undefined ? this.employeeData.EmployeeAddress[0].EmployeeAddressId : 0,
      relationshipId: 0,
      address1: sampleForm.value.address,
      address2: '',
      phoneNumber: sampleForm.value.phone,
      phoneNumber2: sampleForm.value.cell,
      email: sampleForm.value.email,
      state: sampleForm.value.state,
      country: sampleForm.value.country,
      zip: sampleForm.value.zipcode,
      city: 1,
      isActive: sampleForm.value.status === 'true' ? true : false
    };
    const employeeRoleObj = {
      employeeId: this.employeeData !== undefined ? this.employeeData.EmployeeId : 0,
      employeeRolesId: this.employeeData !== undefined ? this.employeeData.EmployeeRoles[0].EmployeeRolesId : 0,
      roleId: sampleForm.value.role,
      isDefault: true, // need to check
      isActive: sampleForm.value.status === 'true' ? true : false
    };
    employeeRole.push(employeeRoleObj);
    employeAddress.push(employeeAddressObj);
    employeeDetails.push(employeeDetailObj);
    const role = [
      {
        employeeRoleId: 0,
        employeeId: 0,
        roleName: 'name',
        roleId: 3,
        isActive: sampleForm.value.status === 'true' ? true : false
      }
    ];
    const employeeObj = {
      employeeId: this.employeeData !== undefined ? this.employeeData.EmployeeId : 0,
      firstName: sampleForm.value.firstName,
      lastName: sampleForm.value.lastName,
      middleName: sampleForm.value.middleName,
      gender: sampleForm.value.gender,
      ssNo: sampleForm.value.ssn,
      maritalStatus: sampleForm.value.marital,
      birthDate: sampleForm.value.dob,
      isActive: sampleForm.value.status === 'true' ? true : false,
      isCitizen: true,  //  need to check
      alienNo: '123252', // need to check
      immigrationStatus: 1, // need to check
      createdDate: '',
      employeeDetail: employeeDetails,
      employeeAddress: employeAddress,
      employeeRoles: employeeRole,
      employeeRole: role
    };
    sourceObj.push(employeeObj);
    console.log(sourceObj, 'sourceobj');
    this.employeeService.updateEmployee(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        } else {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Record Saved Successfully!!' });
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
    // }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      console.log(res, 'getAllRoles');
    });
  }

  addCollision() {

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

  uploadDocument() {
    const finalObj = [];
    const uploadbj = {
      documentId: 0,
      employeeId: 1,
      fileName: this.fileName,
      filePath: 'D:\\Upload\\',
      password: this.documentForm.value.password,
      createdDate: '2020 - 07 - 21T12: 41: 47.395Z',
      modifiedDate: '2020 - 07 - 21T12: 41: 47.395Z',
      isActive: true,
      base64Url: this.fileUploadformData
    };
    finalObj.push(uploadbj);
    this.employeeService.uploadDocument(finalObj).subscribe( res => {
      console.log(res, 'uploadDcument');
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Document addedd successfully' });
        this.documentDailog = false;
        this.getAllDocument();
      }
    });
  }

  getAllDocument() {
    const employeeId = 1;
    const locationId = 3;
    this.employeeService.getAllDocument(employeeId, locationId).subscribe( res => {
      console.log(res, 'allDocument');
    });
  }

  getDocumentById() {
    const documentId = 1;
    const employeeId = 1;
    const password = '123456789';
    this.employeeService.getDocumentById(documentId, employeeId, password).subscribe( res => {

    });
  }

  closeDocumentPopup() {
    this.documentDailog = false;
  }
}
