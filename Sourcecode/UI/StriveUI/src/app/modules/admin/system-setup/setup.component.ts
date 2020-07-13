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

    this.loadLocationSetup();
  }

  

  loadLocationSetup(): void {
    this.router.navigate([`/admin/setup/location`], { relativeTo: this.route });
  }  
}