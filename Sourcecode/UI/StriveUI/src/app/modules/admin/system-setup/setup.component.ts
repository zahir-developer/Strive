import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.css']
})
export class SetupComponent implements OnInit {

    setupForm : FormGroup;
    sysSetup : any;

  constructor(private fb: FormBuilder,private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {

    this.sysSetup = ["Location Setup","Service Setup","Product Setup","Vendor Setup"];
    this.setupForm = this.fb.group({
        systemSetup: ['', Validators.required],
    });
  }

  submit(){
      if(this.setupForm.value.systemSetup === this.sysSetup[0])
      {
          this.loadLocationSetup();
      }
      else if(this.setupForm.value.systemSetup === this.sysSetup[1])
      {
          this.loadServiceSetup();
      }
      else if(this.setupForm.value.systemSetup === this.sysSetup[2])
      {
          this.loadProductSetup();
      }
      else if(this.setupForm.value.systemSetup === this.sysSetup[3])
      {
          this.loadVendorSetup();
      }
      // else
      // {
      //       this.setupForm.reset();
      // }
  }

  loadLocationSetup(): void {
    this.router.navigate([`/admin/location`], { relativeTo: this.route });
  }

  loadServiceSetup(): void {
    this.router.navigate([`/admin/service`], { relativeTo: this.route });
  }

  loadProductSetup(): void {
    this.router.navigate([`/admin/product`], { relativeTo: this.route });
  }

  loadVendorSetup(): void {
    this.router.navigate([`/admin/vendor`], { relativeTo: this.route });
  }

}