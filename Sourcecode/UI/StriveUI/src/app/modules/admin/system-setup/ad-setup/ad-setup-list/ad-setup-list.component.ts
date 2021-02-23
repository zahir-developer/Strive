import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-ad-setup-list',
  templateUrl: './ad-setup-list.component.html',
  styleUrls: ['./ad-setup-list.component.css']
})
export class AdSetupListComponent implements OnInit {

  adSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isLoading = true;
  search: any = '';
  searchStatus: any;
  recordCount: any;
  page: any;
  pageSize: any;
  pageSizeList: any[];
  clonedadSetupDetails: any = [];
  collectionSize: number = 0;
  Status: any;
  query = '';
  documentTypeId: any;
  pdfData: any;
  serviceDetails: any;
  constructor(private adSetup: AdSetupService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService, private getCode: GetCodeService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;

    this.Status = [{ id: 0, Value: "InActive" }, { id: 1, Value: "Active" }, { id: 2, Value: "All" }];
    this.searchStatus = "";
    this.getAlladSetupDetails();
    this.getDocumentType();
  }

  // Get All Services
  getAlladSetupDetails() {
    this.isLoading = true;
    this.adSetup.getAdSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        this.serviceDetails = JSON.parse(data.resultData);
        if (this.serviceDetails.GetAllAdSetup !== null) {
          this.adSetupDetails = this.serviceDetails.GetAllAdSetup;
        }
        if (this.adSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.adSetupDetails.forEach(ad => {
            ad.serupName = ad.Name + '' + ad.Description;
          });
          this.clonedadSetupDetails = this.adSetupDetails.map(x => Object.assign({}, x));
          this.collectionSize = Math.ceil(this.adSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
      this.spinner.hide();
    });
  }

  getDocumentType() {
    this.getCode.getCodeByCategory("DOCUMENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
        this.documentTypeId = dType.Codes.filter(i => i.CodeValue === "Ads")[0].CodeId;
      } else {
      }
    });
  }
  paginate(event) {

    this.pageSize = +this.pageSize;
    this.page = event;

    this.getAlladSetupDetails()
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;

    this.getAlladSetupDetails()
  }

  // Get Service By Id

  edit(data) {
    this.spinner.show()
    this.adSetup.getAdSetupById(data.AdSetupId).subscribe(data => {
      if (data.status === "Success") {
        this.spinner.hide()
        const sType = JSON.parse(data.resultData);
        this.selectedData = sType.GetAdSetupById;
        this.showDialog = true;

      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });


  }
  delete(data) {
    this.confirmationService.confirm('Delete Service', `Are you sure you want to delete this service? All related 
  information will be deleted and the service cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Service
  confirmDelete(data) {
    this.adSetup.deleteAdSetup(data.AdSetupId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success(MessageConfig.Admin.SystemSetup.AdSetup.Delete, 'Success!');
        this.getAlladSetupDetails();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAlladSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, serviceDetails?) {
    if (data === 'add') {
      this.headerData = 'New AdSetup';
      this.selectedData = serviceDetails;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.headerData = 'Edit AdSetup';
      this.edit(serviceDetails)
      this.isEdit = true;
    }
  }

  searchAdList(text) {
    if (text.length > 0) {
      this.adSetupDetails = this.clonedadSetupDetails.filter(item => item.serupName.toLowerCase().includes(text));
    } else {
      this.adSetupDetails = [];
      this.adSetupDetails = this.clonedadSetupDetails;
    }
  }

  download(data) {
    this.adSetup.getAdSetupById(data.AdSetupId).subscribe(data => {
      if (data.status === "Success") {
        const sType = JSON.parse(data.resultData);
        this.pdfData = sType.GetAdSetupById;
        const base64 = this.pdfData?.Base64;
        const linkSource = 'data:application/image;base64,' + base64;
        const downloadLink = document.createElement('a');
        const fileName = this.pdfData?.OriginalFileName;
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }

    })
  }

}

