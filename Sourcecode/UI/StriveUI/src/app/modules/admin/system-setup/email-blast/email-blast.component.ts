import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
// import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
// import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
// import { NgxSpinnerService } from 'ngx-spinner';

import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-email-blast',
  templateUrl: './email-blast.component.html'
})
export class EmailBlastComponent implements OnInit {
  fileExportType:any;
  daterangepickerModel: any;
  todayDate = new Date();
  startDate: any;
  dateRange: any = [];
  currentWeek: any;
  endDate: any;
  fileTypeEvent: boolean = false;
  fileType: number;
  isMembership: any;
  isLoading = false;
  ExportDate: any = [];
  // isTableEmpty: boolean;
  // showDialog: boolean;
  // selectedData: any;
  // header: any;
  // DealsDetails: any;
  searchStatus = false;
  ExportType = 0;
  Status: any;
  // actionType: string;
  constructor(
    private excelService: ExcelService,
    private Client: ClientService,
    // private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    // private confirmationService: ConfirmationUXBDialogService,
    private datePipe: DatePipe,
  ) { }
  ngOnInit(): void {
    this.isLoading = false;
    this.Status = [{ id: false, Value: 'No' }, { id: true, Value: 'Yes' }, { id: '', Value: 'All' }];
    // this.getDeals(); 
    this.fileExportType = [
      { id: 0, name: 'Select' },
      { id: 1, name: 'CSV (comma delimited)' },
      { id: 2, name: 'Excel 97 - 2003' },
    ]; 
    this.weeklyDateAssign();
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 6;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    this.endDate = this.endDate.setDate(this.startDate.getDate() + 6);
    this.endDate = new Date(moment(this.endDate).format());
    this.daterangepickerModel = [this.startDate, this.endDate];
  }

  getFileType(event) {
    this.fileTypeEvent = true;
    this.fileType = +event.target.value;
  }
  
  onValueChange(event) {
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
    }
  }
  
  ExportMailDetails(){
    const obj = {
      fromDate:moment(this.startDate).format('YYYY-MM-DD'),
      toDate: moment(this.endDate).format('YYYY-MM-DD') ,
      isMembership: this.searchStatus
    };
   
        // const client = JSON.parse(data.resultData);
        // this.ExportDate = client;

        const fileType = Number(this.ExportType);
   
        switch (fileType) {
          case 0:{            
             this.toastr.error("Select type to export", 'Error!');
          }
          case 1: {
           
            this.Client.getCSVClientList(obj).subscribe(data => {
               if (data.status === 'Success') {                
               const csvData = JSON.parse(data.resultData);
                this.excelService.exportAsCSVFile(csvData.ClientCSVExport, 'PromotionEmail' +
                  this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') );
    
    
                return data;
              }
          
          }, (err) => {
              this.toastr.error(MessageConfig.CommunicationError, 'Error!');
            });

            break;
          }
          case 2: {
            this.Client.getClientList(obj).subscribe(data => {
            if (data) {
              this.download(data, 'excel', 'PromotionEmail_' +
              this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy'));
              return data;
            }
        }, (err) => {
            this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          });
      
            break;
          }
         
          default: {
            return;
          }
        }
      
   
   
  }

  download(data: any, type, fileName = 'Excel') {
    let format: string;
    format = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    let a: HTMLAnchorElement;
    a = document.createElement('a');
    document.body.appendChild(a);
    const blob = new Blob([data], { type: format });
    const url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
  }

}
