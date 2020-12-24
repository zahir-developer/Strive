import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { AdSetupService } from 'src/app/shared/services/data-service/ad-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { PaginationConfig } from 'src/app/shared/services/Pagination.config';

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
  page: any ;
  pageSize :any;
  pageSizeList: any[];

  collectionSize: number = 0;
  Status: any;
  query = '';
  documentTypeId: any;
  constructor(private adSetup: AdSetupService, 
    private toastr: ToastrService,  private getCode: GetCodeService,
    private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.isLoading = false;
    this.page= PaginationConfig.page;
    this.pageSize = PaginationConfig.TableGridSize;
    this.pageSizeList = PaginationConfig.Rows;

    this.Status = [{id : 0,Value :"InActive"}, {id :1 , Value:"Active"}, {id :2 , Value:"All"}];
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
        const serviceDetails = JSON.parse(data.resultData);
        this.adSetupDetails = serviceDetails.GetAllAdSetup;
        if (this.adSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.adSetupDetails.length / this.pageSize) * 10;

          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  
  getDocumentType(){
    this.getCode.getCodeByCategory("DOCUMENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
          this.documentTypeId = dType.Codes.filter(i => i.CodeValue === "Ads")[0].CodeId;
          console.log(this.documentTypeId);

      
      } else {
      }
    });
  }
  paginate(event) {
    
    this.pageSize= +this.pageSize;
    this.page = event ;
    
    this.getAlladSetupDetails()
  }
  paginatedropdown(event) {
    this.pageSize= +event.target.value;
    this.page =  this.page;
    
    this.getAlladSetupDetails()
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
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
    this.adSetup.deleteAdSetup(2).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAlladSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
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
      this.selectedData = serviceDetails;
      this.isEdit = true;
      this.showDialog = true;
    }
  }
  


}

