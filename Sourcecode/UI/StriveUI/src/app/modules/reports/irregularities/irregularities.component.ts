import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';

@Component({
  selector: 'app-irregularities',
  templateUrl: './irregularities.component.html'
})
export class IrregularitiesComponent implements OnInit, AfterViewInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  date = new Date();
  maxDate = new Date();
  locationId: any;
  dailyTip = [];
  fileType: any;
  page = 1;
  pageSize = 25;
  collectionSize: number;
  totalTip = 0;
  tipAmount: number;
  totalHours: number = 0;
  fileTypeEvent: boolean = false;
  tips: number =0;
  constructor(private cd: ChangeDetectorRef, private reportService: ReportsService,
    private excelService: ExcelService, private spinner: NgxSpinnerService,
    private toastr :ToastrService) { }

  ngOnInit(): void {
    getIrregulrities()
  }
  export(){

  }

  getIrregulrities(){

  }

  refresh() {
   
  }
}

