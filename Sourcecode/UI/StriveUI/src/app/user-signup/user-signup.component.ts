import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmPasswordValidator } from '../error_helper/password-mismatch';
import { MakeService } from '../shared/services/common-service/make.service';
import { ModelService } from '../shared/services/common-service/model.service';
import { ClientService } from '../shared/services/data-service/client.service';
import { WashService } from '../shared/services/data-service/wash.service';
import { MessageConfig } from '../shared/services/messageConfig';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.css']
})
export class UserSignupComponent implements OnInit {

  userSignupForm: FormGroup;
  filteredMake: any[];
  vehicleMakeTotalList: any;
  makeTotalList: any;
  filteredModel: any[];
  vehicleModelTotalList: any;
  modelTotalList: any;
  filteredColor: any[];
  colorTotalList: any;
  submitted: boolean = false;
  vehicleForm: FormGroup;
  items: FormArray;
  formVal: boolean = false;
  errorText: string;

  constructor(private fb: FormBuilder, private makeService: MakeService, private modelService: ModelService,
    private toastr: ToastrService, private wash: WashService, private router: Router, private spinner: NgxSpinnerService,
    private client: ClientService,) { }

  ngOnInit() {
    this.createSignupForm();
    this.getVehicleMakeList();
    this.getVehicleColorList();
    this.createVehicleForm();
  }

