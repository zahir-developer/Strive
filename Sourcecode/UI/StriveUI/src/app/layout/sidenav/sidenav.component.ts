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
  viewName: string;
  rollName: string;
  RolePermission: any;
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
  customerHistoryView: boolean = false
  customerView: boolean = false

  roles = [];
  newRolePermission: string;
  @Input() localStorageUpdation: string = 'localStorageUpdation';
  localStorageupdate: boolean = false;
  logoName: any;
  customerModule: boolean;

  constructor(private user: UserDataService, private authService: AuthService, private logoService: LogoService) { }

  ngOnInit(): void {
    this.roles = [];

    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.getLogo();

    this.user.navName.subscribe((data = []) => {
      this.roles = [];
      this.localStorageUpdation = 'localStorageUpdation'
      
    this.salesModule = false;
    this.adminModule = false;
    this.detailModule = false;
    this.washModule = false;
    this.dashBoardModule = false;
    this.customerModule = false;
    this.reportModule = false;
    this.whiteLabellingModule = false;
    this.payRollModule = false;
    this.checkOutModule = false;
    this.messengerModule = false;

      if (data) {
        const newparsedData = JSON.parse(data);
        for (let i = 0; i < newparsedData?.length; i++) {
          const viewName = newparsedData[i].ViewName;
          const rollName = newparsedData[i].RollName;
          const ModuleName = newparsedData[i].ModuleName;
          this.localStorageUpdation = 'localStorageUpdation1';
          this.routingNav(ModuleName, viewName);
        }
      }
      else {
        const localData = localStorage.getItem('navName');
        const paersedLocalData = JSON.parse(localData);
        for (let i = 0; i < paersedLocalData?.length; i++) {
          const viewNameLocal = paersedLocalData[i].ViewName;
          this.rollName = paersedLocalData[i].RollName;
          const ModuleNameLocal = paersedLocalData[i].ModuleName;
          this.localStorageUpdation = 'localStorageUpdation1'
          this.routingNav(ModuleNameLocal, viewNameLocal);
        }
      }
    });
  }

  routingNav(ModuleNameLocal, viewNameLocal) {
        // Sales Module        
    if (ModuleNameLocal === "Sales") {
      this.salesModule = true;
    }
    //Admin Module
    if (ModuleNameLocal === "Admin") {
      this.adminModule = true;
      // Admin Module View Setup
      if (viewNameLocal === 'SystemSetup') {
        this.systemSetupView = true;
      }
      else if (viewNameLocal === 'GiftCards') {
        this.giftCardsView = true;
      }
      else if (viewNameLocal === 'Schedules') {
        this.SchedulesView = true;
      }
      else if (viewNameLocal === 'Vehicles') {
        this.vechiclesView = true;
      }
      else if (viewNameLocal === 'Clients') {
        this.clientsView = true;
      }
      else if (viewNameLocal === 'Employees') {
        this.employeesView = true;
      }
      else if (viewNameLocal === 'TimeClockMaintenance') {
        this.timeClockMaintenanceView = true;
      }
      else if (viewNameLocal === 'CloseOutRegister') {
        this.closeOutRegisterView = true;
      }
      else if (viewNameLocal === 'CashRegisterSetup') {
        this.cashRegisterSetupView = true;
      }
    }
    //Detail Module
    if (ModuleNameLocal === "Detail") {
      this.detailModule = true;
    }
    // Wash Module
    else if (ModuleNameLocal === "Washes") {
      this.washModule = true;
    }
    //DashBoard Module
    else if (ModuleNameLocal === "Dashboard") {
      this.dashBoardModule = true;
    }
    // Customer
    else if (ModuleNameLocal === "Customer") {
      this.customerModule = true;
    }
    //Report Module
    if (ModuleNameLocal === "Report") {
      this.reportModule = true;
      //Report Module view setup
      if (viewNameLocal === 'DailyStatusScreen') {
        this.dailyStatusView = true;
      }
      else if (viewNameLocal === 'EODReport') {
        this.eodReportView = true;
      }
      else if (viewNameLocal === 'DailyTipreport') {
        this.dailyTipReportView = true;
      }
      else if (viewNameLocal === 'MonthlyTipreport') {
        this.monthlyTipView = true;
      }
      else if (viewNameLocal === 'MonthlySalesReport') {
        this.monthlySalesView = true;
      }
      else if (viewNameLocal === 'MonthlyCustomerSummaryReport') {
        this.monthlyCustomerSummaryView = true;
      }
      else if (viewNameLocal === 'MonthlyMoneyOwedReport') {
        this.monthlyMoneyOwnedView = true;
      }
      else if (viewNameLocal === 'MonthlyCustomerDetailReport') {
        this.monthlyCustomerDetailView = true;
      }
      else if (viewNameLocal === 'HourlyWashreport') {
        this.hourlyWashReportView = true;
      }
      else if (viewNameLocal === 'DailySalesreport') {
        this.dailyTipReportView = true;
      }
    }
    // White Labelling
    if (ModuleNameLocal === "WhiteLabelling") {
      this.whiteLabellingModule = true;
    }
    // PayRoll
    if (ModuleNameLocal === "PayRoll") {
      this.payRollModule = true;
    }

    //Checkout
    if (ModuleNameLocal === "Checkout") {
      this.checkOutModule = true;
    }
    //Messenger
    if (ModuleNameLocal === "Messenger") {
      this.messengerModule = true;
    }
    //Customer
    if (ModuleNameLocal === "Customer") {
      this.customerView = true;
    }
    //CustomerHistory
    if (ModuleNameLocal === "CustomerHistory") {
      this.customerHistoryView = true;
    }
  }

  getLogo() {
    this.logoBase64 = '';
    this.logoService.name.subscribe(data => {
      const base64 = 'data:image/png;base64,';
      this.logoBase64 = base64 + data;
    });
    this.logoService.title.subscribe(title => {
      this.logoName = title;
    });
  }
  openNav(menu) {
    if (menu === 'reports') {
      document.getElementById('reportSliderMenu').style.width = '180px';
      document.getElementById('navSliderMenu').style.width = '0';
      document.getElementById('content-wrapper').style.marginLeft = '0';
      document.getElementById('content-wrapper').style.marginLeft = '180px';
    } else {
      document.getElementById('navSliderMenu').style.width = '180px';
      document.getElementById('content-wrapper').style.marginLeft = '180px';
    }
    $('.menu li').on('click', function () {
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
