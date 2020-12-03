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
  giftCardsView: boolean;
  SchedulesView: boolean;
  vechiclesView: boolean;
  employeesView: boolean;
  clientsView: boolean;
  cashRegisterSetupView: boolean;
  closeOutRegisterView: boolean;
  timeClockMaintenanceView: boolean;
  reportModule: boolean;
  whiteLabellingModule: boolean;
  payRollModule: boolean;
  checkOutModule: boolean;
  messengerModule: boolean;
  dailyStatusView: boolean;
  eodReportView: boolean;
  dailyTipReportView: boolean;
  monthlyTipView: boolean;
  monthlySalesView: boolean;
  monthlyCustomerSummaryView: boolean;
  monthlyMoneyOwnedView: boolean;
  monthlyCustomerDetailView: boolean;
  hourlyWashReportView: boolean;
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
              // Sales Module        
        if(this.ModuleName === "Sales"){
           this.salesModule = true;
        }
              //Admin Module
        if(this.ModuleName === "Admin"){
          this.adminModule = true;
          // Admin Module View Setup
          if(this.viewName === 'System Setup'){
            this.systemSetupView = true;
        }
        if(this.viewName === 'Gift Cards'){
         this.giftCardsView = true;
        }
        if(this.viewName === 'Schedules'){
         this.SchedulesView = true;
        }
        if(this.viewName === 'Vehicles'){
         this.vechiclesView = true;
        }
        if(this.viewName === 'Clients'){
         this.clientsView = true;
        }
        if(this.viewName === 'Employees'){
         this.employeesView = true;
        }
        if(this.viewName === 'Time Clock Maintenance'){
         this.timeClockMaintenanceView = true;
        }
        if(this.viewName === 'Close Out Register'){
         this.closeOutRegisterView = true;
        }
        if(this.viewName === 'Cash Register Setup'){
         this.cashRegisterSetupView = true;
        }
                  }
              //Detail Module
         if(this.ModuleName === "Detail"){
             this.detailModule = true;
                    }
            // Wash Module
          if(this.ModuleName === "Washes"){
              this.washModule = true;
             }
             //DashBoard Module
             if(this.ModuleName === "Dashboard"){
              this.dashBoardModule = true;
             }
              //Report Module
              if(this.ModuleName === "Report"){
                this.reportModule = true;

                 //Report Module view setup

                if(this.viewName === 'Daily Status Screen'){
                  this.dailyStatusView = true;
              }
              if(this.viewName === 'Eod Report'){
               this.eodReportView = true;
              }
              if(this.viewName === 'Daily Tip report'){
               this.dailyTipReportView = true;
              }
              if(this.viewName === 'Monthly Tip report'){
               this.monthlyTipView = true;
              }
              if(this.viewName === 'Monthly Sales Report'){
               this.monthlySalesView = true;
              }
              if(this.viewName === 'Monthly Customer Summary Report'){
               this.monthlyCustomerSummaryView = true;
              }
              if(this.viewName === 'Monthly Money Owed Report'){
               this.monthlyMoneyOwnedView = true;
              }
              if(this.viewName === 'Monthly Customer Detail Report'){
               this.monthlyCustomerDetailView = true;
              }
              if(this.viewName === 'Hourly Wash report'){
               this.hourlyWashReportView = true;
              }
              if(this.viewName === 'Daily Sales report'){
                this.dailyTipReportView = true;
               }
               }
              // White Labelling
              if(this.ModuleName === "White Labelling"){
                  this.whiteLabellingModule = true;
                 }
                // PayRoll
                if(this.ModuleName === "PayRoll"){
                  this.payRollModule = true;
                 }

                 //Checkout
                 if(this.ModuleName === "Checkout"){
                  this.checkOutModule = true;
                 }
                 //Messenger
                 if(this.ModuleName === "Messenger"){
                  this.messengerModule = true;
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
