import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import * as _ from 'underscore';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-employee-collision',
  templateUrl: './employee-collision.component.html'
})
export class EmployeeCollisionComponent implements OnInit {
  submitted: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private spinner: NgxSpinnerService,
    private getCode: GetCodeService,
    private wash: WashService,
    private toastr: ToastrService,
    private codeValueService: CodeValueService
  ) { }
  @Input() public employeeId?: any;
  @Input() public collisionId?: any;
  @Input() public mode?: any;
  collisionForm: FormGroup;
  collisionDetail: any;
  makeDropdownList: any = [];
  modelDropdownList: any = [];
  colorDropdownList: any = [];
  clientList: any = [];
  filteredClient: any = [];
  vehicleList: any = [];
  liabilityDetail: any;
  liabilityTypeId: any;
  liabilityDetailTypeId: any;
  ngOnInit(): void {
    this.submitted = false;
    this.collisionForm = this.fb.group({
      dateOfCollision: ['', Validators.required],
      amount: ['', Validators.required],
      reason: ['', Validators.required],
      client: ['', Validators.required],
      vehicle: ['', Validators.required]
    });
    this.getLiabilityType();
    this.getLiabilityDetailType();
    this.getAllModel();
    this.getAllMake();
    this.getAllColor();
    if (this.mode === 'edit') {
      this.getCollisionDetail();
    }
  }

  closeModal() {
    this.activeModal.close();
  }

  getCollisionDetail() {
    this.spinner.show();
    this.employeeService.getDetailCollision(this.collisionId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const employeesCollison = JSON.parse(res.resultData);
        if (employeesCollison.Collision) {
          const detail = employeesCollison.Collision.Liability[0];
          this.collisionDetail = detail;
          this.liabilityDetail = employeesCollison.Collision.LiabilityDetail[0];
          this.setValue(detail);
        }
      }
      else{
        this.spinner.hide();

      }
    }, (err) => {
      this.spinner.hide();

      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  setValue(detail) {
    // const clientName = _.where(this.clientList, { id: detail.ClientId });
    // if (clientName.length > 0) {
    //   this.selectedClient(clientName[0]);
    //   this.collisionForm.patchValue({
    //     client: clientName[0]
    //   });
    // }
    const clientName = detail.ClientFirstName + '' + detail.ClientLastName;
    this.collisionForm.patchValue({
      dateOfCollision: moment(detail.CreatedDate).toDate(),
      amount: this.liabilityDetail.Amount.toFixed(2),
      reason: this.liabilityDetail.Description,
      client: { id: detail.ClientId , name: clientName }
    });
    this.selectedClient(detail.ClientId);
  }

  getLiabilityType() {
    const liabilityTypeVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.liablityType);
    console.log(liabilityTypeVaue, 'cached value ');
    if (liabilityTypeVaue.length > 0) {
      this.liabilityTypeId = liabilityTypeVaue.filter(i => i.CodeValue === ApplicationConfig.CodeValue.Collision)[0].CodeId;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.liablityType).subscribe(data => {
        if (data.status === 'Success') {
          const dType = JSON.parse(data.resultData);
          this.liabilityTypeId = dType.Codes.filter(i => i.CodeValue === ApplicationConfig.CodeValue.Collision)[0].CodeId;
        }
      }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  getLiabilityDetailType() {
    const liabilityDetailTypeVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.liablityDetailType);
    console.log(liabilityDetailTypeVaue, 'cached value ');
    if (liabilityDetailTypeVaue.length > 0) {
      this.liabilityDetailTypeId = liabilityDetailTypeVaue.filter(i => i.CodeValue === ApplicationConfig.CodeValue.adjustment)[0].CodeId;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.LiablityDetailType).subscribe(data => {
        if (data.status === 'Success') {
          const dType = JSON.parse(data.resultData);
          this.liabilityDetailTypeId = dType.Codes.filter(i => i.CodeValue === ApplicationConfig.CodeValue.adjustment)[0].CodeId;
        }
      }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  get f() {
    return this.collisionForm.controls;
  }

  onKeyUp(event) {
    if (event.target.value === '') {
      this.collisionForm.patchValue({ vehicle: '' });
    }
  }

  saveCollision() {
    this.submitted = true;
    if (this.collisionForm.invalid) {
      return;
    }
    const liabilityDetailObj = {
      liabilityDetailId: this.mode === 'edit' ? this.liabilityDetail.LiabilityDetailId : 0,
      liabilityId: this.mode === 'edit' ? +this.collisionDetail.LiabilityId : 0,
      liabilityDetailType: this.liabilityDetailTypeId,
      amount: +this.collisionForm.value.amount,
      paymentType: 1,
      documentPath: null, 
      description: this.collisionForm.value.reason,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.collisionForm.value.dateOfCollision,
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const liabilityObj = {
      liabilityId: this.mode === 'edit' ? +this.collisionDetail.LiabilityId : 0,
      employeeId: this.employeeId,
      liabilityType: this.liabilityTypeId,
      liabilityDescription: this.collisionForm.value.reason,
      productId: null, 
      totalAmount: +this.collisionForm.value.amount,
      status: 0, 
      isActive: true,
      isDeleted: false,
      vehicleId: this.collisionForm.value.vehicle,
      clientId: this.collisionForm.value.client.id,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.collisionForm.value.dateOfCollision,
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      locationId : localStorage.getItem('empLocationId')
    };
    const finalObj = {
      employeeLiability: liabilityObj,
      employeeLiabilityDetail: liabilityDetailObj
    };
    if (this.mode === 'create') {
      this.spinner.show();
      this.employeeService.saveCollision(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Collision.Add, 'Success!');
          this.activeModal.close(true);
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.employeeService.updateCollision(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Collision.Update, 'Success!');
          this.activeModal.close(true);
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    }
  }

  getAllClient() {
    this.employeeService.getAllClient().subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.Client.forEach(item => {
          item.fullName = item.FirstName + ' ' + item.LastName;
        });
        this.clientList = client.Client.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  filterClient(event) {
    const filtered: any[] = [];
    const query = event.query;
    this.wash.getAllClients(query).subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.ClientName.forEach(item => {
          item.fullName = item.FirstName + ' ' + item.LastName;
        });
        this.clientList = client.ClientName.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  
  }

  selectedClient(client) {
    this.employeeService.getVehicleByClientId(client.id).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        this.vehicleList = vehicle.Status;
        if (this.vehicleList.length !== 0) {
          this.collisionForm.patchValue({ vehicle: this.vehicleList[this.vehicleList.length - 1].VehicleId });
        }
      }
    }
    , (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  getAllMake() {
    const makeVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.vehcileMake);
    console.log(makeVaue, 'cached value ');
    if (makeVaue.length > 0) {
      this.makeDropdownList = makeVaue;
    } else {
      this.employeeService.getDropdownValue('MAKE').subscribe(res => {
        if (res.status === 'Success') {
          const make = JSON.parse(res.resultData);
          this.makeDropdownList = make.Codes;
        }
      }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  getAllModel() {
    const modelVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.vehicleModel);
    console.log(modelVaue, 'cached value ');
    if (modelVaue.length > 0) {
      this.modelDropdownList = modelVaue;
    } else {
      this.employeeService.getDropdownValue('VEHICLEMODEL').subscribe(res => {
        if (res.status === 'Success') {
          const model = JSON.parse(res.resultData);
          this.modelDropdownList = model.Codes;
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  getAllColor() {
    const colorVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.vehcileColor);
    console.log(colorVaue, 'cached value ');
    if (colorVaue.length > 0) {
      this.colorDropdownList = colorVaue;
    } else {
      this.employeeService.getDropdownValue('VEHICLECOLOR').subscribe(res => {
        if (res.status === 'Success') {
          const color = JSON.parse(res.resultData);
          this.colorDropdownList = color.Codes;
        }
      }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

}
