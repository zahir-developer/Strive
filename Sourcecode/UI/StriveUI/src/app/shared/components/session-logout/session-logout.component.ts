import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/common-service/auth.service';
import { LandingService } from '../../services/common-service/landing.service';
import { GetCodeService } from '../../services/data-service/getcode.service';
import { UserDataService } from '../../util/user-data.service';

@Component({
  selector: 'app-session-logout',
  templateUrl: './session-logout.component.html',
  styleUrls: ['./session-logout.component.css']
})
export class SessionLogoutComponent implements OnInit {
  @Input() dialogDisplay = false;
  @Input() header: string;
  @Input() dialogType: string;
  @Input() countdown?: number;
  @Output() closeDialog = new EventEmitter();
  authentication: FormGroup;
  submitted = false;
  constructor(
    private user: UserDataService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private fb: FormBuilder,
    private landing: LandingService,
    private getCodeService: GetCodeService
  ) { }

  ngOnInit(): void {
    this.authentication = this.fb.group({
      password: ['', Validators.required],
      username: ['', Validators.required]
    });
  }

  /*
  * Logout Session and navigate to login page
  */
  logout() {
    this.authService.logout();
    this.dialogDisplay = false;
    this.closeDialog.emit();
    this.router.navigate([`/login`], { relativeTo: this.route });
  }

  /*
 * Clear the idle lockout
 */
  idleClear() {
    this.dialogDisplay = false;
    this.closeDialog.emit();
  }

  get f() { return this.authentication.controls; }


  login() {
    this.submitted = true;
    if (this.authentication.invalid) {
      return;
    }
    const obj = {
      email: this.authentication.value.username,
      passwordHash: this.authentication.value.password
    };
    this.authService.login(obj).subscribe(res => {
      if (res.status === 'Success') {
        this.getCodeValue();
        //this.landing.loadTheLandingPage();
        this.closeDialog.emit();
      }
    });
  }

  getCodeValue() {
    this.getCodeService.getCodeByCategory('ALL').subscribe( res => {
      if (res.status === 'Success') {
        const value = JSON.parse(res.resultData);
        localStorage.setItem('codeValue', JSON.stringify(value.Codes));
      }
    });
  }

}
