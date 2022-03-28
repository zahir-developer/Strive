import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation, AfterViewInit } from '@angular/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin, { Draggable } from '@fullcalendar/interaction';
import timelinePlugin from '@fullcalendar/timeline';
import * as moment from 'moment';
import { FullCalendar } from 'primeng';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ScheduleService } from 'src/app/shared/services/data-service/schedule.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DashboardStaticsComponent } from 'src/app/shared/components/dashboard-statics/dashboard-statics.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { DatePipe } from '@angular/common';

declare var $: any;
@Component({
  selector: 'app-scheduling',
  templateUrl: './scheduling.component.html',
  styleUrls: ['./scheduling.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SchedulingComponent implements OnInit, AfterViewInit {
  public theme = 'theme-light';
  calendar: any;
  submitted = false;
  locationFlag = false;
  isLeave: boolean;
  selectedList = [];
  buttonText = 'Add';
  empName: any;
  empId: any;
  fromDate: any;
  endDate: any;
  today = moment(new Date());
  clickCnt = 0;
  events = [];
  options: any;
  searchEmp = '';
  search = ' ';
  headerData = '';
  mytime: any;
  location = [];
  empLocation: any;
  selectedEvent = [];
  startTime: Date;
  endTime: Date;
  dateTime: Date
  @ViewChild('fc') fc: FullCalendar;
  @ViewChild('draggable_people') draggablePeopleExternalElement: ElementRef;
  empList: any;
  showDialog: boolean;
  locationId: any;
  scheduleId: any;
  scheduleType: any;
  totalHours: any;
  EmpCount: any;
  jobTypeId: any;
  clonedEmpList = [];
  @ViewChild(DashboardStaticsComponent) dashboardStaticsComponent: DashboardStaticsComponent;
  noOfForcastedCars = 0;
  noOfForcastedHours = 0;
  noOfCurrentEmployee = 0;
  noOfCurrentHours = 0;
  forecastDialog: boolean;
  forecastedList: any;
  isSelectAll: false;

  constructor(
    private empService: EmployeeService,
    private datePipe: DatePipe, 
    private locationService: LocationService,
    private messageService: MessageServiceToastr,
    private scheduleService: ScheduleService,
    private employeeService: EmployeeService,
    private spinner: NgxSpinnerService,
    private detailService: DetailService,
    private toastr: ToastrService,
    public dashboardService: DashboardService
  ) {
    this.dateTime = new Date();
  }
  ngAfterViewInit() {
    // tslint:disable-next-line:no-unused-expression
    new Draggable(this.draggablePeopleExternalElement?.nativeElement, {
      itemSelector: '.fc-event',
      eventData: function (eventEl) {
        return {
          id: 'dragged1',
          title: eventEl.innerText,
          classNames: ['draggedEvent'],
        };
      }
    });

  }
  ngOnInit(): void {
    this.forecastDialog = false;
    this.locationId = +localStorage.getItem('empLocationId');
    this.searchEmployee();
    this.getLocationList();

    this.options = {
      plugins: [dayGridPlugin, timelinePlugin, timeGridPlugin, interactionPlugin],
      defaultDate: new Date(),
      header: {
        left: '',
        center: 'prev, title, next',
        right: 'timeGridDay,timeGridWeek'
      },
      editable: true,
      allDaySlot: false,
      droppable: true,
      slotDuration: '00:30:00',
      minTime: '07:00:00',
      maxTime: '20:00:00',
      defaultView: 'timeGridWeek',
      slotEventOverlap: false,
      height: 'auto',
      contentHeight: 'auto',
      eventRender(element) {
      },
      eventClick: (event) => {
        this.getScheduleById(event.event);
        /*if (!event.event.id.startsWith('click')) {
          this.getScheduleById(+event.event.id);
        } else {
          this.splitEmpName(event);
          this.bindPopUp(event);
          this.selectedList.forEach(item => {
            if (item.EmployeeId === +this.empId) {
              item.clicked = true;
            }
          });
        }*/
      },
      dayClick: (event) => {
      },
      eventResize: (event) => {
        this.updateSchedule(event);
      },
      eventReceive: (eventReceiveEvent) => {
        var empScheduleList = [];
        const selectedDate = eventReceiveEvent.event.start;
        const currentDate: any = new Date();
        if (Date.parse(selectedDate) < Date.parse(currentDate)) {
          this.messageService.showMessage({ severity: 'info', title: 'Info', body: MessageConfig.Schedule.schedulePassDate });
          this.cancel();
        } else {
          this.buttonText = 'Add';
          this.empLocation = this.locationId.toString();
          this.events = this.events.filter(item => item.classNames[0] !== 'draggedEvent');
          const multiSelect = this.selectedList.filter(item => item.clicked === false);
          this.selectedList = this.empList.filter(item => item.selected === true);
          this.splitEmpName(eventReceiveEvent);
          this.startTime = eventReceiveEvent.event.start;
          this.isLeave = false;
          this.endTime = moment(eventReceiveEvent.event.start).add(60, 'minutes').toDate();
          if (this.selectedList.length === 0) {
            this.selectedList = this.empList.filter(item => item.EmployeeId === +this.empId);
          }
          if (multiSelect.length !== 0) {
            multiSelect.forEach(element => {
              //this.selectedList.push(element);
            });
          }
          if (this.selectedList.length === 1 && multiSelect.length === 0) {
            /*
            $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
            $('#name').html(this.empName);
            $('#empId').html(this.empId);
            $('.modal').find('#location').val(0);
            */
            empScheduleList.push(
              {
                employeeId: this.empId,
                startTime: eventReceiveEvent.event.start,
                endTime: moment(eventReceiveEvent.event.start).add(60, 'minutes'),
                scheduleDate: selectedDate,
                locationId: this.locationId
              }
            );

          } else {
            this.removeDraggedEvent();
            let i = 0;
            this.selectedList.forEach(item => {
              i++;
              item.id = 'clicked' + i,
                item.title = item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
                item.start = this.startTime,
                item.end = moment(eventReceiveEvent.event.start).add(60, 'minutes'),
                item.clicked = false,
                // this.events = [... this.events, {
                //   id: 'clicked' + i,
                //   title: item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
                //   start: this.startTime,
                //   end: moment(eventReceiveEvent.event.start).add(60, 'minutes'),
                //   classNames: ['draggedEvent'],
                // }];

                empScheduleList.push(
                  {
                    employeeId: item.EmployeeId,
                    startTime: eventReceiveEvent.event.start,
                    endTime: moment(eventReceiveEvent.event.start).add(60, 'minutes'),
                    scheduleDate: selectedDate,
                    locationId: this.locationId
                  }
                )
            });
          }
          
          this.saveSchedule(empScheduleList);
        }
      },
      eventDragStop: (event) => {
        if ((150 <= event.jsEvent.pageX) && (event.jsEvent.pageX <= 500)) {
          this.deleteEvent(event);
        }
      },
      datesRender: (event) => {
        this.fromDate = moment(event.view.activeStart).format('YYYY-MM-DDTHH:mm:ss');
        this.endDate = moment(event.view.activeEnd).format('YYYY-MM-DDTHH:mm:ss');
        this.getSchedule();
      },
      eventDrop: (event) => {
        this.updateSchedule(event);
      },
    };
  }

  updateSchedule(event)
  {

    var empScheduleList = [];
    empScheduleList.push(
      {
        employeeId: event.event.extendedProps.employeeId,
        startTime: event.event.start,
        endTime: event.event.end === null ? moment(event.event.start).add(60, 'minutes').toDate() :
        event.event.end,
        scheduleId: event?.event?.extendedProps?.scheduleId,
        scheduleDate: event.event.start,
        locationId: this.locationId
      }
    );

    this.saveSchedule(empScheduleList);
  }

  // Get all the Employees details
  getEmployeeList() {
    this.empService.getAllEmployeeName(this.locationId).subscribe(data => {
      if (data.status === 'Success') {
        this.empList = JSON.parse(data.resultData);
        this.setBoolean();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Set Default boolean for customization
  setBoolean() {

    if (this.empList.EmployeeList !== undefined) {
      this.empList.EmployeeList.forEach(item => {
        item.selected = false;
        item.clicked = false;
      });
    }
  }
  // Get All Location
  getLocationList() {
    const locID = 0;
    this.dashboardService.getAllLocationWashTime(locID,this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss')).subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Washes;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  // Save Schedule
  addSchedule() {
    const schedule = [];
    this.submitted = true;
    if (this.empLocation === '' || this.empLocation === undefined) {
      this.locationFlag = true;
      return;
    }
    if (this.dateTime > this.startTime) {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.Schedule.passedDateTime });
      return;
    }
    let alreadyScheduled = false;
    this.events.forEach(item => {
      if (moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss') === item.start) {
        if (item.extendedProps.employeeId === +this.empId &&
          item.extendedProps.locationId === +this.empLocation) {
          if (!this.scheduleId) {
            alreadyScheduled = true;
          }
        }
      }
    });
    if (alreadyScheduled) {
      this.messageService.showMessage({ severity: 'info', title: 'Info', body: MessageConfig.Schedule.sameTime });
      return;
    }
    const form = {
      scheduleId: this.scheduleId ? this.scheduleId : 0,
      employeeId: +this.empId,
      locationId: +this.empLocation,
      roleId: +localStorage.getItem('roleId'),
      isAbscent: this.isLeave,
      scheduledDate: moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      startTime: moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      endTime: moment(this.endTime).format('YYYY-MM-DDTHH:mm:ss'),
      scheduleType: this.scheduleType ? this.scheduleType : 1,
      comments: null,
      isActive: true,
      isDeleted: false
    };
    schedule.push(form);
    const scheduleObj = {
      schedule
    };
    this.spinner.show();
    this.scheduleService.saveSchedule(scheduleObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        this.scheduleId = false;
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Schedule.save });
        $('#calendarModal').modal('hide');
        this.getSchedule();
      } else {
        this.spinner.hide();

        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
        this.getSchedule();
        $('#calendarModal').modal('hide');
      }
    }, (err) => {
      this.spinner.hide();

      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      this.getSchedule();
      $('#calendarModal').modal('hide');
    });
  }

  saveSchedule(emplist)
  {
  var schedules = [];
  emplist.forEach(item => {

    var schedule =
    {
    scheduleId: item.scheduleId ? item.scheduleId : 0,
      employeeId: item.employeeId,
      locationId: item.locationId,
      roleId: null,
      isAbscent: this.isLeave,
      scheduledDate: moment(item.scheduleDate).format('YYYY-MM-DDTHH:mm:ss'),
      startTime: moment(item.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      endTime: moment(item.endTime).format('YYYY-MM-DDTHH:mm:ss'),
      scheduleType: this.scheduleType ? this.scheduleType : 1,
      comments: null,
      isActive: true,
      isDeleted: false
    }
    
    schedules.push(schedule);
    
  });

  const scheduleObj = {
    schedule: schedules
  };

  //this.spinner.show();
  this.scheduleService.saveSchedule(scheduleObj).subscribe(data => {
    if (data.status === 'Success') {
      //this.spinner.hide();
      this.scheduleId = false;
      this.messageService.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Schedule.save });
      $('#calendarModal').modal('hide');
      this.getSchedule();
    } else {
      this.spinner.hide();

      this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      this.getSchedule();
      $('#calendarModal').modal('hide');
    }
  }, (err) => {
    this.spinner.hide();

    this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
    this.getSchedule();
    $('#calendarModal').modal('hide');
  });
}
  // Get LocationById
  getLocation(event) {
    this.empLocation = event.target.value;
  }
  // Get all schedule based on date
  getSchedule() {
    this.events = [];
    const getScheduleObj = {
      startDate: this.fromDate,
      endDate: this.endDate,
      locationId: this.locationId ? this.locationId : 0
    };
    //this.spinner.show();
    this.scheduleService.getSchedule(getScheduleObj).subscribe(data => {
      if (data.status === 'Success') {
        this.setDefaultBoolean(false);
        //this.spinner.hide();
        const empSchehdule = JSON.parse(data.resultData);
        this.getScheduleAndForcasted(getScheduleObj);
        if (empSchehdule.ScheduleDetail !== null) {
          this.totalHours = empSchehdule?.ScheduleDetail?.ScheduleHoursViewModel?.Totalhours ?
            empSchehdule?.ScheduleDetail?.ScheduleHoursViewModel?.Totalhours : 0;
          this.EmpCount = empSchehdule?.ScheduleDetail?.ScheduleEmployeeViewModel?.TotalEmployees ?
            empSchehdule?.ScheduleDetail?.ScheduleEmployeeViewModel?.TotalEmployees : 0;
          if (empSchehdule?.ScheduleDetail?.ScheduleDetailViewModel !== null) {
            empSchehdule?.ScheduleDetail?.ScheduleDetailViewModel.forEach(item => {
              const startTime = item.StartTime.split('+');
              const endTime = item.EndTime.split('+');
              const emp = {
                id: +item.ScheduleId,
                start: moment(startTime[0]).format('YYYY-MM-DDTHH:mm:ss'),
                end: moment(endTime[0]).format('YYYY-MM-DDTHH:mm:ss'),
                title:  '#'+item.EmployeeId +' - '+ item.EmployeeName + '\xa0 \xa0' + ' - '+ item.LocationName,
                textColor: 'white',
                backgroundColor: item.IsEmployeeAbscent === true ? '#A9A9A9' : item.ColorCode,
                classNames: ['event'],
                extendedProps: {
                  employeeId: +item.EmployeeId,
                  roleId: +item.RoleId,
                  scheduleType: +item.ScheduleType,
                  locationId: +item.LocationId,
                  scheduleId: +item.ScheduleId,
                  employeeName: +item.EmployeeName
                }
              };
              this.events = [... this.events, emp];
            });
          }
        }
        this.removeDraggedEvent();
        //this.retainUnclickedEvent();
      }
      else {
        //this.spinner.hide();

      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      });
  }

  getScheduleAndForcasted(getScheduleObj) {
    this.scheduleService.getScheduleAndForcasted(getScheduleObj).subscribe(res => {
      if (res.status === 'Success') {
        const scheduleDetail = JSON.parse(res.resultData);
        this.forecastedList = scheduleDetail.ScheduleForcastedDetail.ForcastedCarEmployeehoursViewModel;
        //this.forecastedList.forEach(item => {
        // item.TotalEmployees = 0;
        //  item.Totalhours = 0;
        //});
        // this.noOfForcastedCars = scheduleDetail.ScheduleForcastedDetail.ForcastedCarEmployeehoursViewModel.ForcastedCars ?
        //   scheduleDetail.ScheduleForcastedDetail.ForcastedCarEmployeehoursViewModel.ForcastedCars : 0;
        // this.noOfForcastedHours = scheduleDetail.ScheduleForcastedDetail.ForcastedCarEmployeehoursViewModel.ForcastedEmployeeHours ?
        //   scheduleDetail.ScheduleForcastedDetail.ForcastedCarEmployeehoursViewModel.ForcastedEmployeeHours : 0;
        // this.noOfCurrentEmployee = scheduleDetail.ScheduleForcastedDetail.ScheduleEmployeeViewModel.TotalEmployees;
        // this.noOfCurrentHours = scheduleDetail.ScheduleForcastedDetail.ScheduleHoursViewModel.Totalhours;
        // $('.fc-day-header').append(`<i class="mdi mdi-file-document theme-secondary-color">`);
        // $('.fc-day-header').prop('title', `No of Forecasted Cars: ${this.noOfForcastedCars}\nNo of Forecasted Hours: ${this.noOfForcastedHours}\nNo of Current Employees: ${this.noOfCurrentEmployee}\nNo of Current Hours: ${this.noOfCurrentHours}`);
      }
    }, (err) => {
      // $('.fc-day-header').append(`<i class="mdi mdi-file-document theme-secondary-color">`);
      // $('.fc-day-header').prop('title', `No of Forecasted Cars: ${this.noOfForcastedCars}\nNo of Forecasted Hours: ${this.noOfForcastedHours}\nNo of Current Employees: ${this.noOfCurrentEmployee}\nNo of Current Hours: ${this.noOfCurrentHours}`);
    });
  }

  forecastedDetail() {
    this.forecastDialog = true;
  }
  // Retain Unclicked EmployeeList
  retainUnclickedEvent() {
    if (this.selectedList.length !== 0) {
      this.selectedList.forEach(item => {
        if (item.clicked === false) {
          this.events = [... this.events, {
            id: item.id,
            start: item.start,
            end: item.end,
            title: item.title,
            classNames: ['draggedEvent'],
          }];
        }
      });
    }
  }
  // Remove Dragged Event
  removeDraggedEvent() {
    const draggedEvent = this.fc.getCalendar().getEventById('dragged1');
    if (draggedEvent !== null) {
      draggedEvent.remove();
    }
  }
  // For Dynamic ID Creation
  guid() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
  }

  // Delete Event
  deleteEvent(event) {

    this.scheduleService.deleteSchedule(event.event.id).subscribe(data => {
      if (data.status === 'Success') {
        this.getSchedule();
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  getJobType() {
    this.detailService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Wash') {
              this.jobTypeId = item.valueid;
              this.dashboardStaticsComponent.jobTypeId = this.jobTypeId;
              this.dashboardStaticsComponent.getDashboardDetails();
            }
          });
        }
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }

  getLocationId(event) {
    this.locationId = event.LocationId;
    this.getSchedule();
    this.searchEmployee();
  }
  // Get the schedule by Id
  getScheduleById(event) {
    this.scheduleService.getScheduleById(+event.id).subscribe(data => {
      if (data.status === 'Success') {
        const selectedScheduledData = JSON.parse(data.resultData);
        if (selectedScheduledData.Status.length !== 0) {

          const startTime = selectedScheduledData.Status[0].StartTime.split('+');
          const endTime = selectedScheduledData.Status[0].EndTime.split('+');
          
          this.empName = selectedScheduledData.Status[0].EmployeeName;
          this.empId = selectedScheduledData.Status[0].EmployeeId;
          $('#name').html(this.empName);
          $('#empId').html(this.empId);

          this.startTime = startTime[0];
          this.endTime = endTime[0];
          this.scheduleId = selectedScheduledData.Status[0].ScheduleId;
          this.scheduleType = selectedScheduledData.Status[0].ScheduleType;
          this.isLeave = selectedScheduledData.Status[0].IsEmployeeAbscent;
          this.empLocation = selectedScheduledData.Status[0].LocationId;
          $('.modal').find('#location').val(selectedScheduledData.Status[0].LocationId);
          $('.modal').find('#isleave').val(selectedScheduledData.Status[0].IsEmployeeAbscent);
          this.buttonText = 'Save';
          $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
        }
      }

    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
  }
  splitEmpName(event) {
    const str = event.event.title.split('\n');
    this.empName = str[0];
    this.empId = str[1];
  }
  // Bind the Modal with value
  bindPopUp(event) {
    this.startTime = event.event.start;
    this.endTime = event.event.end === null ? moment(event.event.start).add(60, 'minutes').toDate() :
      event.event.end;
    this.scheduleId = event?.event?.extendedProps?.scheduleId;
    this.scheduleType = event?.event?.extendedProps?.scheduleType;
    $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
    $('#name').html(this.empName);
    $('#empId').html(this.empId);
    $('.modal').find('#isleave').val(false);
    this.isLeave = false;
    if (event.event.extendedProps.locationId) {
      $('.modal').find('#location').val(event.event.extendedProps.locationId);
      this.empLocation = event.event.extendedProps.locationId;
    } else {
      this.empLocation = undefined;
    }
  }
  cancel() {
    const draggedEvent = this.fc.getCalendar().getEventById('dragged1');
    if (draggedEvent !== null) {
      draggedEvent.remove();
    } else {
      this.getSchedule();
    }
    this.fc.getCalendar().render();
    $('#calendarModal').modal('hide');
    this.isLeave = false;
    this.empLocation = undefined;
    this.submitted = false;
    this.locationFlag = false;
  }
  // Search Employee
  searchEmployee() {
    this.spinner.show();
    const empObj = {
      startDate: null,
      endDate: null,
      locationId: null,
      pageNo: null,
      pageSize: null,
      query: this.search,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.getJobType();
    this.employeeService.getAllEmployeeName(this.locationId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        // this.empList = JSON.parse(res.resultData);
        const seachList = JSON.parse(res.resultData);
        this.empList = seachList.EmployeeName;
        this.empList.forEach(item => {
          item.employeeName = item.FirstName + ' ' + item.LastName;
        });
        this.clonedEmpList = this.empList.map(x => Object.assign({}, x));
        // if (seachList.EmployeeList.Employee !== null) {
        //   this.empList = seachList.EmployeeList.Employee;
        // }
      } else {
        this.spinner.hide();
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  getAll() {
    this.locationId = 0;
    this.getSchedule();
    this.searchEmployee();
  }
  isAbsentChange(event) {
  }
  searchFocus() {
    this.search = this.search.trim();
  }

  searchEmployeeList(text) {
    if (text.length > 0) {
      this.empList = this.clonedEmpList.filter(item => item.employeeName.toLowerCase().includes(text));
    } else {
      this.empList = [];
      this.empList = this.clonedEmpList;
    }
  }

  setDefaultBoolean(flag) {
    this.empList?.forEach(item => {
      item.selected = flag;
    });
    this.isSelectAll = flag;
  }

  selectAllEmployees(event) {
    if (event.target.checked === true) {
      this.setDefaultBoolean(true);
    } else {
      this.setDefaultBoolean(false);
    }
  }
}
