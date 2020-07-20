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
  collision: any = [];
  collisionDetail: any;
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private messageService: MessageServiceToastr) { }

  ngOnInit() {
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
    // console.log(this.selectedData);
    this.employeRole();
    this.setValue();
    this.getAllCollision();
    this.getCollisionById();
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
      const employeeDetail = employee.EmployeeDetail !== null ? employee.EmployeeDetail[0] : '';
      const employeeAddress = employee.EmployeeAddress !== null ? employee.EmployeeAddress[0] : '';
      const employeeRole = employee.EmployeeRole !== null ? employee.EmployeeRole[0] : '';
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

  submit(sampleForm) {
    console.log(sampleForm);
    const sourceObj = [];
    const employeeDetails = [];
    const employeAddress = [];
    const employeeRoles = [];
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
      employeeRolesId: this.employeeData !== undefined ? this.employeeData.EmployeeRole !== null ?
        this.employeeData.EmployeeRole[0].EmployeeRolesId : 0 : 0,
      roleId: sampleForm.value.role,
      isDefault: true, // need to check
      isActive: sampleForm.value.status === 'true' ? true : false
    };
    employeeRoles.push(employeeRoleObj);
    employeAddress.push(employeeAddressObj);
    employeeDetails.push(employeeDetailObj);
    const employeeObj = {
      employeeDetail: employeeDetails,
      employeeAddress: employeAddress,
      employeeRole: employeeRoles,
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
      createdDate: '2020-07-17T14:43:00.607Z'
    };
    sourceObj.push(employeeObj);
    console.log(sourceObj, 'sourceobj');
    const finalObj = {
      lstEmployee: employeeObj
    };
    this.employeeService.updateEmployee(employeeObj).subscribe(data => {
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

  getAllCollision() {
    this.employeeService.getAllCollision().subscribe(res => {
      if (res.status === 'Success') {
        const responseParseData = JSON.parse(res.resultData);
        this.collision = responseParseData.Collision;
        console.log(this.collision, 'collision');
      }
    });
  }

  getCollisionById() {
    const id = 1;
    this.employeeService.getCollisionById(id).subscribe(res => {
      if (res.status === 'Success') {
        const responseParseData = JSON.parse(res.resultData);
        this.collisionDetail = responseParseData.CollisionDetail;
        console.log(this.collisionDetail, 'collisiondetail');
      }
    });
  }

  deleteCollision() {
    const id = 1;
    this.employeeService.deleteCollision(id).subscribe(res => {
      if (res.status === 'Success') {
        this.getAllCollision();
      }
    });
  }

  saveCollision() {
    const detail = [];
    const finalObj = [];
    const detailObj = {
      liabilityDetailId: 0,
      liabilityId: 0,
      liabilityDetailType: 1,
      amount: 4,
      paymentType: 1,
      documentPath: 'anadh',
      description: 'anndg',
      createdDate: '2020-07-17T15:03:21.229Z',
      isActive: true
    };
    detail.push(detailObj);
    const collitionObj = {
      liabilityId: 0,
      employeeId: 20015,
      liabilityType: 2,
      liabilityDescription: 'description',
      productId: 1,
      status: 1,
      createdDate: '2020-07-17T15:03:21.229Z',
      isActive: true,
      liabilityDetail: detail
    };
    finalObj.push(collitionObj);
    this.employeeService.saveCollision(finalObj).subscribe(res => {
      console.log(res);
    });
  }

  saveEmployee(sampleForm) {
    const detail = [];
    const address = [];
    const role = [];
    const detailObj = {
      employeeDetailId:  0,
      employeeId:  0,
      employeeCode: 'string',
      authId: 0,
      locationId: 1,
      payRate: 'pay', // sampleForm.value.hourly,
      sickRate: 'sick', //sampleForm.value.sick,
      vacRate: 'vcc', // sampleForm.value.vacation,
      comRate: 'com', // sampleForm.value.commission,
      hiredDate: '2020-07-20T06:03:10.403Z', // sampleForm.value.hireDate,
      salary: 'saraly',  // sampleForm.value.salary,
      tip: 'tip', // sampleForm.value.tip,
      lrt: '2020-07-20T06:03:10.403Z', //sampleForm.value.lrtDate,
      exemptions: 0,   // sampleForm.value.exemption,
      isActive: true // sampleForm.value.status === 'true' ? true : false
    };
    const addressObj = {
      employeeAddressId: 0,
      relationshipId: 1,
      address1: 'adreess1', //sampleForm.value.address,
      address2: 'string',
      phoneNumber: 'phne', // sampleForm.value.phone,
      phoneNumber2: 'cell', // sampleForm.value.cell,
      email: 'email',  // sampleForm.value.email,
      city: 1,
      state: 1, // sampleForm.value.state,
      zip: 'zip' , // sampleForm.value.zipcode,
      isActive: true, // sampleForm.value.status === 'true' ? true : false,
      country: 1  // sampleForm.value.country
    };
    const roleObj = {
      employeeRoleId:  0,
      employeeId:  0,
      roleId: sampleForm.value.role,
      isDefault: true,
      isActive: true, // sampleForm.value.status === 'true' ? true : false
    };
    detail.push(detailObj);
    address.push(addressObj);
    role.push(roleObj);
    const employeeObj = {
      employeeDetail: detail,
      employeeAddress: address,
      employeeRole: role,
      employeeId: 0,
      firstName: 'anandh', // sampleForm.value.firstName,
      middleName: 'as', // sampleForm.value.middleName,
      lastName: 'subburaj', // sampleForm.value.lastName,
      gender: 14, // sampleForm.value.gender,
      ssNo: 'string', // sampleForm.value.ssn,
      maritalStatus: 14, // sampleForm.value.marital,
      isCitizen: true,
      alienNo: 'string',
      birthDate: '2020-07-20T06:03:10.403Z',  // sampleForm.value.dob,
      immigrationStatus: 0,
      createdDate: '2020-07-20T06:03:10.403Z',
      isActive: true // sampleForm.value.status === 'true' ? true : false
    };
    this.employeeService.updateEmployee(employeeObj).subscribe(res => {
      console.log(res);
      if (res.status === 'Success') {
        if (this.isEdit === true) {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        } else {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Record Saved Successfully!!' });
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
  }

}
