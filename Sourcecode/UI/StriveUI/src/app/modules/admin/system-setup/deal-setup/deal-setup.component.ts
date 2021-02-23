import { Component, OnInit } from '@angular/core';
import { DealsService } from 'src/app/shared/services/data-service/deals.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

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
  
  constructor(private Deals :DealsService,
    private toastr: ToastrService,) { }
  ngOnInit(): void {
    this.isLoading = false;
    this.getDeals();
  }

  adddata(data,selectedData?) {
 this.header = data
    this.selectedData = selectedData[0];
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
       this.DealsDetails = Deals.GetAllDeals
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }

  
  

}
