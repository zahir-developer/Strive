import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { MembershipService } from 'src/app/shared/services/data-service/membership.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import * as moment from 'moment';
import * as _ from 'underscore';
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
  washes: any;
  upchargeType: any;
  additional: any;
  memberService: any = [];
  additionalService: any;
  patchedService: any;
  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr, private member: MembershipService) { }

  ngOnInit() {
    this.status = [{ CodeId: 0, CodeValue: "Active" }, { CodeId: 1, CodeValue: "InActive" }];
    this.formInitialize();
  }

  formInitialize() {
    this.membershipForm = this.fb.group({
      membershipName: ['',],
      service: ['',],
      washes: ['',],
      upcharge: ['',],
      status: ['',],
      price: ['',],
      notes: ['',]
    });
    this.membershipForm.patchValue({ status: 0 });
    this.getMembershipService();
  }

  // Get Membership Services
  getMembershipService() {
    this.member.getMembershipService().subscribe(data => {
      if (data.status === 'Success') {
        const membership = JSON.parse(data.resultData);
        this.service = membership.ServicesWithPrice;
        this.washes = this.service.filter(item => item.ServiceTypeName === "Washes");
        this.upchargeType = this.service.filter(item => item.ServiceTypeName === "Upcharges");
        this.additionalService = this.service.filter(item => item.ServiceTypeName === "Additional Services");
        this.additional = this.additionalService.map(item => {
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
        if (this.isEdit === true) {
          this.membershipForm.reset();
          this.getMembershipById();
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  // getPrice(data) {
  //   this.membershipForm.get('price').patchValue(this.service.filter(item => item.ServiceId === Number(data))[0].Price);
  // }

  getMembershipById() {
    let service = [];
    this.membershipForm.patchValue({
      membershipName: this.selectedData.Membership.MembershipName,
      notes: this.selectedData.Membership.Notes,
      price: this.selectedData?.Membership.Price,
      status: this.selectedData.Membership.Status === true ? 0 : 1
    });
    if (this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 15)[0] !== undefined) {
      this.membershipForm.get('washes').patchValue(this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 15)[0].ServiceId);
    }
    if (this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 18)[0] !== undefined) {
      this.membershipForm.get('upcharge').patchValue(this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 18)[0].ServiceId);
    }
    if (this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 17).length !== 0) {
      this.patchedService = this.selectedData?.MembershipService.filter(item => Number(item.ServiceTypeId) === 17);
      const serviceIds = this.selectedData?.MembershipService.filter(item => Number(item.ServiceTypeId) === 17).map(item => item.ServiceId);
      const memberService = serviceIds.map((e) => {
        const f = this.additionalService.find(a => a.ServiceId === e);
        return f ? f : 0;
      });
      this.memberService = memberService.map(item => {
        return {
          item_id: item.ServiceId,
          item_text: item.ServiceName
        };
      });

    }
  }

  check(data) {
  }

  // Add/Update Membership
  submit() {
    let memberService = [];
    const wash = {
      membershipServiceId: 0,
      membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
      serviceId: Number(this.membershipForm.value.washes),
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const upcharge = {
      membershipServiceId: 0,
      membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
      serviceId: Number(this.membershipForm.value.upcharge),
      isActive: true,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    if (this.isEdit === true && this.patchedService !== undefined) {
      const r = this.patchedService.filter((elem) => this.memberService.find(({ item_id }) => elem.ServiceId === item_id));
      r.forEach(item => item.isDeleted = false);
      const r1 = this.memberService.filter((elem) => !this.patchedService.find(({ ServiceId }) => elem.item_id === ServiceId));
      r1.forEach(item => {
        item.MembershipServiceId = 0;
        item.isDeleted = false,
        item.MembershipId = this.selectedData.Membership.MembershipId,
          item.ServiceId = item.item_id;
      });
      const r2 = this.patchedService.filter((elem) => !this.memberService.find(({ item_id }) => elem.ServiceId === item_id));
      r2.forEach(item => item.isDeleted = true);
      memberService = r.concat(r1).concat(r2);
    } else {
      memberService = this.memberService;
    }
    let ServiceObj = [];
    if (this.isEdit === false) {
      ServiceObj = this.memberService.map(item => {
        return {
          membershipServiceId: 0,
          membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
          serviceId: item.item_id,
          isActive: true,
          isDeleted: false,
          createdBy: 1,
          createdDate: new Date(),
          updatedBy: 1,
          updatedDate: new Date()
        };
      });
    } else {
      ServiceObj = memberService.map(item => {
        return {
          membershipServiceId: item.MembershipServiceId ? item.MembershipServiceId : 0,
          membershipId: item.MembershipId ?  item.MembershipId : this.selectedData.Membership.MembershipId ?
           this.selectedData.Membership.MembershipId : 0,
          serviceId: item.ServiceId ? item.ServiceId : item.item_id ? item.item_id : 0,
          isActive: true,
          isDeleted: item?.isDeleted ? item?.isDeleted : false,
          createdBy: 1,
          createdDate: new Date(),
          updatedBy: 1,
          updatedDate: new Date()
        };
      });
    }
    ServiceObj.push(wash);
    ServiceObj.push(upcharge);
    const membership = {
      membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
      membershipName: this.membershipForm.value.membershipName,
      price: this.membershipForm.value.price ? this.membershipForm.value.price : 0,
      notes: this.membershipForm.value.notes ? this.membershipForm.value.notes : 0,
      locationId: 1,
      isActive: Number(this.membershipForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date()
    };
    const formObj = {
      membership,
      membershipService: ServiceObj
    };
    if (this.isEdit === true) {
      this.member.updateMembership(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Membership Updated Successfully' });
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
          // this.membershipForm.reset();
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

