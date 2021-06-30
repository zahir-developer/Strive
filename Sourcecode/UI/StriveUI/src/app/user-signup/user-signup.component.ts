import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
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

  constructor(private fb: FormBuilder, private makeService: MakeService, private modelService: ModelService,
    private toastr: ToastrService, private wash: WashService, private router: Router, private spinner: NgxSpinnerService,
    private client: ClientService,) { }

  ngOnInit() {
    this.createSignupForm();
    this.getVehicleMakeList();
    this.getVehicleColorList();
  }

  createSignupForm() {
    this.userSignupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userEmail: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      vehicleModel: ['', Validators.required],
      vehicleType: ['', Validators.required],
      vehicleColor: ['', Validators.required],
    },{
      validator: ConfirmPasswordValidator("password", "confirmPassword")
    });
  }

  get f() {
    return this.userSignupForm.controls;
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
  console.log(this.userSignupForm.controls);
  const groupList = {
    "client": {
      "clientId": 0,
      "firstName": this.userSignupForm.controls.firstName.value ? this.userSignupForm.controls.firstName.value : '',
      "middleName": "",
      "lastName": this.userSignupForm.controls.lastName.value ? this.userSignupForm.controls.lastName.value : '',
      "gender": 0,
      "maritalStatus": 0,
      "birthDate": "",
      "notes": "",
      "recNotes": "",
      "score": 0,
      "noEmail": true,
      "clientType": 0,
      "authId": 0,
      "isActive": true,
      "isDeleted": true,
      "createdBy": 0,
      "createdDate": "",
      "updatedBy": 0,
      "updatedDate": "",
      "amount": 0,
      "isCreditAccount": true
    },
    "clientVehicle": [
      {
        "vehicleId": 0,
        "clientId": 0,
        "locationId": 0,
        "vehicleNumber": "",
        "vehicleMfr": 0,
        "vehicleModel": this.userSignupForm.controls.vehicleModel.value.code ? this.userSignupForm.controls.vehicleModel.value.code : 0,
        "vehicleModelNo": 0,
        "vehicleYear": "",
        "vehicleColor": this.userSignupForm.controls.vehicleColor.value.code ? this.userSignupForm.controls.vehicleColor.value.code : 0,
        "upcharge": 0,
        "barcode": "",
        "notes": "",
        "isActive": true,
        "isDeleted": true,
        "createdBy": 0,
        "createdDate": "",
        "updatedBy": 0,
        "updatedDate": "",
        "monthlyCharge": 0
      }
    ],
    "clientAddress": [
      {
        "clientAddressId": 0,
        "clientId": 0,
        "address1": "",
        "address2": "",
        "phoneNumber": this.userSignupForm.controls.phoneNumber.value ? this.userSignupForm.controls.phoneNumber.value.replace(/[^a-zA-Z0-9]/g, "") : '',
        "phoneNumber2": "",
        "email": this.userSignupForm.controls.userEmail.value ? this.userSignupForm.controls.userEmail.value : '',
        "city": 0,
        "state": 0,
        "country": 0,
        "zip": "",
        "isActive": true,
        "isDeleted": true,
        "createdBy": 0,
        "createdDate": "",
        "updatedBy": 0,
        "updatedDate": ""
      }
    ]
  }
  this.spinner.show();
  this.client.addClient(groupList).subscribe(data => {
      if (data.status === 'Success') {
        alert('welcome');
        this.spinner.hide();
        this.toastr.success(MessageConfig.Client.Add, 'Success');
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    },(err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });


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
