import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown/multiselect.model';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { CheckListService } from 'src/app/shared/services/data-service/check-list.service';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

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
  constructor(private employeeService: EmployeeService,
    private checkListSetup: CheckListService,
   // private httpClient: HttpClient,
    private confirmationService: ConfirmationUXBDialogService,
    // private toastr: ToastrService
     ) { }

  ngOnInit(): void {
    this.getAllRoles();
this.getAllcheckListDetails();
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
// Get All Services
getAllcheckListDetails() {
  // this.httpClient.get('assets/json/checkList.json').toPromise()
  // .then((data: any) => {
  //   this.checkListDetails = data.Checklist;
  //   console.log(this.checkListDetails)
  // });
 this.isLoading = true;
  this.checkListSetup.getCheckListSetup().subscribe(data => {
   this.isLoading = false;
    if (data.status === 'Success') {
      const serviceDetails = JSON.parse(data.resultData);
      this.checkListDetails = serviceDetails.GetChecklist;
      console.log(data)
      if (this.checkListDetails.length === 0) {
        this.isTableEmpty = true;
      } else {
        this.isTableEmpty = false;
      }
    } else {
     // this.toastr.error('Communication Error', 'Error!');
    }
  });
}
onRoleDeSelect(event) {
  if (this.RoleId ) {
    this.employeeRole = this.employeeRole.filter(item => item.item_id !== event.item_id);
    this.employeeRole.push(event);
   
      this.roles = this.employeeRole;
    
  } 

}
  getAllRoles() {
    this.employeeService.getAllRoles().subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.rollList = roles.EmployeeRoles
        this.employeeRoles = roles.EmployeeRoles.map( item => {
          return {
            item_id: item.RoleMasterId,
            item_text: item.RoleName
          };
        });
        this.dropdownSettings = {
          singleSelection: false,
          defaultOpen: false,
          idField: 'item_id',
          textField: 'item_text',
          selectAllText: 'Select All',
          unSelectAllText: 'UnSelect All',
          itemsShowLimit: 3,
          allowSearchFilter: false
        };
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
   
      
    this.checkListSetup.deleteCheckListSetup(data.ChecklistId).subscribe(res => {
      if (res.status === "Success") {
      //  this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllcheckListDetails();
      } else {
       // this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  add(data, serviceDetails?) {
    if (data === 'add') {
     
      this.isEdit = false;
      this.submit(serviceDetails);
    } else {
      this.selectedData = serviceDetails.ChecklistId;
      this.isEdit = true;
      this.checklistAdd = false;


    }
  }
  cancel(){
    this.selectedData = false;

  }
//   onItemSelect(employeeRoles){
// console.log(employeeRoles)

//   this.employeeRoleId.push(employeeRoles.item_id);

//   }
  submit(data) {
   // this.submitted = true;
    // if (this.serviceSetupForm.invalid) {
    //   return;
    // }
    const formObj = {
   checkList: { ChecklistId: data.ChecklistId ? data.ChecklistId : 0,
    Name: data.Name ? data.Name : this.checkListName,
    RoleId:data.RoleId ? data.RoleId : this.RoleId ,
    IsDeleted : false,
    IsActive: true,}
       
        
    };
    if (data.ChecklistId) {
      this.checkListSetup.addCheckListSetup(formObj).subscribe(data => {
        if (data.status === 'Success') {   
        //  this.toastr.success('Record Updated Successfully!!', 'Success!'); 
              
          this.getAllcheckListDetails();
          this.selectedData = false;

        } else {
          //this.toastr.error('Communication Error', 'Error!');
        }
      });
    } else {
      this.checkListSetup.addCheckListSetup(formObj).subscribe(data => {
        if (data.status === 'Success') { 
         // this.toastr.success('Record Saved Successfully!!', 'Success!');  
          this.getAllcheckListDetails();
          this.checkListName = '';
              this.RoleId = [];
     
        } else {
        //  this.toastr.error('Communication Error', 'Error!');
         
        }
      });
    }


    
  }
  
}