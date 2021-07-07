import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmPasswordValidator } from '../error_helper/password-mismatch';
import { MakeService } from '../shared/services/common-service/make.service';
import { ModelService } from '../shared/services/common-service/model.service';
import { ClientService } from '../shared/services/data-service/client.service';
import { WashService } from '../shared/services/data-service/wash.service';
import { LoginService } from '../shared/services/login.service';
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
  vehicleArray: any;
  emailVal = false;
  token: any;
  userGroup: any;
  constructor(private fb: FormBuilder, private makeService: MakeService, private modelService: ModelService,
    private toastr: ToastrService, private wash: WashService, private router: Router, private spinner: NgxSpinnerService,
    private client: ClientService, private activatedRoute: ActivatedRoute,
    private login: LoginService) { }

  ngOnInit() {
    this.getSignupToken();
    this.createSignupForm();
    this.getVehicleMakeList();
    this.getVehicleColorList();
    this.createVehicleForm();
  }

  createSignupForm() {
    this.userSignupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userEmail: ['', [Validators.required]],
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
      this.errorText = 'Please enter vehicle details'
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
        this.errorText = "Passsword and Confirm Password didn't match."
        return;
      }
    }

    if (this.vehicleForm.invalid) {
      this.formVal = true;
      this.vehicleForm.controls.items.value.forEach(data => {
        if (data.vehicleType === '') {
          this.errorText = "Please enter Vehicle Make."
          return;
        } else if (data.vehicleModel === '') {
          this.errorText = "Please enter Vehicle Model."
          return;
        } else if (data.vehicleColor === '') {
          this.errorText = "Please enter Vehicle Color."
          return;
        }
      });
      return;
    }

    if (this.emailVal) {
      this.errorText = 'Email already exists, Please try login.';
      return;
    }

    this.formVal = false;
    this.vehicleArrayGroup();

    const client = {
      "clientId": 0,
      "firstName": this.userSignupForm.controls.firstName.value ? this.userSignupForm.controls.firstName.value : '',
      "middleName": null,
      "lastName": this.userSignupForm.controls.lastName.value ? this.userSignupForm.controls.lastName.value : '',
      "gender": null,
      "maritalStatus": null,
      "birthDate": null,
      "notes": "",
      "recNotes": "",
      "score": null,
      "clientType": "82",
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
        "phoneNumber2": null,
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

    const finalObj = {
      "client": client,
      "clientAddress": clientAddress,
      "clientVehicle": this.vehicleArray,
      "token": this.token,
      "password": this.userSignupForm.controls.password.value
    }

    this.spinner.show();
    this.login.createAccount(finalObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        this.toastr.success(MessageConfig.Client.Add, 'Success');
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  fieldValidation() {
    if (this.userSignupForm.controls.firstName.invalid || this.userSignupForm.controls.firstName.value === '') {
      this.errorText = "Please enter first name."
    } else if (this.userSignupForm.controls.lastName.invalid || this.userSignupForm.controls.lastName.value === '') {
      this.errorText = "Please enter last name."
    } else if (this.userSignupForm.controls.userEmail.invalid || this.userSignupForm.controls.userEmail.value === '') {
      this.errorText = "Please enter email address."
    } else if (this.userSignupForm.controls.phoneNumber.invalid || this.userSignupForm.controls.phoneNumber.value === '' ||
      this.userSignupForm.controls.phoneNumber.value.replace(/[^a-zA-Z0-9]/g, "").length === 1) {
      this.errorText = "Please enter phone number."
    } else if (this.userSignupForm.controls.password.invalid || this.userSignupForm.controls.password.value === '') {
      this.errorText = "Please enter password."
    } else if (this.userSignupForm.controls.confirmPassword.invalid || this.userSignupForm.controls.confirmPassword.value === '') {
      this.errorText = "Please enter confirm password."
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
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }


  filterColor(event) {
    let filtered: any[] = [];
    let query = event.query;
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
    this.makeService.getColor().subscribe(data => {
      if (data.status === 'Success') {
        const result = JSON.parse(data.resultData);
        this.colorTotalList = result.Color.map(item => {
          return {
            code: item.ColorId,
            name: item.ColorValue
          };
        });
      }
    });
  }

  loginPage() {
    this.router.navigate(['/login'])
  }


  vehicleArrayGroup() {
    const totalArray = [];
    for (let i = 0; i < this.vehicleForm.value.items.length; i++) {
      const group = {
        "vehicleId": 0,
        "locationId": null,
        "vehicleNumber": "",
        "vehicleMfr": this.vehicleForm.value.items[i].vehicleType.code,
        "vehicleModel": this.vehicleForm.value.items[i].vehicleModel.code,
        "vehicleModelNo": null,
        "vehicleYear": null,
        "vehicleColor": this.vehicleForm.value.items[i].vehicleColor.code,
        "upcharge": null,
        "barcode": null,
        "notes": null,
        "isActive": true,
        "isDeleted": false,
        "createdBy": null,
        "createdDate": new Date(),
        "updatedBy": null,
        "updatedDate": new Date(),
      }
      totalArray.push(group);
    }
    this.vehicleArray = totalArray;
  }


  emailCheck(email) {
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email)) {
      this.login.emailIdExists(this.userSignupForm.controls.userEmail.value).subscribe(res => {
        if (res.status === 'Success') {
          const sameEmail = JSON.parse(res.resultData);
          if (sameEmail.EmailIdExist === true) {
            this.errorText = 'Email already exists, Please try login.'
            this.formVal = true;
            this.emailVal = true;
          } else {
            this.formVal = false;
            this.emailVal = false;
          }
        }
      }, (err) => {
        this.emailVal = true;
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.errorText = 'Please enter valid email Id.'
      this.formVal = true;
      this.emailVal = true;
    }
  }


  getSignupToken() {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params.token) {
        this.token = params.token;
      }
    });
  }

  validateGroup(event, user) {
    this.formVal = false;
    this.userGroup = user;
    switch (this.userGroup) {
      case 'first': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter first name."
        } else if (event !== '') {
          this.formVal = false;
        }
        break;
      }
      case 'last': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter last name."
        } else if (event !== '') {
          this.formVal = false;
        }
        break;
      }
      case 'email': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter email address."
        } else if (event !== '') {
          this.emailCheck(event);
        }
        break;
      }
      case 'phone': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter phone number."
        } else if (event !== '') {
          this.formVal = false;
        }
        break;
      }
      case 'password': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter password."
        } else if (event !== '') {
          this.formVal = false;
        }
        break;
      }
      case 'confirm': {
        if (event === '') {
          this.formVal = true;
          this.errorText = "Please enter confirm password."
        } else if (event !== '') {
          this.formVal = false;
        }
        break;
      }
    }
  }


}
