import { Component, OnInit } from '@angular/core';
import { DealsService } from 'src/app/shared/services/data-service/deals.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-deal-setup',
  templateUrl: './deal-setup.component.html',
  styleUrls: ['./deal-setup.component.css']
})
export class DealSetupComponent implements OnInit {
  isLoading = false;
  isTableEmpty: boolean;
  showDialog: boolean;
  selectedData: any;
  header: any;
  DealsDetails: any;
  dealStatus = false;
  actionType: string;
  constructor(
    private Deals: DealsService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
  ) { }
  ngOnInit(): void {
    this.isLoading = false;
    this.getDeals();
  }

  adddata() {
    if (this.DealsDetails.length === 2) {
      this.toastr.warning(MessageConfig.Admin.SystemSetup.Deal.DealLimit, 'Warning');
      return;
    }
    this.actionType = 'Add';
    this.showDialog = true;
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
    }
    this.showDialog = event.isOpenPopup;
  }
  getDeals() {
    this.isLoading = true;
    this.Deals.getDealSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const Deals = JSON.parse(data.resultData);
        this.DealsDetails = Deals.GetAllDeals;
        this.DealsDetails.forEach( item => {
          if (item.StartDate === '0001-01-01T00:00:00') {
            item.StartDate = 'None';
          }
          if (item.EndDate === '0001-01-01T00:00:00') {
            item.EndDate = 'None';
          }
        });
        if (this.DealsDetails.length > 0) {
          this.dealStatus = true;
        } else {
          this.dealStatus = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }

  dealChange(event) {
    console.log(event, 'deal');
  }

  delete(data) {
    this.confirmationService.confirm('Delete Deals', `Are you sure you want to delete this deal? All related 
    information will be deleted and the client cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  confirmDelete(data) {
    const dealObj = {
      dealId: data.DealId,
      dealName: data.DealName,
      timePeriod: data.TimePeriod,
      deals: true,
      startDate: data.StartDate === '0001-01-01T00:00:00' ? null : data.StartDate,
      endDate: data.EndDate === '0001-01-01T00:00:00' ? null : data.EndDate,
      isActive: true,
      isDeleted: true
    };
    const finalObj = {
      deal : dealObj
    };
    this.Deals.addDealsSetup(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        this.getDeals();
      }
    });
  }


  editDeal(data) {
    this.actionType = 'Edit';
    this.selectedData = data;
    this.showDialog = true;
  }



}
