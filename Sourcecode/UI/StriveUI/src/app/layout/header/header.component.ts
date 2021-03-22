import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/shared/util/user-data.service';
import { AuthService } from 'src/app/shared/services/common-service/auth.service';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { MessengerService } from 'src/app/shared/services/data-service/messenger.service';
import { WeatherService } from 'src/app/shared/services/common-service/weather.service';
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
  locationName: string;
  weatherDetails: any;
  rainPrediction: any;
  temperature: number;
  cityName: string;
  constructor(private authService: AuthService, private userService: UserDataService, private router: Router,
              private route: ActivatedRoute, private msgService: MessengerService,
              private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.empName = localStorage.getItem('employeeName');

    this.userService.headerName.subscribe(data => {
      this.empName = data;
    });

    this.userService.cityName.subscribe(data => {
      if(data == null){
        this.cityName = JSON.parse(localStorage.getItem('employeeCityName'));

      }
      else{
 this.cityName = data;
      }
     

    });
  
        this.userService.locationName.subscribe(data => {
          if(data == null){
            this.locationName = JSON.parse(localStorage.getItem('empLocationName'));

    
          }
          else{
            this.locationName = data;

          }
      
        
    });
  
  this.getWeatherDetails()
    this.getUnReadMessage();
  
  }
  // Get WeatherDetails
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
        this.weatherDetails = data;
        this.rainPrediction = data.currentWeather?.rainPercentage;
        this.temperature = Math.floor(data?.currentWeather?.temporature);

      }
    });
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
    });
  }

  navigateToMessage(message) {
    this.router.navigate(['/messenger']);
  }
}
