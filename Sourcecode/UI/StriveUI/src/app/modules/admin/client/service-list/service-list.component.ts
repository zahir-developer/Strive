import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.css']
})
export class ServiceListComponent implements OnInit {
  @Input() historyGrid?: any;
  @Input() ticketNumber?: any;
  page = 1;
  pageSize = 10;
  collectionSize: number;
  Date: any;
  constructor(
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
    this.collectionSize = Math.ceil(this.historyGrid.length / this.pageSize) * 10;
    if(this.historyGrid.length > 0){
      this.Date = this.historyGrid[0]?.Date;
    }
   
  }

  closeHistoryModel() {
    this.activeModal.close();
  }

}
