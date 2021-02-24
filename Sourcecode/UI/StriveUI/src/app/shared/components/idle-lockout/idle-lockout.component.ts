import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/common-service/auth.service';
import { UserDataService } from '../../util/user-data.service';

@Component({
  selector: 'app-idle-lockout',
  templateUrl: './idle-lockout.component.html',
  styleUrls: ['./idle-lockout.component.css']
})
export class IdleLockoutComponent implements OnInit {
  @Input() dialogDisplay = false;
  @Input() header: string;
  @Input() dialogType: string;
  @Input() countdown?: number;
  @Output() closeDialog = new EventEmitter();
  constructor(
    private user: UserDataService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
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
  }

}
