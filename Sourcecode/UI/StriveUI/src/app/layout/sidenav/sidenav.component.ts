import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserDataService } from 'src/app/shared/util/user-data.service';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { Observable } from 'rxjs';
import { LogoService } from 'src/app/shared/services/common-service/logo.service';
declare var $: any;
@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SidenavComponent implements OnInit {
  isAuthenticated: boolean;
  logoBase64: any;
  isLoggedIn$: Observable<boolean>;
  viewName: string;
  rollName: string;
  RolePermission :any = [];
  ModuleName: any;
  salesModule: boolean;
  adminModule: boolean;
  detailModule: boolean;
  washModule: boolean;
  dashBoardModule: boolean;
  systemSetupView: boolean;
  constructor(private user: UserDataService, private authService: AuthService, private logoService: LogoService) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.getLogo();
    this.RolePermission = JSON.parse(localStorage.getItem('RolePermission'))
  this.getRoles();

  }
  getRoles(){
    for (let i = 0; i < this.RolePermission.length; i++){
        this.viewName = this.RolePermission[i].ViewName;
        this.rollName = this.RolePermission[i].RollName;
        this.ModuleName = this.RolePermission[i].ModuleName;
        console.log(this.ModuleName)
        if(this.ModuleName === "Sales"){
           this.salesModule = true;
        }
        if(this.ModuleName === "Admin"){
          this.adminModule = true;
                  }
         if(this.ModuleName === "Detail"){
             this.detailModule = true;
                            }
          if(this.ModuleName === "Washes"){
              this.washModule = true;
             }
             if(this.ModuleName === "Dashboard"){
              this.dashBoardModule = true;
             }
             if(this.viewName === 'System Setup'){
this.systemSetupView = true;
             }
    }
    
  }
  getLogo() {
    this.logoService.name.subscribe(data => {
      const base64 = 'data:image/png;base64,';
      this.logoBase64 = base64 + data;
    });
  }
  openNav(menu) {
    if (menu === 'reports'){
      document.getElementById('reportSliderMenu').style.width = '180px';
      document.getElementById('navSliderMenu').style.width = '0';
      document.getElementById('content-wrapper').style.marginLeft = '0';
      document.getElementById('content-wrapper').style.marginLeft = '180px';
    } else {
      document.getElementById('navSliderMenu').style.width = '180px';
      document.getElementById('content-wrapper').style.marginLeft = '180px';
    }
    $('.menu li').on('click', function() {
      $('.menu li').removeClass('theme-secondary-background-color active');
      $(this).addClass('theme-secondary-background-color active');
    });
    $('.nav-slider-menu-items li a').on('click', function () {
      $('.nav-slider-menu-items li a').removeClass('theme-secondary-color text-underline');
      $(this).addClass('theme-secondary-color text-underline');
    });
  }

  closeNav() {
    document.getElementById('reportSliderMenu').style.width = '0';
    document.getElementById('navSliderMenu').style.width = '0';
    document.getElementById('content-wrapper').style.marginLeft = '0';
  }

  closembsidebar() {
    document.getElementById('mySidenav').style.width = '0';
  }
}