  createSignupForm() {
    this.userSignupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userEmail: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  createVehicleForm() {
    this.vehicleForm = new FormGroup({
      items: new FormArray([this.createItem()])
    });
  }

  createItem() {
    return this.fb.group({
      vehicleModel: ['', Validators.required],
      vehicleType: ['', Validators.required],
      vehicleColor: ['', Validators.required],
    });
  }

  get aliasesArrayControl() {
    return (this.vehicleForm.get('items') as FormArray).controls;
  }

  addVehicleRow() {
    if (this.vehicleForm.invalid) {
      this.formVal = true;
      this.errorText = 'Please fill all the vehicle info field'
    } else if (this.vehicleForm.valid) {
      this.formVal = false;
      this.items = this.vehicleForm.get('items') as FormArray;
      this.items.push(this.createItem());
    }
  }


  ConfirmPasswordValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      let control = formGroup.controls[controlName];
      let matchingControl = formGroup.controls[matchingControlName]
      if (
        matchingControl.errors &&
        !matchingControl.errors.confirmPasswordValidator
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmPasswordValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }


  userSignupSubmit() {
    this.submitted = true;
    if (this.userSignupForm.invalid) {
      this.formVal = true;
      this.fieldValidation();
      return;
    } else if (this.userSignupForm.valid) {
      this.formVal = true;
      if (this.userSignupForm.controls.password.value !== this.userSignupForm.controls.confirmPassword.value) {
        this.errorText = "Passsword and Confirm Password didn't match"
        return;
      }
    }

    if (this.vehicleForm.invalid) {
      this.formVal = true;
      this.vehicleForm.controls.items.value.forEach(data => {
        if (data.vehicleType === '') {
          this.errorText = "Please enter make"
          return;
        } else if (data.vehicleModel === '') {
          this.errorText = "Please enter model"
          return;
        } else if (data.vehicleColor === '') {
          this.errorText = "Please enter color"
          return;
        }
      });
      return;
    }
    this.formVal = false;

    const client = {
      "clientId": 0,
      "firstName": this.userSignupForm.controls.firstName.value ? this.userSignupForm.controls.firstName.value : '',
      "middleName": null,
      "lastName": this.userSignupForm.controls.lastName.value ? this.userSignupForm.controls.lastName.value : '',
      "gender": null,
      "maritalStatus": null,
      "birthDate": "",
      "notes": "",
      "recNotes": "",
      "score": 0,
      "clientType": 0,
      "isActive": true,
      "isDeleted": false,
      "createdBy": 0,
      "createdDate": new Date(),
      "updatedBy": 0,
      "updatedDate": new Date(),
      "isCreditAccount": true
    }

    const clientAddress = [
      {
        "clientAddressId": 0,
        "clientId": 0,
        "address1": null,
        "address2": null,
        "phoneNumber": this.userSignupForm.controls.phoneNumber.value ? this.userSignupForm.controls.phoneNumber.value : '',
        "phoneNumber2": "",
        "email": this.userSignupForm.controls.userEmail.value ? this.userSignupForm.controls.userEmail.value : '',
        "state": null,
        "country": null,
        "zip": null,
        "isActive": true,
        "isDeleted": false,
        "createdBy": 0,
        "createdDate": new Date(),
        "updatedBy": 0,
        "updatedDate": new Date()
      }
    ]

    const vehicle = [
      {
        "vehicleId": 0,
        "locationId": 0,
        "vehicleNumber": "",
        "vehicleMfr": 0,
        "vehicleModel": 0,
        "vehicleModelNo": null,
        "vehicleYear": null,
        "vehicleColor": 0,
        "upcharge": 0,
        "barcode": "",
        "notes": null,
        "isActive": true,
        "isDeleted": false,
        "createdBy": 0,
        "createdDate": new Date(),
        "updatedBy": 0,
        "updatedDate": new Date(),
      }
    ]

    const totalList = {
      "client": client,
      "clientAddress": clientAddress,
      "clientVehicle": vehicle
    }

    this.spinner.show();
    this.client.addClient(totalList).subscribe(data => {
      if (data.status === 'Success') {
        alert('welcome');
        this.spinner.hide();
        this.toastr.success(MessageConfig.Client.Add, 'Success');
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  fieldValidation() {
    if (this.userSignupForm.controls.firstName.invalid || this.userSignupForm.controls.firstName.value === '') {
      this.errorText = "Please enter first name"
    } else if (this.userSignupForm.controls.lastName.invalid || this.userSignupForm.controls.lastName.value === '') {
      this.errorText = "Please enter last name"
    } else if (this.userSignupForm.controls.userEmail.invalid || this.userSignupForm.controls.userEmail.value === '') {
      this.errorText = "Please enter email"
    } else if (this.userSignupForm.controls.phoneNumber.invalid || this.userSignupForm.controls.phoneNumber.value === '' ||
      this.userSignupForm.controls.phoneNumber.value.replace(/[^a-zA-Z0-9]/g, "").length === 1) {
      this.errorText = "Please enter phone number"
    } else if (this.userSignupForm.controls.password.invalid || this.userSignupForm.controls.password.value === '') {
      this.errorText = "Please enter password"
    } else if (this.userSignupForm.controls.confirmPassword.invalid || this.userSignupForm.controls.confirmPassword.value === '') {
      this.errorText = "Please enter confirm password"
    }
  }


  filterType(event) {
    let filtered: any[] = [];
    let query = event.query;
    if (this.makeTotalList) {
      for (let i = 0; i < this.makeTotalList.length; i++) {
        let makeList = this.makeTotalList[i];
        if (makeList.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
          filtered.push(makeList);
        }
      }
    }
    this.filteredMake = filtered;
  }


  getVehicleMakeList() {
    this.makeService.getMake().subscribe(data => {
      if (data.status === 'Success') {
        this.vehicleMakeTotalList = JSON.parse(data.resultData);
        const makeList = this.vehicleMakeTotalList.Make;
        if (makeList) {
          this.makeTotalList = makeList.map(item => {
            return {
              code: item.MakeId,
              name: item.MakeValue
            };
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  selectMake(event) {
    this.getVehicleModelList(event.code)
  }



  filterModel(event) {
    let filtered: any[] = [];
    let query = event.query;
    console.log(this.modelTotalList);
    if (this.modelTotalList) {
      for (let i = 0; i < this.modelTotalList.length; i++) {
        let modelList = this.modelTotalList[i];
        if (modelList.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
          filtered.push(modelList);
        }
      }
    }
    this.filteredModel = filtered;
  }


  getVehicleModelList(code) {
    this.modelService.getModelByMakeId(code).subscribe(data => {
      if (data.status === 'Success') {
        this.vehicleModelTotalList = JSON.parse(data.resultData);
        const modelList = this.vehicleModelTotalList.Model;
        if (modelList) {
          this.modelTotalList = modelList.map(item => {
            return {
              code: item.ModelId,
              name: item.ModelValue
            };
          });
        }
        console.log(this.modelTotalList);
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  filterColor(event) {
    let filtered: any[] = [];
    let query = event.query;
    console.log(this.colorTotalList);
    if (this.colorTotalList) {
      for (let i = 0; i < this.colorTotalList.length; i++) {
        let colorList = this.colorTotalList[i];
        if (colorList.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
          filtered.push(colorList);
        }
      }
    }
    this.filteredColor = filtered;
  }



  getVehicleColorList() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        const colorList = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');
        if (colorList) {
          this.colorTotalList = colorList.map(item => {
            return {
              code: item.CodeId,
              name: item.CodeValue
            };
          });
        }
      }
    });
  }

  loginPage() {
    this.router.navigate(['/login'])
  }


}
