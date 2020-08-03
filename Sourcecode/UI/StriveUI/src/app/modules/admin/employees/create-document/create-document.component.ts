import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder } from '@angular/forms';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.css']
})
export class CreateDocumentComponent implements OnInit {
  isPassword: boolean;
  fileName: any = '';
  fileUploadformData: any;
  passwordForm: FormGroup;
  @Input() employeeId?: any;
  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.isPassword = false;
    this.formInitialize();
  }

  formInitialize() {
    this.passwordForm = this.fb.group({
      password: [''],
      confirm: ['']
    });
  }

  closeModal() {
    this.activeModal.close();
  }

  protectPassword(event) {
    console.log(event, 'event');
    if (event.target.checked === true) {
      this.isPassword = true;
    } else {
      this.isPassword = false;
    }
  }

  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('filepaths');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;
      let fileReader: any;
      fileReader = new FileReader();
      fileReader.onload = function (fileLoadedEventTigger) {
        let textAreaFileContents: any;
        textAreaFileContents = document.getElementById('filepaths');
        textAreaFileContents.innerHTML = fileLoadedEventTigger.target.result;
      };
      fileReader.readAsDataURL(fileToLoad);
      setTimeout(() => {
        let fileTosaveName: any;
        fileTosaveName = fileReader.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
      }, 5000);
    }
  }

  uploadDocument() {
    const finalObj = [];
    const uploadbj = {
      documentId: 0,
      employeeId: this.employeeId,
      fileName: this.fileName,
      filePath: 'D:\\Upload\\',
      password: this.passwordForm.value.confirm,
      createdDate: '2020 - 07 - 21T12: 41: 47.395Z',
      modifiedDate: '2020 - 07 - 21T12: 41: 47.395Z',
      isActive: true,
      base64Url: this.fileUploadformData
    };
    finalObj.push(uploadbj);
    console.log(finalObj, 'obj');
    this.employeeService.uploadDocument(finalObj).subscribe(res => {
      console.log(res, 'uploadDcument');
      if (res.status === 'Success') {
        this.activeModal.close(true);
        // this.getAllDocument();
      }
    });
  }

  getAllDocument() {
    this.employeeService.getAllDocument(this.employeeId).subscribe(res => {
      console.log(res, 'allDocument');
      if (res.status === 'Success') {
        const document = JSON.parse(res.resultData);
      }
    });
  }

}
