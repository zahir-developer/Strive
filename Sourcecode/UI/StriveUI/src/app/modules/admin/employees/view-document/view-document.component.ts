import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-view-document',
  templateUrl: './view-document.component.html',
  styleUrls: ['./view-document.component.css']
})
export class ViewDocumentComponent implements OnInit {
  passwordForm: FormGroup;
  @Input() employeeId?: any;
  @Input() documentId?: any;
  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private employeeService: EmployeeService,) { }

  ngOnInit(): void {
    this.formInitialize();
  }

  formInitialize() {
    this.passwordForm = this.fb.group({
      password: ['']
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  viewDocument() {
    const password = this.passwordForm.value.password;
    this.employeeService.getDocumentById(this.documentId, password).subscribe( res => {
      if (res.status === 'Success') {
        const documentDetail = JSON.parse(res.resultData);
        console.log(documentDetail);
        const base64 = documentDetail.DocumentDetail.Base64Url;
        const linkSource = 'data:application/pdf;base64,' + base64;
        const downloadLink = document.createElement('a');
        const fileName = documentDetail.DocumentDetail.FileName;
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
        // const base64 = documentDetail.
        this.activeModal.close();
      }
    });
  }

}
