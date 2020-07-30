import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ClientService } from 'src/app/shared/services/data-service/client.service';

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
  searchByName = '';
  searchById = '';
  isEdit: boolean;
  isTableEmpty: boolean;
  constructor(private client: ClientService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.getAllClientDetails();

  }
  getAllClientDetails() {
    this.client.getClient().subscribe(data => {
      if (data.status === 'Success') {
        const client = JSON.parse(data.resultData);
        this.clientDetails = client.Clients;
        if (this.clientDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
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
  add(data, clientDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Client';
      this.showDialog = true;
      this.selectedData = clientDetails;
      this.isEdit = false;      
    } else {
      this.headerData = 'Edit Client';
        this.selectedData = clientDetails;
        this.isEdit = true;
        this.showDialog = true;
    }
  }  
}
