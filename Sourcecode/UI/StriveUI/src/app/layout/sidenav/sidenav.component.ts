import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
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
  isAuthenticated: boolean = false;

  logoBase64: any;
  isLoggedIn$: Observable<boolean>;
  viewName: string ;
  rollName: string ;
  RolePermission :any ;
  ModuleName: string;
  salesModule: boolean = false
  adminModule: boolean = false
  detailModule: boolean = false
  washModule: boolean = false
  dashBoardModule: boolean = false
  systemSetupView: boolean = false
  giftCardsView: boolean = false
  SchedulesView: boolean = false
  vechiclesView: boolean = false
  employeesView: boolean = false
  clientsView: boolean = false
  cashRegisterSetupView: boolean = false
  closeOutRegisterView: boolean = false
  timeClockMaintenanceView: boolean = false
  reportModule: boolean = false
  whiteLabellingModule: boolean = false
  payRollModule: boolean = false
  checkOutModule: boolean = false
  messengerModule: boolean = false
  dailyStatusView: boolean = false
  eodReportView: boolean = false
  dailyTipReportView: boolean = false
  monthlyTipView: boolean = false
  monthlySalesView: boolean = false
  monthlyCustomerSummaryView: boolean = false
  monthlyMoneyOwnedView: boolean = false
  monthlyCustomerDetailView: boolean = false
  hourlyWashReportView: boolean = false
  roles  = [];
  newRolePermission: string;
  @Input() localStorageUpdation: string  = 'localStorageUpdation';
