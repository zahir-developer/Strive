import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../services/common-service/auth.service';
import { LandingService } from '../../services/common-service/landing.service';
import { GetCodeService } from '../../services/data-service/getcode.service';
import { UserDataService } from '../../util/user-data.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from '../../services/messageConfig';
import { ApplicationConfig } from '../../services/ApplicationConfig';

@Component({
  selector: 'app-session-logout',
  templateUrl: './session-logout.component.html'
})
export class SessionLogoutComponent implements OnInit {
  @Input() dialogDisplay: boolean;
  @Input() header: string;
  @Input() dialogType: string;
  @Input() countdown: number;
  @Output() closeDialog = new EventEmitter();
  @Output() continueSession = new EventEmitter();
  authentication: FormGroup;
  submitted = false;
  constructor(
    private user: UserDataService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private fb: FormBuilder,
    private getCodeService: GetCodeService,
    private modalService: NgbModal,
    private toastr: ToastrService
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
    this.modalService.dismissAll();
    this.authService.logout();
    this.dialogDisplay = false;
    this.closeDialog.emit();
    this.router.navigate([`/login`], { relativeTo: this.route });
  }

  /*
 * Clear the idle lockout
 */
  idleClear() {
    var token = localStorage.getItem('authorizationToken');
    if (token !== null) {
      this.authService.refreshToken().subscribe();
      this.continueSession.emit();
      this.closeDialog.emit();
    }
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
    this.authService.sessionLogin(obj).subscribe(res => {
      if (res.status === 'Success') {
        this.getCodeValue();
        localStorage.setItem('isAuthenticated', 'true');
        // this.authService.loggedIn.next(true);
        // this.landing.loadTheLandingPage();
        this.closeDialog.emit();
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  getCodeValue() {
    this.getCodeService.getCodeByCategory(ApplicationConfig.Category.all).subscribe(res => {
      if (res.status === 'Success') {
        const value = JSON.parse(res.resultData);
        localStorage.setItem('codeValue', JSON.stringify(value.Codes));
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
