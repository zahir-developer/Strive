import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';


@Component({
  selector: 'app-employee-hand-book',
  templateUrl: './employee-hand-book.component.html',
  styleUrls: ['./employee-hand-book.component.css']
})
export class EmployeeHandBookComponent implements OnInit {
  dropdownSettings: IDropdownSettings = {};
  employeeRoles: any;
  isLoading: boolean;
  checkListDetails: any;
  isTableEmpty: boolean;
  selectedData: boolean = false;
  isEdit: boolean;
  checkListName: any;
  RoleId = [];
  Roles: any;
  roles: any;
  employeeRole: any;
  employeeRoleId = [];
  rollList: any;
  checklistAdd: boolean;
  showDialog: boolean;
  documentTypeId: any;
  document: any;
  fileName: any;
  Documents: any;
  url: any;
 
     constructor(private documentService: DocumentService, private toastr: MessageServiceToastr, 
      private sanitizer:DomSanitizer,
      private confirmationService: ConfirmationUXBDialogService, private getCode: GetCodeService) { }
  ngOnInit(): void {
  this.getDocumentType();
  }

  adddata(data, handbookDetails?) {
    if (data === 'add') {
     
      this.selectedData = handbookDetails;
      this.showDialog = true;
    } else {
      this.selectedData = handbookDetails;
      this.showDialog = true;
    }
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
    }
    this.showDialog = event.isOpenPopup;
  }
  checlist(){
    this.checklistAdd = true;
    this.selectedData = false;


  }
  checlistcancel(){
    this.checkListName = '';
    this.RoleId = [];
    this.checklistAdd = false;
  }


  
  delete() {
    this.confirmationService.confirm('Delete Service', `Are you sure you want to delete this service? All related 
  information will be deleted and the service cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete();
        }
      })
      .catch(() => { });
  }
  confirmDelete() {
    this.documentService.deleteDocument(this.documentTypeId,'TERMSANDCONDITION').subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Deleted Successfully' });
        this.getDocument();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }
  getDocumentType(){
    this.getCode.getCodeByCategory("DOCUMENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
          this.documentTypeId = dType.Codes.filter(i => i.CodeValue === "EmployeeHandBook")[0].CodeId;
          console.log(this.documentTypeId);
          this.getDocument();

      
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }
documentOpen(data) {
  const url:any =  this.sanitizer.bypassSecurityTrustUrl(data);
 window.open(data)
 this.url = data
}
getSantizeUrl() {
  return this.sanitizer.bypassSecurityTrustResourceUrl(this.Documents.FilePath);
  }
  getDocument() {
    this.isLoading = true;
    this.documentService.getDocument(this.documentTypeId, "EMPLOYEEHANDBOOK").subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const documentDetails = JSON.parse(data.resultData);
        this.document = documentDetails.Document;

        if (this.document.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.Documents = this.document.Document;
          this.isTableEmpty = false;
  //         this.document.then(response => {
  //   // response.data -> response data base64 encoded
  //   // decoding the data via atob()
  //   const byteArray = new Uint8Array(atob(response.FileName).split('').map(char => char.charCodeAt(0)));
  //   return new Blob([byteArray], {type: 'application/pdf'});
  // })
  // .then(blob => {
  //   // Here is your URL you can use
  //   const url = window.URL.createObjectURL(blob);

  //   // i.e. display the PDF content via iframe
  //   document.querySelector("iframe").src = url;
  //   console.log(url)
  // });

        }
        
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }

 
  add(data, serviceDetails?) {
    if (data === 'add') {
     
      this.isEdit = false;
    } else {
      this.selectedData = serviceDetails.ChecklistId;
      this.isEdit = true;
      this.checklistAdd = false;


    }
  }
  cancel(){
    this.selectedData = false;

  }

  
}
