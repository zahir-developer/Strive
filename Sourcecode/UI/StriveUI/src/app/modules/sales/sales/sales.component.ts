import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { EditItemComponent } from './edit-item/edit-item.component';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SalesComponent implements OnInit {
  services: any;
  filteredItem = [];
  constructor(private membershipService: MembershipService, private salesService: SalesService,
    private confirmationService: ConfirmationUXBDialogService, private modalService: NgbModal) { }
  ItemName = '';
  ticketNumber = '';
  washes = [];
  additionalService = [];
  details = [];
  ngOnInit(): void {
    this.getAllService();
  }
  getAllService() {
    this.membershipService.getMembershipService().subscribe(data => {
      if (data.status === 'Success') {
        const services = JSON.parse(data.resultData);
        if (services.ServicesWithPrice !== null && services.ServicesWithPrice.length > 0) {
          this.services = services.ServicesWithPrice.map(item => {
            return {
              id: item.ServiceId,
              name: item.ServiceName.trim()
            };
          });
          console.log(this.services);
        }
      }
    });
  }
  selectedItem(event) {
    console.log(event.name);
  }
  getDetailByTicket() {
    if (this.ticketNumber !== undefined || this.ticketNumber !== '') {
      this.salesService.getItemByTicketNumber(+this.ticketNumber).subscribe(data => {
        console.log(data, 'ticket');
        if (data.status === 'Success') {
          const itemList = JSON.parse(data.resultData);
          this.washes = itemList.Status.filter(item => item.ServiceType === 'Washes');
          this.details = itemList.Status.filter(item => item.ServiceType === 'Details');
          this.additionalService = itemList.Status.filter(item => item.ServiceType === 'Additional Services');
        }
      });
    }
  }
  filterItem(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.services) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredItem = filtered;
  }
  deleteItem(data) {
    this.confirmationService.confirm('Delete Item', `Are you sure you want to delete the selected Item?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete location
  confirmDelete(data) {
    this.salesService.deleteItemById(+data.JobId).subscribe(res => {
      if (res.status === 'Success') {
        this.getDetailByTicket();
console.log('deleted');
      } else {

      }
    });
  }
  opengiftcard() {
    document.getElementById('Giftcardpopup').style.width = '300px';
    document.getElementById('creditcardpopup').style.width = '0';
  }

  closegiftcard() {
    document.getElementById('Giftcardpopup').style.width = '0';
  }

  opencreditcard() {
    document.getElementById('creditcardpopup').style.width = '300px';
    document.getElementById('Giftcardpopup').style.width = '0';
  }

  closecreditcard() {
    document.getElementById('creditcardpopup').style.width = '0';
  }
  editItem(event) {
    const jobId = event.JobId;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef =  this.modalService.open(EditItemComponent, ngbModalOptions);
    modalRef.componentInstance.jobId = jobId;
    modalRef.componentInstance.isModal = true;
  }
}
