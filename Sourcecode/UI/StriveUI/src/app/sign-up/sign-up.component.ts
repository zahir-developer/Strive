import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms';
// import { LoginService } from '../shared/services/login.service';
import { Router, ActivatedRoute } from '@angular/router'
import { AuthService } from '../shared/services/common-service/auth.service';
import { WhiteLabelService } from '../shared/services/data-service/white-label.service';
import { MessengerService } from '../shared/services/data-service/messenger.service';
import { UserDataService } from '../shared/util/user-data.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LandingService } from '../shared/services/common-service/landing.service';
import { GetCodeService } from '../shared/services/data-service/getcode.service';
import { CodeValueService } from '../shared/common-service/code-value.service';
// import { tap, mapTo, share } from 'rxjs/operators';
// import { ApplicationConfig } from '../shared/services/ApplicationConfig';
// import { WeatherService } from '../shared/services/common-service/weather.service';
import { LogoService } from '../shared/services/common-service/logo.service';
import { MakeService } from '../shared/services/common-service/make.service';
import { ModelService } from '../shared/services/common-service/model.service';
import { MessageConfig } from '../shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { WashService } from '../shared/services/data-service/wash.service';

@Component({
  selector: 'app-usersignup',
  templateUrl: './sign-up.component.html',
  // styleUrls: ['./sign-up.component.css']
}) 
export class UserSignUpComponent implements OnInit {
  errorFlag = false;
  loginForm: FormGroup;
  submitted = false;
  display = false;
  loginDetail: string;
  whiteLabelDetail: any;
  colorTheme: any; 
  model: any;
  type: any;
  // vehicle: any;
  color: any;
  filteredModel: any = [];
  filteredcolor: any = [];
  filteredMake: any = [];
  favIcon: HTMLLinkElement = document.querySelector('#appIcon');
  // dashBoardModule: boolean;
  // isRememberMe: boolean;
  constructor(
     private router: Router, private route: ActivatedRoute,
    // private authService: AuthService, 
    private whiteLabelService: WhiteLabelService, private getCodeService: GetCodeService,
    private msgService: MessengerService, 
    private user: UserDataService,
    private logoService: LogoService,
    private spinner: NgxSpinnerService, 
    private makeService: MakeService,
    private modelService: ModelService,
    private toastr: ToastrService,
    private wash: WashService,
    private landing: LandingService, private codeValueService: CodeValueService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required),
      model: new FormControl('', Validators.required),
      color: new FormControl('', Validators.required),
      type: new FormControl('', Validators.required),
    });
    
    this.getAllMake();
    this.getColor();
  }

  get f() { return this.loginForm.controls; }
  
  
  getModel(id) {
    this.modelService.getModelByMakeId(id).subscribe(res => {
      if (res.status === 'Success') {
        const makeModel = JSON.parse(res.resultData);
        this.model = makeModel.Model;
        this.model = this.model.map(item => {
          return {
            id: item.ModelId,
            name: item.ModelValue
          };
        });
        // const models = this.model.filter( item =>  item.id === id.VehicleModelId);
        // if (models.length > 0) {
        //   this.washForm.patchValue({
        //     type: models[0]
        //   });
        // }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getAllMake() {
    this.makeService.getMake().subscribe(res => {
      if (res.status === 'Success') {
        const make = JSON.parse(res.resultData);
        const makes = make.Make;
        this.type = makes.map(item => {
          return {
            id: item.MakeId,
            name: item.MakeValue
          };
        });
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  selectedModel(event) {
    const id = event.id;
    // this.washForm.patchValue({
    //   model: ''
    // });
    if (id !== null) {
      this.getModel(id);
    }
  }
  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.Category === 'VehicleColor');
/*
        if (this.isEdit) {
          vehicle.VehicleDetails.forEach(item => {
            if (+this.selectedData.Washes[0].Make === item.CodeId) {
              this.selectedData.Washes[0].vehicleMake = item.CodeValue;
            } else if (+this.selectedData.Washes[0].Model === item.CodeId) {
              this.selectedData.Washes[0].vehicleModel = item.CodeValue;
            }

            else if (+this.selectedData.Washes[0].Color === item.CodeId) {
              this.selectedData.Washes[0].vehicleColor = item.CodeValue;
            }
          });
          if (this.selectedData.Washes !== null && this.selectedData.WashItem !== null) {
            this.printData = {
              Details: this.selectedData.Washes[0],
              DetailsItem: this.selectedData.WashItem
            };
          }
        }*/


        this.color = this.color.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });


      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getThemeColor() {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.whiteLabelService.getAllWhiteLabelDetail().subscribe(res => {
        if (res.status === 'Success') {
          const label = JSON.parse(res.resultData);
          this.logoService.setLogo(label.WhiteLabelling.WhiteLabel?.Base64);
          const base64 = 'data:image/png;base64,';
          const logoBase64 = base64 + label.WhiteLabelling.WhiteLabel?.Base64;
          this.favIcon.href = logoBase64;
          this.colorTheme = label.WhiteLabelling.Theme;
          this.whiteLabelDetail = label.WhiteLabelling.WhiteLabel;
          this.colorTheme.forEach(item => {
            if (this.whiteLabelDetail.ThemeId === item.ThemeId) {
              document.documentElement.style.setProperty(`--primary-color`, item.PrimaryColor);
              document.documentElement.style.setProperty(`--navigation-color`, item.NavigationColor);
              document.documentElement.style.setProperty(`--secondary-color`, item.SecondaryColor);
              document.documentElement.style.setProperty(`--tertiary-color`, item.TertiaryColor);
              document.documentElement.style.setProperty(`--body-color`, item.BodyColor);
            }
          });
          document.documentElement.style.setProperty(`--text-font`, this.whiteLabelDetail.FontFace);
        }
      });
    }
  }
  signUp(){
    alert('hi');
  }
  
  filterColor(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.color) {
      const color = i;
      if (color.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(color);
      }
    }
    this.filteredcolor = filtered;
  }

  filterMake(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.type) {
      const make = i;
      if (make.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(make);
      }
    }
    this.filteredMake = filtered;
  }

  filterModel(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.model) {
      const model = i;
      if (model.name.toLowerCase().includes(query.toLowerCase())) {
        filtered.push(model);
      }
    }
    this.filteredModel = filtered;
  }

}
