import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/shared/util/user-data.service';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
declare var $: any;
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAutheticated: boolean;
  empName = 'Admin';
  isLoggedIn$: Observable<boolean>;
  firstName: string;
  lastName: string;
  unReadMessageDetail: any = [];
  constructor(private authService: AuthService, private userService: UserDataService, private router: Router,
              private route: ActivatedRoute, private msgService: MessengerService) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.empName = localStorage.getItem('employeeName');
    this.userService.headerName.subscribe(data => {
      this.empName = data;
    });
    this.getUnReadMessage();
  }
  logout() {
    this.msgService.closeConnection();
    this.authService.logout();
  }
  openmbsidebar() {
    document.getElementById('mySidenav').style.width = '200px';
    $(document).ready(function() {
      $('.mobile-view-title').click(function() {
        $('#hide-mainmenu').hide();
        $('#show-submenu').show();
      });
      $('.back-to-list').click(function() {
        $('#hide-mainmenu').show();
        $('#show-submenu').hide();
      });
    });
  }

  getUnReadMessage() {
    this.userService.unReadMessageDetail.subscribe( res => {
      this.unReadMessageDetail = res;
      if (res === null) {
        this.unReadMessageDetail = [];
      }
      console.log(res, 'checkimg');
    });
  }

  navigateToMessage(message) {
    this.router.navigate(['/messenger']);
  }
}
