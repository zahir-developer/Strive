import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import * as _ from 'underscore';

@Component({
  selector: 'app-employee-collision',
  templateUrl: './employee-collision.component.html',
  styleUrls: ['./employee-collision.component.css']
})
export class EmployeeCollisionComponent implements OnInit {
  submitted: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private messageService: MessageServiceToastr,
    private spinner: NgxSpinnerService
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
  ngOnInit(): void {
    this.submitted = false;
    this.collisionForm = this.fb.group({
      dateOfCollision: ['', Validators.required],
      amount: ['', Validators.required],
      reason: ['', Validators.required],
      client: [''],
      vehicle: ['']
    });
    this.getAllClient();
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
      this.spinner.hide();
      if (res.status === 'Success') {
        const employeesCollison = JSON.parse(res.resultData);
        console.log(employeesCollison.Collision);
        if (employeesCollison.Collision) {
          const detail = employeesCollison.Collision.Liability[0];
          this.collisionDetail = detail;
          this.liabilityDetail = employeesCollison.Collision.LiabilityDetail[0];
          this.setValue(detail);
        }
      }
    });
  }

  setValue(detail) {
    const clientName = _.where(this.clientList, { id: detail.ClientId });
    if (clientName.length > 0) {
      this.selectedClient(clientName[0]);
      this.collisionForm.patchValue({
        client: clientName[0]
      });
    }
    this.collisionForm.patchValue({
      dateOfCollision: moment(detail.CreatedDate).toDate(),
      amount: this.liabilityDetail.Amount.toFixed(2),
      reason: this.liabilityDetail.Description,
      vehicle: detail.VehicleId
    });
  }

  get f() {
    return this.collisionForm.controls;
  }

  saveCollision() {
    console.log(this.collisionForm);
    this.submitted = true;
    if (this.collisionForm.invalid) {
      return;
    }
    const liabilityDetailObj = {
      liabilityDetailId: this.mode === 'edit' ? this.liabilityDetail.LiabilityDetailId : 0,
      liabilityId: this.mode === 'edit' ? +this.collisionDetail.LiabilityId : 0,
      liabilityDetailType: 1,
      amount: +this.collisionForm.value.amount,
      paymentType: 1,
      documentPath: '',
      description: this.collisionForm.value.reason,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.collisionForm.value.dateOfCollision,//moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const liabilityObj = {
      liabilityId: this.mode === 'edit' ? +this.collisionDetail.LiabilityId : 0,
      employeeId: this.employeeId,
      liabilityType: 103,
      liabilityDescription: this.collisionForm.value.reason,
      productId: 2,
      totalAmount: +this.collisionForm.value.amount,
      status: 0,
      isActive: true,
      isDeleted: false,
      vehicleId: this.collisionForm.value.vehicle,
      clientId: this.collisionForm.value.client.id,
      createdBy: +localStorage.getItem('empId'),
      createdDate: this.collisionForm.value.dateOfCollision,//moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const finalObj = {
      employeeLiability: liabilityObj,
      employeeLiabilityDetail: liabilityDetailObj
    };
    if (this.mode === 'create') {
      this.spinner.show();
      this.employeeService.saveCollision(finalObj).subscribe(res => {
        this.spinner.hide();
        if (res.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Employee Collision Added Successfully!' });
          this.activeModal.close(true);
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
      });
    } else {
      this.spinner.show();
      this.employeeService.updateCollision(finalObj).subscribe(res => {
        this.spinner.hide();
        if (res.status === 'Success') {
          this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Employee Collision Updated Successfully!' });
          this.activeModal.close(true);
        } else {
          this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        }
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
        console.log(client, 'client');
        this.clientList = client.Client.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      }
    });
  }

  filterClient(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.clientList) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredClient = filtered;
  }

  selectedClient(event) {
    const clientId = event.id;
    this.employeeService.getVehicleByClientId(clientId).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        this.vehicleList = vehicle.Status;
      }
    });
  }

  getAllMake() {
    this.employeeService.getDropdownValue('MAKE').subscribe(res => {
      if (res.status === 'Success') {
        const make = JSON.parse(res.resultData);
        this.makeDropdownList = make.Codes;
      }
    });
  }

  getAllModel() {
    this.employeeService.getDropdownValue('VEHICLEMODEL').subscribe(res => {
      if (res.status === 'Success') {
        const model = JSON.parse(res.resultData);
        this.modelDropdownList = model.Codes;
      }
    });
  }

  getAllColor() {
    this.employeeService.getDropdownValue('VEHICLECOLOR').subscribe(res => {
      if (res.status === 'Success') {
        const color = JSON.parse(res.resultData);
        this.colorDropdownList = color.Codes;
      }
    });
  }

}
