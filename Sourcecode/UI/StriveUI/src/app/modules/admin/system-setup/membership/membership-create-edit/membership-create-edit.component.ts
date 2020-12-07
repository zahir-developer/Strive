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
  submitted: boolean;
  PriceServices: any = [];
  constructor(private fb: FormBuilder, private toastr: MessageServiceToastr, private member: MembershipService) { }

  ngOnInit() {
    this.status = [{ CodeId: 0, CodeValue: "Active" }, { CodeId: 1, CodeValue: "Inactive" }];
    this.formInitialize();
  }

  formInitialize() {
    this.membershipForm = this.fb.group({
      membershipName: ['', Validators.required],
      service: ['',],
      washes: ['', Validators.required],
      upcharge: ['', Validators.required],
      status: ['',],
      price: ['', Validators.required],
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

  get f() {
    return this.membershipForm.controls;
  }

  calculate(data, name) {
    if (name === 'washes') {
      this.PriceServices = this.PriceServices.filter(i => i.ServiceTypeName !== 'Washes');
      this.PriceServices.push(this.service.filter(i => +i.ServiceId === +data)[0]);
      let price = 0;
      this.PriceServices.forEach(element => {
        price += +element.Price;
      });
      this.membershipForm.get('price').patchValue(price.toFixed(2));
    } else if (name === 'upcharge') {
      this.PriceServices = this.PriceServices.filter(i => i.ServiceTypeName !== 'Upcharges');
      this.PriceServices.push(this.service.filter(i => +i.ServiceId === +data)[0]);
      let price = 0;
      this.PriceServices.forEach(element => {
        price += +element.Price;
      });
      this.membershipForm.get('price').patchValue(price.toFixed(2));
    }
    console.log(this.PriceServices);
  }

  onItemSelect(data) {
    this.PriceServices.push(this.service.filter(i => +i.ServiceId === +data.item_id)[0]);
    let price = 0;
    this.PriceServices.forEach(element => {
      price += +element.Price;
    });
    this.membershipForm.get('price').patchValue(price.toFixed(2));
    console.log(this.PriceServices, price);
  }

  onItemDeSelect(data) {
    this.PriceServices = this.PriceServices.filter(i => +i.ServiceId !== +data.item_id);
    let price = 0;
    this.PriceServices.forEach(element => {
      price += +element.Price;
    });
    this.membershipForm.get('price').patchValue(price.toFixed(2));
    console.log(this.PriceServices, price);
  }

  getMembershipById() {
    let service = [];
    this.membershipForm.patchValue({
      membershipName: this.selectedData.Membership.MembershipName,
      notes: this.selectedData.Membership.Notes,
      price: this.selectedData?.Membership?.Price?.toFixed(2),
      status: this.selectedData.Membership.Status === true ? 0 : 1
    });
    if (this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 15)[0] !== undefined) {
      this.membershipForm.get('washes').patchValue(this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 15)[0].ServiceId);
      this.PriceServices.push(this.service.filter(i => +(i.ServiceId) === +this.membershipForm.value.washes)[0]);
    }
    if (this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 18)[0] !== undefined) {
      this.membershipForm.get('upcharge').patchValue(this.selectedData.MembershipService.filter(i => Number(i.ServiceTypeId) === 18)[0].ServiceId);
      this.PriceServices.push(this.service.filter(i => +(i.ServiceId) === +this.membershipForm.value.upcharge)[0]);
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
      this.memberService.forEach(element => {
        this.PriceServices.push(this.service.filter(i => +(i.ServiceId) === +element.item_id)[0]);
      });
    }
    let price = 0;
    this.PriceServices.forEach(element => {
      price += +element.Price;
    });
    this.membershipForm.get('price').patchValue(price.toFixed(2));
  }

  bindUpcharge(data) {
    this.membershipForm.patchValue({
      upcharge: +data
    });
    this.calculate(data, 'upcharge');
  }

  // Add/Update Membership
  submit() {
    this.submitted = true;
    if (this.membershipForm.invalid) {
      return;
    }
    let memberService = [];
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
      if (memberService !== null) {
        ServiceObj = memberService.map(item => {
          return {
            membershipServiceId: item.MembershipServiceId ? item.MembershipServiceId : 0,
            membershipId: item.MembershipId ? item.MembershipId : this.selectedData.Membership.MembershipId ?
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
    }
    const washType = this.selectedData?.MembershipService.filter(i => i.ServiceTypeId === 15);
    if(washType !== undefined){
      if(Number(washType[0].ServiceId) !== Number(this.membershipForm.value.washes)){
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
      const washDelete = {
        membershipServiceId: Number(washType[0].MembershipServiceId),
        membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
        serviceId: Number(washType[0].ServiceId),
        isActive: true,
        isDeleted: true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
      ServiceObj.push(wash);
      ServiceObj.push(washDelete);
      }
    }
    const upchargeType = this.selectedData?.MembershipService.filter(i => i.ServiceTypeId === 18);
    if(upchargeType !== undefined){
      if(Number(upchargeType[0].ServiceId) !== Number(this.membershipForm.value.upcharge)){        
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
      const upchargeDelete = {
        membershipServiceId: Number(upchargeType[0].MembershipServiceId),
        membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
        serviceId: Number(upchargeType[0].ServiceId),
        isActive: true,
        isDeleted: true,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      };
      ServiceObj.push(upcharge);
      ServiceObj.push(upchargeDelete);
      }
    }
    const membership = {
      membershipId: this.isEdit ? this.selectedData.Membership.MembershipId : 0,
      membershipName: this.membershipForm.value.membershipName === '' ? "None/UNK" : this.membershipForm.value.membershipName,
      price: this.membershipForm.value.price ? this.membershipForm.value.price : 0,
      notes: this.membershipForm.value.notes ? this.membershipForm.value.notes : '',
      locationId: localStorage.getItem('empLocationId'),
      isActive: Number(this.membershipForm.value.status) === 0 ? true : false,
      isDeleted: false,
      createdBy: 1,
      createdDate: this.isEdit ? this.selectedData.Membership.StartDate : new Date(),
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

