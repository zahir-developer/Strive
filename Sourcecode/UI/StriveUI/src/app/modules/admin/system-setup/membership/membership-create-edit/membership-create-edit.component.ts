import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as moment from 'moment';
@Component({
  selector: 'app-membership-create-edit',
  templateUrl: './membership-create-edit.component.html',
  styleUrls: ['./membership-create-edit.component.css']
})
export class MembershipCreateEditComponent implements OnInit {
  membershipForm: FormGroup;
  dropdownSettings: IDropdownSettings = {};
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  model: any;
  status: any;
  service: any;
  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr, private member: MembershipService) { }

  ngOnInit() {
    this.status = [{ CodeId: 0, CodeValue: "Active" }, { CodeId: 1, CodeValue: "InActive" }];
    this.formInitialize();
    if (this.isEdit === true) {
      this.membershipForm.reset();
      this.getMembershipById();
    }
  }

  formInitialize() {
    this.membershipForm = this.fb.group({
      membershipName: ['',],
      service: ['',],
      status: ['',],
      price: ['',],
      notes: ['',]
    });    
    this.membershipForm.patchValue({status : 0});
    this.getMembershipService();
  }

  // Get Membership Services
  getMembershipService() {
    this.member.getMembershipService().subscribe(data => {
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.service = membership.ServicesWithPrice;
        const newArr = [];
        this.service.forEach((item) => {
          if (newArr.findIndex(i => i.ServiceId == item.ServiceId) === -1) {
            newArr.push(item);
          }
        });
        this.service = newArr;
        this.service = this.service.map(item => {
          return {
            item_id: item.ServiceId,
            item_text: item.ServiceName
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
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  getPrice(data) {
    this.membershipForm.get('price').patchValue(this.service.filter(item => item.ServiceId === Number(data))[0].Price);
  }

  getMembershipById() {
    this.membershipForm.patchValue({
      membershipName: this.selectedData.MembershipName,
      service: this.selectedData.ServiceId,
      notes: this.selectedData.Notes,
      status: this.selectedData.Status === true ? 0 : 1
    });
    this.getPrice(this.selectedData.ServiceId);
  }

  check(data) {
    console.log(data);
  }

  // Add/Update Membership
  submit() {
    const ServiceObj = this.membershipForm.value.service.map(item => {
      return {
        serviceId: item.item_id
      };
    });
    const membership = {
      membershipId: this.isEdit ? this.selectedData.MembershipId : 0,
      membershipName: this.membershipForm.value.membershipName,
      serviceId: ServiceObj,
      locationId: 1,
      isActive: Number(this.membershipForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const clientMembership = {
      clientMembershipId: this.isEdit ? this.selectedData.ClientMembershipId : 0,
      clientVehicleId: 1,
      locationId: 1,
      membershipId: this.isEdit ? this.selectedData.MembershipId : 0,
      startDate: moment(new Date()).format('MM-DD-YYYY'),
      endDate: moment(new Date(new Date().getFullYear(), (new Date().getMonth() + 1), new Date().getDate())).format('MM-DD-YYYY'),
      status: Number(this.membershipForm.value.status) === 0 ? true : false,
      notes: this.membershipForm.value.notes,
      isActive: Number(this.membershipForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const formObj = {
      membership: membership,
      clientMembershipDetails: clientMembership
    };
    if (this.isEdit === true) {
      this.member.updateMembership(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Membership Updated Successfully' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.membershipForm.reset();
        }
      });
    } else {
      this.member.addMembership(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Membership Saved Successfully' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          this.membershipForm.reset();
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