localStorageupdate: boolean = false;

  constructor(private user: UserDataService, private authService: AuthService, private logoService: LogoService) { }

  ngOnInit(): void {
   this.roles  = [];

    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.getLogo();


this.user.navName.subscribe((data = []) => {
  this.roles = [];
  this.salesModule = false
  this.adminModule = false
  this.detailModule = false
  this.washModule =  false
  this.dashBoardModule = false
  this.systemSetupView = false
  this.giftCardsView = false
  this.SchedulesView = false
  this.vechiclesView = false
  this.employeesView = false
  this.clientsView = false
  this.cashRegisterSetupView = false
  this.closeOutRegisterView = false
  this.timeClockMaintenanceView = false
  this.reportModule = false
  this.whiteLabellingModule = false
  this.payRollModule = false
  this.checkOutModule = false
  this.messengerModule= false
  this.dailyStatusView = false
  this.eodReportView = false
  this.dailyTipReportView = false
  this.monthlyTipView = false
  this.monthlySalesView = false
  this.monthlyCustomerSummaryView = false
  this.monthlyMoneyOwnedView= false
  this.monthlyCustomerDetailView = false
  this.hourlyWashReportView = false
  this.localStorageUpdation = 'localStorageUpdation'
  if(data){
   const newparsedData =  JSON.parse(data);
    for (let i = 0; i < newparsedData.length ; i++){
    const viewName = newparsedData[i].ViewName;
      const rollName = newparsedData[i].RollName;
     const ModuleName = newparsedData[i].ModuleName;
     this.localStorageUpdation = 'localStorageUpdation1';
            // Sales Module        
      if(ModuleName === "Sales"){
         this.salesModule = true;
      }
            //Admin Module
      if(ModuleName === "Admin"){
        this.adminModule = true;
        // Admin Module View Setup
        if(viewName === 'System Setup'){
          this.systemSetupView = true;
      }
      if(viewName === 'Gift Cards'){
       this.giftCardsView = true;
      }
      if(viewName === 'Schedules'){
       this.SchedulesView = true;
      }
      if(viewName === 'Vehicles'){
       this.vechiclesView = true;
      }
      if(viewName === 'Clients'){
       this.clientsView = true;
      }
      if(viewName === 'Employees'){
       this.employeesView = true;
      }
      if(viewName === 'Time Clock Maintenance'){
       this.timeClockMaintenanceView = true;
      }
      if(viewName === 'Close Out Register'){
       this.closeOutRegisterView = true;
      }
      if(viewName === 'Cash Register Setup'){
       this.cashRegisterSetupView = true;
      }
                }
            //Detail Module
       if(ModuleName === "Detail"){
           this.detailModule = true;
                  }
          // Wash Module
        if(ModuleName === "Washes"){
            this.washModule = true;
           }
           //DashBoard Module
           if(ModuleName === "Dashboard"){
            this.dashBoardModule = true;
           }
            //Report Module
            if(ModuleName === "Report"){
              this.reportModule = true;

               //Report Module view setup

              if(viewName === 'Daily Status Screen'){
                this.dailyStatusView = true;
            }
            if(viewName === 'EOD Report'){
             this.eodReportView = true;
            }
            if(viewName === 'Daily Tip report'){
             this.dailyTipReportView = true;
            }
            if(viewName === 'Monthly Tip report'){
             this.monthlyTipView = true;
            }
            if(viewName === 'Monthly Sales Report'){
             this.monthlySalesView = true;
            }
            if(viewName === 'Monthly Customer Summary Report'){
             this.monthlyCustomerSummaryView = true;
            }
            if(viewName === 'Monthly Money Owed Report'){
             this.monthlyMoneyOwnedView = true;
            }
            if(viewName === 'Monthly Customer Detail Report'){
             this.monthlyCustomerDetailView = true;
            }
            if(viewName === 'Hourly Wash report'){
             this.hourlyWashReportView = true;
            }
            if(viewName === 'Daily Sales report'){
              this.dailyTipReportView = true;
             }
             }
            // White Labelling
            if(ModuleName === "White Labelling"){
                this.whiteLabellingModule = true;
               }
              // PayRoll
              if(ModuleName === "PayRoll"){
                this.payRollModule = true;
               }

               //Checkout
               if(ModuleName === "Checkout"){
                this.checkOutModule = true;
               }
               //Messenger
               if(ModuleName === "Messenger"){
                this.messengerModule = true;
               }
  }   
  }
  else{
   const localData = localStorage.getItem('navName')
    const paersedLocalData =  JSON.parse(localData);
    for (let i = 0; i < paersedLocalData.length ; i++){
      const viewNameLocal = paersedLocalData[i].ViewName;
      this.rollName = paersedLocalData[i].RollName;
     const ModuleNameLocal = paersedLocalData[i].ModuleName;
     this.localStorageUpdation = 'localStorageUpdation1'
 
            // Sales Module        
      if(ModuleNameLocal === "Sales"){
         this.salesModule = true;
      }
            //Admin Module
      if(ModuleNameLocal === "Admin"){
        this.adminModule = true;
        // Admin Module View Setup
        if(viewNameLocal === 'System Setup'){
          this.systemSetupView = true;
      }
      if(viewNameLocal === 'Gift Cards'){
       this.giftCardsView = true;
      }
      if(viewNameLocal === 'Schedules'){
       this.SchedulesView = true;
      }
      if(viewNameLocal === 'Vehicles'){
       this.vechiclesView = true;
      }
      if(viewNameLocal === 'Clients'){
       this.clientsView = true;
      }
      if(viewNameLocal === 'Employees'){
       this.employeesView = true;
      }
      if(viewNameLocal === 'Time Clock Maintenance'){
       this.timeClockMaintenanceView = true;
      }
      if(viewNameLocal === 'Close Out Register'){
       this.closeOutRegisterView = true;
      }
      if(viewNameLocal === 'Cash Register Setup'){
       this.cashRegisterSetupView = true;
      }
                }
            //Detail Module
       if(ModuleNameLocal === "Detail"){
           this.detailModule = true;
                  }
          // Wash Module
        if(ModuleNameLocal === "Washes"){
            this.washModule = true;
           }
           //DashBoard Module
           if(ModuleNameLocal === "Dashboard"){
            this.dashBoardModule = true;
           }
            //Report Module
            if(ModuleNameLocal === "Report"){
              this.reportModule = true;

               //Report Module view setup

              if(viewNameLocal === 'Daily Status Screen'){
                this.dailyStatusView = true;
            }
            if(viewNameLocal === 'EOD Report'){
             this.eodReportView = true;
            }
            if(viewNameLocal === 'Daily Tip report'){
             this.dailyTipReportView = true;
            }
            if(viewNameLocal === 'Monthly Tip report'){
             this.monthlyTipView = true;
            }
            if(viewNameLocal === 'Monthly Sales Report'){
             this.monthlySalesView = true;
            }
            if(viewNameLocal === 'Monthly Customer Summary Report'){
             this.monthlyCustomerSummaryView = true;
            }
            if(viewNameLocal === 'Monthly Money Owed Report'){
             this.monthlyMoneyOwnedView = true;
            }
            if(viewNameLocal === 'Monthly Customer Detail Report'){
             this.monthlyCustomerDetailView = true;
            }
            if(viewNameLocal === 'Hourly Wash report'){
             this.hourlyWashReportView = true;
            }
            if(viewNameLocal === 'Daily Sales report'){
              this.dailyTipReportView = true;
             }
             }
            // White Labelling
            if(ModuleNameLocal === "White Labelling"){
                this.whiteLabellingModule = true;
               }
              // PayRoll
              if(ModuleNameLocal === "PayRoll"){
                this.payRollModule = true;
               }

               //Checkout
               if(ModuleNameLocal === "Checkout"){
                this.checkOutModule = true;
               }
               //Messenger
               if(ModuleNameLocal === "Messenger"){
                this.messengerModule = true;
               }
  }

  }
 

})

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
