import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ClientStatementComponent } from '../client-statement/client-statement.component';
import { ClientHistoryComponent } from '../client-history/client-history.component';
import { ClientFormComponent } from 'src/app/shared/components/client-form/client-form.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { AddActivityComponent } from '../../gift-card/add-activity/add-activity.component';
import { AddActivityAdditionalComponent } from '../../gift-card/add-activity-additional/add-activity-additional.component';
// import { AddCreditCardComponent } from '../../credit-card/add-credit-card.component';
import { duration } from 'moment';
import { debug } from 'console';


@Component({
  selector: 'app-client-create-edit',
  templateUrl: './client-create-edit.component.html'
})
export class ClientCreateEditComponent implements OnInit {
  @ViewChild(ClientFormComponent) clientFormComponent: ClientFormComponent;
  address: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  @Input() isView?: any;
  @Input() isAdd?: any;
  // cardDetails = [];
  vehicleDetails = [];
  activityDetails = [];
  historyData = [];
  statementData = [];
  vehicleDet = [];
  isTableEmpty: boolean;
  headerData: string;
  headerActivity: string;
  selectedVehicle: any;
  showVehicleDialog: boolean;
  clientId: number = 0;
  page = 1;
  pageSize = 3;
  activityPage = 1;
  activityPageSize = 3;
  cardPage = 1;
  cardPageSize = 3;
  collectionSize: number = 0;
  activityCollectionSize: number = 0;
  cardCollectionSize: number = 0;
  deleteIds = [];
  additionalService: any = [];
  vehicleNumber: number;
  sort = { column: 'VehicleNumber', descending: true };
  sortColumn: { column: string; descending: boolean; };
  employeeId: number;
  showDialog = false;
  vehicleDetail: any;
  isVehicleEdit: boolean;
  // clonedCardDetails=[];
  clonedVehicleDetails = [];
  clonedAccountDetails = [];
  showActivity : boolean;
  TotalAmount:number = 0;
  constructor(private toastr: ToastrService, private client: ClientService,
    private confirmationService: ConfirmationUXBDialogService, private spinner: NgxSpinnerService,
    private modalService: NgbModal, private vehicle: VehicleService) { }

  ngOnInit() {
    this.showActivity = true;
    this.employeeId = +localStorage.getItem('empId');
    this.isVehicleEdit = false;
    this.getService();
    this.vehicle.addVehicle = undefined;
    if (this.isEdit === true) {
      this.getClientVehicle(this.selectedData.ClientId);
      this.getClientActivity(this.selectedData.ClientId);
      //this.getClientCreditCard(this.selectedData.ClientId);
    }
    else {
      this.vehicleNumber = 1;
    }
  }

  change(data) {
    this.showActivity = data;
  }

