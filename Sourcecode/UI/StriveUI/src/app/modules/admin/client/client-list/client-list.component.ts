import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  clientDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isView: boolean;
  selectedClient: any;
  search: any='';
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  isLoading = true;
  constructor(private client: ClientService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.getAllClientDetails();
  }

  // Get All Client
  getAllClientDetails() {
    this.isLoading = true;
    this.client.getClient().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const client = JSON.parse(data.resultData);
        this.clientDetails = client.Client;
        if (this.clientDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.clientDetails.length/this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  clientSearch(){
    this.page = 1;
    const obj = {
       clientName: this.search
    }
    this.client.ClientSearch(obj).subscribe(data => {
      if (data.status === 'Success') {
        const client = JSON.parse(data.resultData);
        this.clientDetails = client.ClientSearch;
        console.log(this.clientDetails);
        if (this.clientDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.clientDetails.length/this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  delete(data) {
    this.confirmationService.confirm('Delete Client', `Are you sure you want to delete this client? All related 
    information will be deleted and the client cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Client
  confirmDelete(data) {
    this.client.deleteClient(data.ClientId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllClientDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllClientDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, clientDet?) {
    if (data === 'add') {
      this.headerData = 'Add New Client';
      this.showDialog = true;
      this.selectedData = clientDet;
      this.isEdit = false;
      this.isView = false;
    } else {
      this.getClientById(data, clientDet);
    }
  }

  // Get Client By Id
  getClientById(data, client) {
    this.spinner.show();
    this.client.getClientById(client.ClientId).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        this.selectedClient = client.Status[0];
        if (data === 'edit') {
          this.headerData = 'Edit Client';
          this.selectedData = this.selectedClient;
          this.isEdit = true;
          this.isView = false;
          this.showDialog = true;
        } else {
          this.headerData = 'View Client';
          this.selectedData = this.selectedClient;
          this.isEdit = true;
          this.isView = true;
          this.showDialog = true;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
}
