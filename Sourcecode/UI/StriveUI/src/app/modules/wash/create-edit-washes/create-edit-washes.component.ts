import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { isEmpty } from 'rxjs/operators';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';
import { ClientService } from 'src/app/shared/services/data-service/client.service';

@Component({
  selector: 'app-create-edit-washes',
  templateUrl: './create-edit-washes.component.html',
  styleUrls: ['./create-edit-washes.component.css']
})
export class CreateEditWashesComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  washForm: FormGroup;
  timeIn: any;
  timeOut: any;
  minutes: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  Score: any;
  ticketNumber: any;
  barcodeDetails: any;
  vehicle: any;
  color: any;
  type: any;
  jobTypeId: any;
  additionalService: any = [];
  serviceEnum: any;
  additional: any;
  washes: any;
  upcharges: any;
  airFreshner: any;
  UpchargeType: any;
  jobItems: any;
  washItem: any = [];
  membership: any;
  timeInDate: any;
  timeOutDate: any;
  model: any;
  clientList: any;
  filteredClient: any[];
  memberService: any[];
  submitted: boolean;
  isBarcode: boolean = false;
  headerData: string;
  showVehicleDialog: boolean;
  showClientDialog: boolean;
  clientId: any;
  address: any;
  closeclientDialog: any;
  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr,
    private wash: WashService, private client: ClientService) { }

  ngOnInit() {
    this.formInitialize();
    this.timeInDate = new Date();
    const dt = new Date();
    this.timeOutDate = dt.setMinutes(dt.getMinutes() + 30);
    // this.timeOutDate = new Date() + 30;
    this.Score = [{ CodeId: 1, CodeValue: "None" }, { CodeId: 2, CodeValue: "Option1" }, { CodeId: 3, CodeValue: "Option2" }];
    if (this.isView === true) {
      this.viewWash();
    }
    this.getJobType();
  }

  formInitialize() {
    this.washForm = this.fb.group({
      client: ['',],
      vehicle: ['', Validators.required],
      type: ['',],
      barcode: ['',],
      washes: ['', Validators.required],
      model: ['',],
      color: ['',],
      upcharges: ['',],
      upchargeType: ['',],
      airFreshners: ['',],
      notes: ['',],
      pastNotes: ['',]
    });
    this.getTicketNumber();
  }

  get f() {
    return this.washForm.controls;
  }

  getTicketNumber() {
    this.wash.getTicketNumber().subscribe(data => {
      if (!this.isEdit) {
        this.ticketNumber = data;
      }
    });
    this.getAllClient();
    this.getServiceType();
    this.getColor();
  }

  getWashById() {
    console.log(this.selectedData);
    this.getClientVehicle(this.selectedData?.Washes[0]?.ClientId);
    this.washForm.patchValue({
      barcode: this.selectedData?.Washes[0]?.Barcode,
      client: { id: this.selectedData?.Washes[0]?.ClientId, name: this.selectedData?.Washes[0]?.ClientName },
      vehicle: this.selectedData.Washes[0].VehicleId,
      type: this.selectedData.Washes[0].Make,
      model: this.selectedData.Washes[0].Model,
      color: this.selectedData.Washes[0].Color,
      notes: this.selectedData.Washes[0].ReviewNote,
      pastNotes: this.selectedData.Washes[0].PastHistoryNote,
      washes: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === 15)[0]?.ServiceId,
      upchargeType: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === 18)[0]?.ServiceId,
      upcharges: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === 18)[0]?.ServiceId,
      airFreshners: this.selectedData.WashItem.filter(i => Number(i.ServiceTypeId) === 19)[0]?.ServiceId,
    });
    this.ticketNumber = this.selectedData.Washes[0].TicketNumber;
    this.washItem = this.selectedData.WashItem;
    this.washItem.forEach(element => {
      if (this.additional.filter(item => item.ServiceId === element.ServiceId)[0] !== undefined) {
        this.additional.filter(item => item.ServiceId === element.ServiceId)[0].IsChecked = true;
      }
    });
  }

  vehicleChange(id) {
    this.additional.forEach(element => {
      element.IsChecked = false;
    });
    this.getMembership(id);
    this.getVehicleById(id);
  }

  getMembership(id) {
    this.wash.getMembership(+id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.membership = vehicle.VehicleMembershipDetails.ClientVehicleMembershipService;
        if (this.membership !== null) {
          this.membershipChange(+vehicle.VehicleMembershipDetails.ClientVehicleMembership.MembershipId);
          this.membership.forEach(element => {
            const additionalService = this.additional.filter(i => Number(i.ServiceId) === Number(element.ServiceId));
            if (additionalService !== undefined && additionalService.length !== 0) {
              additionalService.forEach(item => {
                this.change(item);
              });
            }
          });
        } else {
          this.washForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  membershipChange(data) {
    this.memberService = [];
    this.wash.getMembershipById(Number(data)).subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.memberService = membership.MembershipAndServiceDetail.MembershipService;
        if (this.memberService !== null) {
          const washService = this.memberService.filter(i => Number(i.ServiceTypeId) === 15);
          if (washService.length !== 0) {
            this.washService(washService[0].ServiceId);
          } else {
            this.washForm.get('washes').reset();
          }
        } else {
          this.washForm.get('washes').reset();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }


  getServiceType() {
    this.wash.getServiceType("SERVICETYPE").subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.serviceEnum = sType.Codes;
        this.getAllServices();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
//To get JobType
getJobType() {
  this.wash.getJobType().subscribe(res => {
    if (res.status === 'Success') {
      const jobtype = JSON.parse(res.resultData);
      if (jobtype.GetJobType.length > 0) {
        jobtype.GetJobType.forEach(item => {
          if (item.valuedesc === 'Wash') {
            this.jobTypeId = item.valueid;
          }
        });
      }
    }
  });
}
  getVehicleById(data) {
    this.wash.getVehicleById(data).subscribe(res => {
      if (res.status === 'Success') {
        const vehicle = JSON.parse(res.resultData);
        const vData = vehicle.Status;
        this.washForm.patchValue({
          vehicle: vData.ClientVehicleId,
          barcode: vData.Barcode,
          type: vData.VehicleMakeId,
          model: vData.VehicleModelId,
          color: vData.ColorId
        });
        this.upchargeService(vData.Upcharge);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllServices() {
    this.wash.getServices().subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.additional = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[2].CodeValue);
        this.washes = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[0].CodeValue);
        this.upcharges = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[3].CodeValue);
        this.airFreshner = serviceDetails.ServiceSetup.filter(item => item.IsActive === true && item.ServiceType === this.serviceEnum[4].CodeValue);
        this.UpchargeType = this.upcharges;
        // this.upcharges = this.upcharges.filter(item => Number(item.ParentServiceId) !== 0);
        this.additional.forEach(element => {
          element.IsChecked = false;
        });
        if (this.isEdit === true) {
          this.washForm.reset();
          this.getWashById();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getAllClient() {
    this.wash.getAllClient().subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.Client.forEach(item => {
          item.fullName = item.FirstName + '\t' + item.LastName;
        });
        console.log(client, 'client');
        this.clientList = client.Client.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
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
    this.clientId = event.id;
    this.getClientVehicle(this.clientId);
  }

  clientChange(){
    this.clientId = this.washForm.value.client.id;
  }

  change(data) {
    const temp = this.washItem.filter(item => item.ServiceId === data.ServiceId);
    if (temp.length !== 0) {
      this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted = this.washItem.filter(item => item.ServiceId === data.ServiceId)[0].IsDeleted ? false : true;
      console.log(this.washItem);
    } else {
      data.IsChecked = data.IsChecked ? false : true;
    }
  }



  getColor() {
    this.wash.getVehicleColor().subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.color = vehicle.VehicleDetails.filter(item => item.CategoryId === 30);
        this.type = vehicle.VehicleDetails.filter(item => item.CategoryId === 28);
        this.model = vehicle.VehicleDetails.filter(item => item.CategoryId === 29);
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  viewWash() {
    this.washForm.disable();
  }

  // Get Client And Vehicle Details By Barcode
  getByBarcode(barcode) {
    this.wash.getByBarcode(barcode).subscribe(data => {
      if (data.status === 'Success') {
        this.isBarcode = true;
        const wash = JSON.parse(data.resultData);
        if (wash.ClientAndVehicleDetail !== null && wash.ClientAndVehicleDetail.length > 0) {
          this.barcodeDetails = wash.ClientAndVehicleDetail[0];
          this.getClientVehicle(this.barcodeDetails.ClientId);
          setTimeout(() => {
            this.washForm.patchValue({
              client: { id: this.barcodeDetails.ClientId, name: this.barcodeDetails.FirstName + ' ' + this.barcodeDetails.LastName },
              vehicle: this.barcodeDetails.VehicleId,
              model: this.barcodeDetails.VehicleModelId,
              color: this.barcodeDetails.VehicleColor,
              type: this.barcodeDetails.VehicleMfr
            });
            this.getMembership(this.barcodeDetails.VehicleId);
          }, 200);
        } else {
          const barCode = this.washForm.value.barcode;
          this.washForm.reset();
          this.washForm.patchValue({ barcode: barCode });
          this.additional.forEach(element => {
            element.IsChecked = false;
          });
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.wash.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicle = vehicle.Status;
        if (!this.isEdit && !this.isBarcode) {
          this.washForm.patchValue({ vehicle: this.vehicle[0].VehicleId });
          this.getVehicleById(+this.vehicle[0].VehicleId);
          this.getMembership(+this.vehicle[0].VehicleId);
        }
        if (this.isEdit && this.selectedData.Washes[0] !== undefined) {
          this.washForm.patchValue({ vehicle: this.selectedData.Washes[0].VehicleId });
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  washService(data) {
    if (this.isEdit) {
      this.washItem.filter(i => Number(i.ServiceTypeId) === 15)[0].IsDeleted = true;
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 15);
        this.washItem.filter(i => Number(i.ServiceTypeId) === 15)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 15);
        const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
        if (serviceWash.length !== 0) {
          this.additionalService.push(serviceWash[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 15);
      const serviceWash = this.washes.filter(item => item.ServiceId === Number(data));
      if (serviceWash.length !== 0) {
        this.additionalService.push(serviceWash[0]);
      }
    }
    this.washForm.patchValue({ washes: +data });
    console.log(this.additionalService, this.washItem);
  }

  upchargeService(data) {
    if (this.isEdit) {
      if (this.washItem.filter(i => Number(i.ServiceTypeId) === 18)[0] !== undefined) {
        this.washItem.filter(i => Number(i.ServiceTypeId) === 18)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
        this.washItem.filter(i => Number(i.ServiceTypeId) === 18)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
        const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
        if (serviceUpcharge.length !== 0) {
          this.additionalService.push(serviceUpcharge[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 18);
      const serviceUpcharge = this.upcharges.filter(item => item.ServiceId === Number(data));
      if (serviceUpcharge.length !== 0) {
        this.additionalService.push(serviceUpcharge[0]);
      }
    }
    this.washForm.patchValue({ upcharges: +data });
    this.washForm.patchValue({ upchargeType: +data });
    console.log(this.additionalService, this.washItem);
  }

  airService(data) {
    if (this.isEdit) {
      if (this.washItem.filter(i => Number(i.ServiceTypeId) === 19)[0] !== undefined) {
        this.washItem.filter(i => Number(i.ServiceTypeId) === 19)[0].IsDeleted = true;
      }
      if (this.washItem.filter(i => Number(i.ServiceId) === Number(data))[0] !== undefined) {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
        this.washItem.filter(i => Number(i.ServiceTypeId) === 19)[0].IsDeleted = false;
      } else {
        this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
        const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
        if (serviceAir.length !== 0) {
          this.additionalService.push(serviceAir[0]);
        }
      }
    } else {
      this.additionalService = this.additionalService.filter(i => Number(i.ServiceTypeId) !== 19);
      const serviceAir = this.airFreshner.filter(item => item.ServiceId === Number(data));
      if (serviceAir.length !== 0) {
        this.additionalService.push(serviceAir[0]);
      }
    }
    this.washForm.patchValue({ airfreshners: +data });
    console.log(this.additionalService, this.washItem);
  }

  // Add/Update Wash
  submit() {
    this.submitted = true;
    if (this.washForm.invalid) {
      return;
    }
    this.additional.forEach(element => {
      if (element.IsChecked) {
        this.additionalService.push(element);
      }
    });
    const job = {
      jobId: this.isEdit ? this.selectedData.Washes[0].JobId : 0,
      ticketNumber: this.ticketNumber,
      locationId: 1,
      clientId: this.washForm.value.client.id,
      vehicleId: this.washForm.value.vehicle,
      make: this.washForm.value.type,
      model: this.washForm.value.model,//0,
      color: this.washForm.value.color,
      jobType: this.jobTypeId,
      jobDate: new Date(),
      timeIn: new Date(),
      estimatedTimeOut: new Date(),
      actualTimeOut: new Date(),
      notes: this.washForm.value.notes,
      jobStatus: 1,
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    this.washItem.forEach(element => {
      this.additionalService = this.additionalService.filter(item => item.ServiceId !== element.ServiceId);
    });
    this.jobItems = this.additionalService.map(item => {
      return {
        jobItemId: 0,
        jobId: this.isEdit ? +this.selectedData.Washes[0].JobId : 0,
        serviceId: item.ServiceId,
        commission: 0,
        price: item.Cost,
        quantity: 1,
        reviewNote: "",
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
    });
    this.washItem.forEach(element => {
      this.jobItems.push(element);
    });
    const formObj = {
      job: job,
      jobItem: this.jobItems
    };
    if (this.isEdit === true) {
      this.wash.updateWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Wash Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          // this.washForm.reset();
        }
      });
    } else {
      this.wash.addWashes(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.washForm.reset();
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }

  addVehicle() {
    this.headerData = 'Add New Vehicle';
    this.showVehicleDialog = true;
  }

  closePopupEmitVehicle(event) {
    if (event.status === 'saved') {
      this.showVehicleDialog = false;
      this.getClientVehicle(this.clientId);
    }
    this.showVehicleDialog = event.isOpenPopup;
  }

  addClient() {
    this.headerData = 'Add New Client';
    this.showClientDialog = true;
  }

  closePopupEmitClient() {    
    this.showClientDialog = false;
  }

  saveClient() {
    this.clientFormComponent.submitted = true;
    this.clientFormComponent.stateDropdownComponent.submitted = true;
    if (this.clientFormComponent.clientForm.invalid) {
      return;
    }
    this.address = [{
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientFormComponent.clientForm.value.address,
      address2: "",
      phoneNumber2: this.clientFormComponent.clientForm.value.phone2,
      isActive: true,
      zip: this.clientFormComponent.clientForm.value.zipcode,
      state: this.clientFormComponent.State,
      city: this.clientFormComponent.city,
      country: 38,
      phoneNumber: this.clientFormComponent.clientForm.value.phone1,
      email: this.clientFormComponent.clientForm.value.email,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: "",
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: 1,
      maritalStatus: 1,
      birthDate: this.isEdit ? this.selectedData.BirthDate : new Date(),
      isActive: this.clientFormComponent.clientForm.value.status == 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score == "" || this.clientFormComponent.clientForm.value.score == null) ? 0 : this.clientFormComponent.clientForm.value.score,
      noEmail: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type == "" || this.clientFormComponent.clientForm.value.type == null) ? 0 : this.clientFormComponent.clientForm.value.type
    };
    const myObj = {
      client: formObj,
      clientVehicle: null,
      clientAddress: this.address
    }
    this.client.addClient(myObj).subscribe(data => {
      if (data.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record Updated Successfully!!' });
        this.closePopupEmitClient();
        this.getAllClient();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.clientFormComponent.clientForm.reset();
      }
    });
  }
}