  //Get Activity By ClientId
  getClientActivity(id) {
    this.client.getActivityByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const activityData = JSON.parse(data.resultData);
        this.TotalAmount = activityData.CreditAccountDetail?.CreditAmount[0]?.CreditAmount;
        if(activityData.CreditAccountDetail.ClientActivityHistoryViewModel)
        {
          this.activityDetails=activityData.CreditAccountDetail.ClientActivityHistoryViewModel;

          this.clonedAccountDetails.push(this.activityDetails);
          this.activityCollectionSize = Math.ceil(this.activityDetails.length / this.activityPageSize) * 10;
        }
        if(activityData.CreditAccountDetail.ClientActivityStatementViewModel)
        {
          this.statementData = activityData.CreditAccountDetail.ClientActivityStatementViewModel;
        }
        if(activityData.CreditAccountDetail.ClientActivityPaymentHistoryViewModel)
        {
          this.historyData = activityData.CreditAccountDetail.ClientActivityPaymentHistoryViewModel;

        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );

  }
  /*
  // Get CreditCard By ClientId
  getClientCreditCard(id) {
    this.client.getClientCreditCard(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.cardDetails = vehicle.Status;
        this.clonedCardDetails = this.cardDetails.map(x => Object.assign({}, x));
        this.cardCollectionSize = Math.ceil(this.cardDetails.length / this.cardPageSize) * 10;
       
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );
  }
  */
  // Get Vehicle By ClientId
  getClientVehicle(id) {
    this.vehicle.getVehicleByClientId(id).subscribe(data => {
      if (data.status === 'Success') {
        const vehicle = JSON.parse(data.resultData);
        this.vehicleDetails = vehicle.Status;
        this.clonedVehicleDetails = this.vehicleDetails.map(x => Object.assign({}, x));

        if (this.vehicleDetails.length === 0) {
          this.isTableEmpty = true;
          this.vehicleNumber = 1;
        } else {
          this.vehicleDetails.forEach(item => {
            item.isAddedVehicle = true;
            if (this.vehicleDetails?.length > 0) {
              for (let i = 0; i < this.vehicleDetails.length; i++) {
                this.vehicleDetails[i].VehicleModel === 'None' ? this.vehicleDetails[i].VehicleModel = 'Unk'
                  : this.vehicleDetails[i].VehicleModel;
              }
            }
          });
          let len = this.vehicleDetails.length;
          this.vehicleNumber = this.vehicleDetails.length + 1;
          this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );
  }

  // Add/Update Client
  submit() {
    this.clientFormComponent.submitted = true;
    this.clientFormComponent.clientForm.controls.status.enable();

    if (this.clientFormComponent.clientForm.invalid) {
      return;
    }
    if (this.clientFormComponent.ClientEmailAvailable == true && this.isEdit === false) {
      this.toastr.warning(MessageConfig.Client.emailExist, 'Warning!');

      return;
    }

    if (this.clientFormComponent.ClientNameAvailable == true && this.isEdit === false) {
      this.toastr.warning(MessageConfig.Client.clientExist, 'Warning!');

      return;
    }

    this.address = [{
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      clientAddressId: this.isEdit ? this.selectedData.ClientAddressId : 0,
      address1: this.clientFormComponent.clientForm.value.address ? this.clientFormComponent.clientForm.value.address : null,
      address2: null,
      phoneNumber2: this.clientFormComponent.clientForm.value.phone2,
      isActive: true,
      zip: this.clientFormComponent.clientForm.value.zipcode ? this.clientFormComponent.clientForm.value.zipcode : null,
      state: this.clientFormComponent.State ? this.clientFormComponent.State : null,
      city: this.clientFormComponent.city === 0 ? null : this.clientFormComponent.city,
      country: null,
      phoneNumber: this.clientFormComponent.clientForm.value.phone1,
      email: this.clientFormComponent.clientForm.value.email,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date()
    }]
    const formObj = {
      clientId: this.isEdit ? this.selectedData.ClientId : 0,
      firstName: this.clientFormComponent.clientForm.value.fName,
      middleName: null,
      lastName: this.clientFormComponent.clientForm.value.lName,
      gender: null,
      maritalStatus: null,
      birthDate: this.isEdit ? this.selectedData.BirthDate === '0001-01-01T00:00:00' ? null : this.selectedData.BirthDate : null,
      isActive: Number(this.clientFormComponent.clientForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedData.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
      notes: this.clientFormComponent.clientForm.value.notes,
      recNotes: this.clientFormComponent.clientForm.value.checkOut,
      score: (this.clientFormComponent.clientForm.value.score === '' || this.clientFormComponent.clientForm.value.score == null) ?
        0 : this.clientFormComponent.clientForm.value.score,
      isCreditAccount: this.clientFormComponent.clientForm.value.creditAccount,
      clientType: (this.clientFormComponent.clientForm.value.type === '' || this.clientFormComponent.clientForm.value.type == null) ?
        0 : this.clientFormComponent.clientForm.value.type,
      amount: this.clientFormComponent.clientForm.value.amount,
      authId: this.selectedData.AuthId,
      locationId : this.clientFormComponent.clientForm.value.location == '0' ? null : this.clientFormComponent.clientForm.value.location
    };
    debugger;
    this.clonedAccountDetails[0] = this.clonedAccountDetails[0].filter(s=>s.isAdded === true || s.isModified === true);
    const myObj = {
      client: formObj,
      clientVehicle: this.vehicleDet.length === 0 ? null : this.vehicleDet,
      clientAddress: this.address,
      CreditAccountHistory:  this.clonedAccountDetails.length > 0 ? this.clonedAccountDetails[0] : null,
      //  ClientCardDetails: this.clonedCardDetails.length > 0 ? this.clonedCardDetails[0] : null,
      token: null,
      password: ''
    }
    if (this.isEdit === true) {
      this.spinner.show();
      this.client.updateClient(myObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.deleteIds.forEach(element => {
            this.vehicle.deleteVehicle(element.VehicleId).subscribe(res => {
              if (res.status === 'Success') {
                this.toastr.success(MessageConfig.Admin.Vehicle.Delete, 'Success!');
              } else {
                this.toastr.error(MessageConfig.CommunicationError, 'Error!');
              }
            });
          })
          this.toastr.success(MessageConfig.Client.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.clientFormComponent.clientForm.reset();
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.client.addClient(myObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Client.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.clientFormComponent.clientForm.reset();
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
    }
  }
  Validate() {
    if (this.isEdit === true) {
      this.submit();
    }
    else {
      this.client.ClientEmailCheck(this.clientFormComponent.clientForm.value.email).subscribe(res => {
        if (res.status === 'Success') {
          const sameEmail = JSON.parse(res.resultData);
          if (sameEmail.EmailIdExist === true) {
            this.clientFormComponent.ClientEmailAvailable = true;
            this.toastr.warning(MessageConfig.Client.emailExist, 'Warning!');
          } else {
            this.clientFormComponent.ClientEmailAvailable = false;
            this.submit();
          }
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }
  // Add Activity Popup
  addActivity() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddActivityAdditionalComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
    modalRef.componentInstance.header = "Add Activity";

    modalRef.componentInstance.userActivity.subscribe((receivedEntry) => {
      const activityObj = {
        clientId: this.isEdit ? this.selectedData.ClientId : 0,
        CreditAccountHistoryId: receivedEntry.CreditAccountHistoryId == undefined ? 0 : receivedEntry.CreditAccountHistoryId,
        Amount: parseFloat(receivedEntry.amount),
        Comments:receivedEntry.notes,
        IsActive: true,
        isDeleted: false,
        createdBy: this.employeeId,
        createdDate: new Date(),
        isAdded: receivedEntry.CreditAccountHistoryId == undefined ? true : false,
        isModified: receivedEntry.CreditAccountHistoryId !== undefined ? true : false,
      };

      this.clonedAccountDetails.length>0 ? this.clonedAccountDetails[0].push(activityObj) : this.clonedAccountDetails.push([activityObj]);
      if (this.clonedAccountDetails.length > 0) {
        this.activityDetails = [];
        this.clonedAccountDetails[0].forEach(item => {
          this.activityDetails.push(item);
        });
      }

      this.activityCollectionSize = Math.ceil(this.activityDetails.length / this.activityPageSize) * 10;
      this.TotalAmount = parseFloat((this.TotalAmount + parseFloat(receivedEntry.amount)).toFixed(2));
      modalRef.close();

    })
  }

  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      var oExists = this.clonedVehicleDetails.find(x => x.VehicleId === this.vehicle.vehicleValue.ClientVehicleId);
      if (this.vehicle.vehicleValue.ClientVehicleId == undefined) {
        this.clonedVehicleDetails.push(this.vehicle.vehicleValue);
      }
      else if (oExists.length == 0) {
        this.clonedVehicleDetails.push(this.vehicle.vehicleValue);
      }
      /*else{

        this.clonedVehicleDetails.forEach(item => {
          if (item.VehicleId === this.vehicle.vehicleValue.ClientVehicleId) {
            item.VehicleColor = this.vehicle.vehicleValue.VehicleColor;
            item.VehicleMfr = this.vehicle.vehicleValue.VehicleMfr;
            item.VehicleModel = this.vehicle.vehicleValue.VehicleModel;
            item.Barcode = this.vehicle.vehicleValue.Barcode;
            item.ClientId = this.vehicle.vehicleValue.ClientId;
            item.MembershipName = this.vehicle.vehicleValue.MembershipName;
            item.Upcharge = this.vehicle.vehicleValue.Upcharge;
            item.VehicleNumber = this.vehicle.vehicleValue.VehicleNumber;
          }
        });
        // var oIndex = this.clonedVehicleDetails.findIndex(x => x.VehicleId === this.vehicle.vehicleValue.ClientVehicleId);
        // this.clonedVehicleDetails[oIndex].Barcode = this.vehicle.vehicleValue.Barcode;
        // this.clonedVehicleDetails[oIndex].ClientId = this.vehicle.vehicleValue.ClientId;
        // this.clonedVehicleDetails[oIndex].MembershipName = this.vehicle.vehicleValue.MembershipName;
        // this.clonedVehicleDetails[oIndex].Upcharge = this.vehicle.vehicleValue.Upcharge;
        // this.clonedVehicleDetails[oIndex].VehicleColor = this.vehicle.vehicleValue.VehicleColor;
        // this.clonedVehicleDetails[oIndex].VehicleMfr = this.vehicle.vehicleValue.VehicleMfr;
        // this.clonedVehicleDetails[oIndex].VehicleModel = this.vehicle.vehicleValue.VehicleModel;
        // this.clonedVehicleDetails[oIndex].VehicleNumber = this.vehicle.vehicleValue.VehicleNumber;
      }*/
      if (this.clonedVehicleDetails.length > 0) {
        this.vehicleDetails = [];
        this.clonedVehicleDetails.forEach(item => {
          this.vehicleDetails.push(item);
        });
      }
      let len = this.vehicleDetails.length;
      this.vehicleNumber = Number(this.vehicleDetails.length) + 1;
      if (this.vehicle.addVehicle != undefined) {
        this.vehicleDet.push(this.vehicle.addVehicle);
      }
      this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
      this.showVehicleDialog = false;
    } else if (event.status === 'edit') {
      this.vehicleDetails.forEach(item => {
        if (item.VehicleId === this.vehicle.vehicleValue.ClientVehicleId) {
          item.VehicleColor = this.vehicle.vehicleValue.VehicleColor;
          item.VehicleMfr = this.vehicle.vehicleValue.VehicleMfr;
          item.VehicleModel = this.vehicle.vehicleValue.VehicleModel;
          item.Barcode = this.vehicle.vehicleValue.Barcode;
          item.MembershipName = this.vehicle.vehicleValue.MembershipName;
        }
      });
    }
    this.vehicleDetails.forEach(item => {
      if (item.hasOwnProperty('VehicleId')) {
        item.isAddedVehicle = true;
      } else {
        item.isAddedVehicle = false;
      }
    });
    this.showVehicleDialog = event.isOpenPopup;
  }
  delete(data) {
    if (this.isView) {
      return;
    }
    this.confirmationService.confirm('Delete Vehicle', `Are you sure you want to delete this vehicle? All related 
    information will be deleted and the vehicle cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Vehicle 
  confirmDelete(data) {
    this.vehicleDetails = this.vehicleDetails.filter(item => item !== data);
    let len = this.vehicleDetails.length;
    this.vehicleNumber = Number(this.vehicleDetails.length) + 1;
    this.vehicleDet = this.vehicleDet.filter(item => item.Barcode !== data.Barcode);
    this.toastr.success(MessageConfig.Admin.Vehicle.Delete, 'Success!');
    this.collectionSize = Math.ceil(this.vehicleDetails.length / this.pageSize) * 10;
    if (data.ClientVehicleId !== 0) {
      this.deleteIds.push(data);
    }
  }
  deleteActivity(data) {
    if (this.isView) {
      return;
    }
    this.confirmationService.confirm('Delete Activity', `Are you sure you want to delete this Activity? All related 
    information will be deleted and the Activity cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDeleteActivity(data);
        }
      })
      .catch(() => { });
  }

  // Delete Vehicle 
  confirmDeleteActivity(receivedEntry) {
    if(this.clonedAccountDetails.length > 0){
      var oExists = this.clonedAccountDetails[0].findIndex(x => x.CreditAccountHistoryId === receivedEntry.CreditAccountHistoryId);

      this.clonedAccountDetails[0][oExists].IsActive  = false;
      this.clonedAccountDetails[0][oExists].isDeleted = true;
      this.clonedAccountDetails[0][oExists].updatedBy = this.employeeId;
      this.clonedAccountDetails[0][oExists].updatedDate =  new Date();
      this.clonedAccountDetails[0][oExists].isModified = true;      
    }
    this.TotalAmount = parseFloat((this.TotalAmount - parseFloat(receivedEntry.Amount)).toFixed(2));
    this.activityDetails = this.activityDetails.filter(s=>s.isDeleted === false || s.isDeleted === undefined );
  }
  editActivity(activity) {
    console.log(activity, 'activity');
    if (!activity.hasOwnProperty('CreditAccountHistoryId')) {
      return;
    }

    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };

    const modalRef = this.modalService.open(AddActivityAdditionalComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
    modalRef.componentInstance.CreditAccountHistoryId = activity.CreditAccountHistoryId;
    modalRef.componentInstance.amount = activity.Amount;
    modalRef.componentInstance.comments = activity.Comments;
    modalRef.componentInstance.header = "Edit Activity";

    modalRef.componentInstance.userActivity.subscribe((receivedEntry) => {
      const activityObj = {
        clientId:this.selectedData.ClientId,
        CreditAccountHistoryId: receivedEntry.CreditAccountHistoryId == undefined ? 0 : receivedEntry.CreditAccountHistoryId,
        Amount:receivedEntry.amount,
        Comments:receivedEntry.notes,
        IsActive: true,
        isDeleted: false,
        createdBy: this.employeeId,
        createdDate: receivedEntry.CreatedDate,
        updatedBy: this.employeeId,
        updatedDate: new Date(),
        isAdded: receivedEntry.CreditAccountHistoryId == undefined ? true : false,
        isModified: receivedEntry.CreditAccountHistoryId !== undefined ? true : false,
      };
      if(this.clonedAccountDetails.length > 0){
        var oExists = this.clonedAccountDetails[0].findIndex(x => x.CreditAccountHistoryId === receivedEntry.CreditAccountHistoryId);
        this.clonedAccountDetails[0][oExists].Amount = receivedEntry.amount;
        this.clonedAccountDetails[0][oExists].Comments = receivedEntry.notes;
        this.clonedAccountDetails[0][oExists].IsActive  = true;
        this.clonedAccountDetails[0][oExists].isDeleted = false;
        this.clonedAccountDetails[0][oExists].updatedBy = this.employeeId;
        this.clonedAccountDetails[0][oExists].updatedDate =  new Date();
        this.clonedAccountDetails[0][oExists].isModified = true;
        if (this.clonedAccountDetails.length > 0) {
          this.activityDetails = [];
          this.clonedAccountDetails[0].forEach(item => {
            this.activityDetails.push(item);
          });
        }
      }else{
        this.clonedAccountDetails.push([activityObj]);
        if (this.clonedAccountDetails.length > 0) {
          this.activityDetails = [];
          this.clonedAccountDetails[0].forEach(item => {
            this.activityDetails.push(item);
          });
        }
      }

      modalRef.close();
    });
  }

  editVehicle(vehicle) {
    console.log(vehicle, 'vehicle');
    if (!vehicle.hasOwnProperty('VehicleId')) {
      return;
    }

    this.vehicle.getVehicleById(vehicle.VehicleId).subscribe(res => {

      if (res.status === 'Success') {
        const vehicleDetail = JSON.parse(res.resultData);
        this.selectedVehicle = vehicleDetail.Status;
        this.headerData = 'Edit Vehicle';
        this.vehicleDetail = this.selectedVehicle;
        this.isVehicleEdit = true;
        this.isView = false;
        this.showVehicleDialog = true;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    );
  }

  // Add New Vehicle
  add() {
    this.isVehicleEdit = false;
    this.headerData = 'Add New vehicle';
    this.showVehicleDialog = true;
    this.clientId = this.selectedData.ClientId;
  }

  openStatement() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(ClientStatementComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
    modalRef.componentInstance.statementData = this.statementData;
  }

  openHistory() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(ClientHistoryComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId;
    // modalRef.componentInstance.historyData = this.historyData;
    modalRef.componentInstance.historyData = this.activityDetails;
  }

  getService() {
    this.vehicle.getAllServiceDetail().subscribe(res => {
      if (res.status === 'Success') {
        const membership = JSON.parse(res.resultData);
        this.additionalService = membership.AllServiceDetail.filter(item => item.ServiceTypeName === 'Additional Services');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    );
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }
  addCreditCard(){
    /*
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddCreditCardComponent, ngbModalOptions);
    modalRef.componentInstance.clientId = this.selectedData.ClientId; 
    modalRef.componentInstance.header = "Add New Credit Card";
  
    modalRef.componentInstance.userCreditCard.subscribe((receivedEntry) => {
     const CreditCardObj = {
        clientId: this.isEdit ? this.selectedData.ClientId : 0,
        Id: receivedEntry.Id == undefined ? 0 : receivedEntry.Id,
        CardType: receivedEntry.cardType,
        CardNumber:receivedEntry.CardNumber,
        ExpiryDate:receivedEntry.ExpiryDate,
        IsActive: true,
        isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      };     
      
      this.clonedCardDetails.length>0 ? this.clonedCardDetails[0].push(CreditCardObj) : this.clonedCardDetails.push([CreditCardObj]);
      if (this.clonedCardDetails.length > 0) {
        this.cardDetails = [];
        this.clonedCardDetails[0].forEach(item => {
          this.cardDetails.push(item);
        });
      }

      this.cardCollectionSize = Math.ceil(this.cardDetails.length / this.cardPageSize) * 10;
      modalRef.close();
     
      })*/
  }
  /*
    deleteCreditCard(data) {
      debugger;
      if (this.isView) {
        return;
      }
      this.confirmationService.confirm('Delete Credit Card', `Are you sure you want to delete this Credit Card details? All related 
      information will be deleted and the CreditCard cannot be retrieved?`, 'Yes', 'No')
        .then((confirmed) => {
          if (confirmed === true) {
            this.confirmCreditCard(data);
          }
        })
        .catch(() => { });
    }
  
    // Delete Vehicle 
    confirmCreditCard(receivedEntry) {
      if(this.clonedCardDetails.length > 0){ 
        var oExists = this.clonedCardDetails[0].findIndex(x => x.Id === receivedEntry.Id);
       
        this.clonedCardDetails[0][oExists].IsActive  = false;
        this.clonedCardDetails[0][oExists].isDeleted = true;
        this.clonedCardDetails[0][oExists].updatedBy = this.employeeId;
        this.clonedCardDetails[0][oExists].updatedDate =  new Date();
      }
    
    }
    */
  editCreditCard(card) {
    /*
     if (!card.hasOwnProperty('Id')) {
       return;
     };
     
     const ngbModalOptions: NgbModalOptions = {
       backdrop: 'static',
       keyboard: false,
       size: 'lg'
     };
 
     const modalRef = this.modalService.open(AddCreditCardComponent, ngbModalOptions);
     modalRef.componentInstance.clientId = this.selectedData.ClientId;
     modalRef.componentInstance.Id = card.Id;
     modalRef.componentInstance.cardType = card.CardType;
     modalRef.componentInstance.CardNumber = card.CardNumber;
     modalRef.componentInstance.ExpiryDate = card.ExpiryDate;
     modalRef.componentInstance.header = "Edit Credit Card ";
 
     modalRef.componentInstance.userCreditCard.subscribe((receivedEntry) => {
       const activityObj = {
         clientId:this.selectedData.ClientId,
         Id: receivedEntry.Id == undefined ? 0 : receivedEntry.Id,
         CardType:receivedEntry.cardType,
         CardNumber:receivedEntry.CardNumber,
         ExpiryDate:receivedEntry.ExpiryDate,
         IsActive: true,
         isDeleted: false,
       createdBy: this.employeeId,
       createdDate: new Date(),
       };
       if(this.clonedCardDetails.length > 0){ 
         var oExists = this.clonedCardDetails[0].findIndex(x => x.CreditAccountHistoryId === receivedEntry.CreditAccountHistoryId);
         this.clonedCardDetails[0][oExists].CardType = receivedEntry.cardType;
         this.clonedCardDetails[0][oExists].CardNumber = receivedEntry.CardNumber;
         this.clonedCardDetails[0][oExists].ExpiryDate = receivedEntry.ExpiryDate;
         this.clonedCardDetails[0][oExists].IsActive  = true;
         this.clonedCardDetails[0][oExists].isDeleted = false;
         this.clonedCardDetails[0][oExists].updatedBy = this.employeeId;
         this.clonedCardDetails[0][oExists].updatedDate =  new Date();
         if (this.clonedCardDetails.length > 0) {
           this.cardDetails = [];
           this.clonedCardDetails[0].forEach(item => {
             this.cardDetails.push(item);
           });
         }
       }else{ 
         this.clonedCardDetails.push([activityObj]);
         if (this.clonedCardDetails.length > 0) {
           this.cardDetails = [];
           this.clonedCardDetails[0].forEach(item => {
             this.cardDetails.push(item);
           });
         }
       }
      
       modalRef.close();
     });
     */
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }
}

