import { Component, OnInit } from '@angular/core';
import jsPDF from 'jspdf';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';

@Component({
  selector: 'app-terms-and-conditions',
  templateUrl: './terms-and-conditions.component.html',
  styleUrls: ['./terms-and-conditions.component.css']
})
export class TermsAndConditionsComponent implements OnInit {
  fileName: any = null;
  isLoading: any;
  showDialog: any;
  document: any = [];
  isEdit: any;
  selectedData: any;
  documentTypeId: any;

  constructor(private documentService: DocumentService, private toastr: MessageServiceToastr,
    private confirmationService: ConfirmationUXBDialogService, private getCode: GetCodeService) { }

  ngOnInit() {
    this.getDocumentType();
  }

  getDocumentType() {
    this.getCode.getCodeByCategory("DOCUMENTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const dType = JSON.parse(data.resultData);
        this.documentTypeId = dType.Codes.filter(i => i.CodeValue === "TermsAndCondition")[0].CodeId;
        console.log(this.documentTypeId);
        this.getDocument();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }

  getDocument() {
    this.isLoading = true;
    this.documentService.getAllDocument(this.documentTypeId).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const documentDetails = JSON.parse(data.resultData);
        this.document = documentDetails.Document;
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    }, (err) => {
      this.isLoading = false;
    });
  }


  delete(Id) {
    this.confirmationService.confirm('Delete Document', `Are you sure you want to delete this Document? All related 
    information will be deleted and the Document cannot be retrieved`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(+Id);
        }
      })
      .catch(() => { });
  }

  confirmDelete(Id) {
    this.documentService.deleteDocumentById(Id,'TERMSANDCONDITION').subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Document Deleted Successfully' });
        this.fileName= null;
        this.getDocument();
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    });
  }

  addData(data) {
    if (data === 'add') {
      this.selectedData = [];
      this.showDialog = true;
    }
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getDocument();
    }
    this.showDialog = event.isOpenPopup;
  }

  downloadPDF(documents) {
        const base64 = documents.Base64;
        const linkSource = 'data:application/pdf;base64,' + base64;
        const downloadLink = document.createElement('a');
        const fileName = documents.OriginalFileName;
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
  }
  

}
