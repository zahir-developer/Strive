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
  isLoading: boolean = false;
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
  fileName: any = null;
  Documents: any;
  url: any;
 
     constructor(private documentService: DocumentService, private toastr: MessageServiceToastr,
      private confirmationService: ConfirmationUXBDialogService, private getCode: GetCodeService) { }
  ngOnInit(): void {
  this.getDocumentType();
  }

  adddata(data, handbookDetails?) {
    if (data === 'add') {     
      this.selectedData = handbookDetails;
      this.showDialog = true;
    }
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getDocument();
    }
    this.showDialog = event.isOpenPopup;
  }

  
  delete() {
    this.confirmationService.confirm('Delete Document', `Are you sure you want to delete this document? All related 
  information will be deleted and the document cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete();
        }
      })
      .catch(() => { });
  }
  confirmDelete() {
    this.documentService.deleteDocument(this.documentTypeId,'EMPLOYEEHANDBOOK').subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Deleted Successfully' });
        this.fileName = null;
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
  downloadPDF() {
    const base64 = this.document.Document.Base64;
    const linkSource = 'data:application/pdf;base64,' + base64;
    const downloadLink = document.createElement('a');
    const fileName = this.fileName;
    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
}
  getDocument() {
    this.isLoading = true;
    this.documentService.getDocument(this.documentTypeId, "EMPLOYEEHANDBOOK").subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const documentDetails = JSON.parse(data.resultData);
        this.document = documentDetails.Document;
        this.Documents = this.document?.Document;
        this.fileName = this.document?.Document?.FileName;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    }, (err) => {
      this.isLoading = false;
    });
  }

  
}
