import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { DocumentService } from 'src/app/shared/services/data-service/document.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';


@Component({
  selector: 'app-employee-hand-book',
  templateUrl: './employee-hand-book.component.html'
})
export class EmployeeHandBookComponent implements OnInit {
  dropdownSettings: IDropdownSettings = {};
  employeeRoles: any;
  isLoading = false;
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
  document: any = [];
  fileName: any = null;
  Documents: any;
  url: any;
  sortColumn: { sortBy: string; sortOrder: string; };
  actionType: string;
  header: string;

  constructor(private documentService: DocumentService, private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService, private getCode: GetCodeService,
    private codeValueService: CodeValueService) { }
  ngOnInit(): void {
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.EmployeeHandbook,
      sortOrder: ApplicationConfig.Sorting.SortOrder.EmployeeHandbook.order
    }
    this.isLoading = false;
    this.getDocumentType();
  }

  adddata(data, handbookDetails?) {
if(data == 'edit'){
this.actionType = "Edit";
this.header = "Edit Employee Handbook";
  this.getById(handbookDetails)

}else{
  this.header = "Create Employee Handbook";

  this.actionType = "Add";
  this.showDialog = true;

}
  
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getDocument();
    }
    this.showDialog = event.isOpenPopup;
  }


  delete(Id) {
    this.confirmationService.confirm('Delete Document', `Are you sure you want to delete this document? 
    All related information will be deleted and the document cannot be retrieved`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(+Id);
        }
      })
      .catch(() => { });
  }
  confirmDelete(Id) {
    this.spinner.show();
    this.documentService.deleteDocumentById(Id, 'EMPLOYEEHANDBOOK').subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.EmployeeHandBook.Delete, 'Success!');
        this.fileName = null;
        this.getDocument();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  getDocumentType() {
    const documentTypeVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValue.documentType);
    console.log(documentTypeVaue, 'cached value ');
    if (documentTypeVaue.length > 0) {
      this.documentTypeId = documentTypeVaue.filter(i => i.CodeValue === ApplicationConfig.CodeValue.Ads)[0].CodeId;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.documentType).subscribe(data => {
        if (data.status === "Success") {
          const dType = JSON.parse(data.resultData);
          this.documentTypeId = dType.Codes.filter(i => i.CodeValue === ApplicationConfig.CodeValue.EmployeeHandBook)[0].CodeId;
          this.getDocument();
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
        , (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
    }
  }

  getDocument() {
    this.isLoading = true;
    this.documentService.getAllDocument(this.documentTypeId).subscribe(data => {
      if (data.status === 'Success') {
        this.isLoading = false;

        const documentDetails = JSON.parse(data.resultData);
        this.document = documentDetails.Document;
        this.Documents = this.document?.Document;
        this.sort(ApplicationConfig.Sorting.SortBy.EmployeeHandbook)

      } else {
        this.isLoading = false;

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      this.isLoading = false;
    });
  }
  getById(documents) {
    this.documentService.getDocumentById(documents.DocumentId, 'EMPLOYEEHANDBOOK').subscribe(res => {
      if (res.status === 'Success') {
        const documentDetails = JSON.parse(res.resultData);
        if (documentDetails.Document !== null) {
          this.selectedData =  documentDetails.Document.Document;
          this.showDialog = true;

        }
        else{
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        }
      }
    },
      (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );
  }
  downloadPDF(documents) {
    this.documentService.getDocumentById(documents.DocumentId, 'EMPLOYEEHANDBOOK').subscribe(res => {
      if (res.status === 'Success') {
        const documentDetails = JSON.parse(res.resultData);
        if (documentDetails.Document !== null) {
          const details = documentDetails.Document.Document;
          this.selectedData =  documentDetails.Document.Document;
          const base64 = details.Base64;
          const linkSource = 'data:application/pdf;base64,' + base64;
          const downloadLink = document.createElement('a');
          const fileName = details.OriginalFileName;
          downloadLink.href = linkSource;
          downloadLink.download = fileName;
          downloadLink.click();
        }
      }
    },
      (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    );
  }
  
  sort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.EmployeeHandbook.order
    }
    this.sorting(this.sortColumn)
    this.selectedCls(this.sortColumn)

  }
  sorting(sortColumn) {
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
    let property = sortColumn.sortBy;
    this.document.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  }
  changesort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.sorting(this.sortColumn)

  }
  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }

}
