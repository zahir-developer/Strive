import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation, AfterViewInit } from '@angular/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin, { Draggable } from '@fullcalendar/interaction';
import timelinePlugin from '@fullcalendar/timeline';
import * as moment from 'moment';
import { FullCalendar } from 'primeng';
// import { FullCalendarComponent } from '@fullcalendar/angular';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ScheduleService } from 'src/app/shared/services/data-service/schedule.service';
import { element } from 'protractor';
import { HomeNavService } from 'src/app/shared/common-service/home-nav.service';


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
  @ViewChild('fc') fc: FullCalendar;
  // @ViewChild('fullcalendar') fullcalendar: FullCalendarComponent;
  @ViewChild('draggable_people') draggablePeopleExternalElement: ElementRef;
  empList: any;
  showDialog: boolean;
  locationId: any;
  scheduleId: any;
  scheduleType: any;
  totalHours: any;
  EmpCount: any;
  constructor(private empService: EmployeeService, private locationService: LocationService,
    private messageService: MessageServiceToastr, private scheduleService: ScheduleService, private employeeService: EmployeeService
    , private homeNavigation: HomeNavService) { }
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
    // this.getEmployeeList();
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
        if (!event.event.id.startsWith('click')) {
          this.getScheduleById(+event.event.id);
        } else {
          this.splitEmpName(event);
          this.bindPopUp(event);
          this.selectedList.forEach(item => {
            if (item.EmployeeId === +this.empId) {
              item.clicked = true;
            }
          });
        }
      },
      dayClick: (event) => {
        console.log(event.view.activeStart, 'dayClick');
      },
      eventResize: (event) => {
        this.empName = event.event.title;
        this.empId = event.event.extendedProps.employeeId;
        this.buttonText = 'Save';
        this.bindPopUp(event);
      },
      eventReceive: (eventReceiveEvent) => {
        this.buttonText = 'Add';
        this.events = this.events.filter(item => item.classNames[0] !== 'draggedEvent');
        const multiSelect = this.selectedList.filter(item => item.clicked === false);
        this.selectedList = this.empList.EmployeeList.filter(item => item.selected === true);
        this.splitEmpName(eventReceiveEvent);
        this.startTime = eventReceiveEvent.event.start;
        this.isLeave = false;
        this.endTime = moment(eventReceiveEvent.event.start).add(60, 'minutes').toDate();
        if (this.selectedList.length === 0) {
          this.selectedList = this.empList.EmployeeList.filter(item => item.EmployeeId === +this.empId);
        }
        if (multiSelect.length !== 0) {
          multiSelect.forEach(element => {
            this.selectedList.push(element);
          });
        }
        if (this.selectedList.length === 1 && multiSelect.length === 0) {
          $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
          $('#name').html(this.empName);
          $('#empId').html(this.empId);
          this.empLocation = undefined;
          $('.modal').find('#location').val(0);
        } else {
          this.removeDraggedEvent();
          let i = 0;
          this.selectedList.forEach(item => {
            // if(multiSelect.length !== 0){
            //   i = multiSelect.length;
            // }
            i++;
            item.id = 'clicked' + i,
              item.title = item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
              item.start = this.startTime,
              item.end = moment(eventReceiveEvent.event.start).add(60, 'minutes'),
              item.clicked = false,
              this.events = [... this.events, {
                id: 'clicked' + i,
                title: item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
                start: this.startTime,
                end: moment(eventReceiveEvent.event.start).add(60, 'minutes'),
                classNames: ['draggedEvent'],
              }];
          });
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
        this.empName = event.event.title;
        this.empId = event.event.extendedProps.employeeId;
        this.buttonText = 'Save';
        this.bindPopUp(event);
      },
    };
  }


  // Get all the Employees details
  getEmployeeList() {
    this.empService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        this.empList = JSON.parse(data.resultData);
        this.setBoolean();
      }
    });
  }

  // Set Default boolean for customization
  setBoolean() {
    this.empList.EmployeeList.forEach(item => {
      item.selected = false;
      item.clicked = false;
    });
  }
  // Get All Location
  getLocationList() {
    this.locationService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
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
    const form = {
      scheduleId: this.scheduleId ? this.scheduleId : 0,
      employeeId: +this.empId,
      locationId: +this.empLocation,
      roleId: +localStorage.getItem('roleId'),
      isAbscent: this.isLeave,
      scheduledDate: moment(this.startTime).format(),
      startTime: moment(this.startTime).format(),
      endTime: moment(this.endTime).format(),
      scheduleType: this.scheduleType ? this.scheduleType : 1,
      comments: 'test',
      isActive: true,
      isDeleted: false
    };
    schedule.push(form);
    const scheduleObj = {
      schedule
    };
    this.scheduleService.saveSchedule(scheduleObj).subscribe(data => {
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Schedule Saved Successfully!!' });
        $('#calendarModal').modal('hide');
        this.setBoolean();

        this.getSchedule();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.getSchedule();
        $('#calendarModal').modal('hide');
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
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
    this.scheduleService.getSchedule(getScheduleObj).subscribe(data => {
      if (data.status === 'Success') {
        const empSchehdule = JSON.parse(data.resultData);
        if (empSchehdule.ScheduleDetail !== null) {
          this.totalHours = empSchehdule?.ScheduleDetail?.ScheduleHoursViewModel?.Totalhours ?
            empSchehdule?.ScheduleDetail?.ScheduleHoursViewModel?.Totalhours : 0;
          this.EmpCount = empSchehdule?.ScheduleDetail?.ScheduleEmployeeViewModel?.TotalEmployees ?
            empSchehdule?.ScheduleDetail?.ScheduleEmployeeViewModel?.TotalEmployees : 0;
          if (empSchehdule?.ScheduleDetail?.ScheduleDetailViewModel !== null) {
            empSchehdule?.ScheduleDetail?.ScheduleDetailViewModel.forEach(item => {
              const emp = {
                id: +item.ScheduleId,
                start: moment(item.StartTime).format('YYYY-MM-DDTHH:mm:ss'),
                end: moment(item.EndTime).format('YYYY-MM-DDTHH:mm:ss'),
                title: item.EmployeeName + '\xa0 \xa0 ' + item.LocationName,
                textColor: 'white',
                backgroundColor: item.IsEmployeeAbscent === true ? '#A9A9A9' : item.ColorCode,
                classNames: ['event'],
                extendedProps: {
                  employeeId: +item.EmployeeId,
                  roleId: +item.RoleId,
                  scheduleType: +item.ScheduleType,
                  locationId: +item.LocationId,
                  scheduleId: +item.ScheduleId
                }
              };
              this.events = [... this.events, emp];
            });
          }
        }
        this.removeDraggedEvent();
        this.retainUnclickedEvent();
      }
    });
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
    });
  }
  getLocationId(event) {
    // const loc = this.location.filter(item => item.LocationName === event.target.textContent);
    this.locationId = event.LocationId;
    this.getSchedule();
  }
  // Get the schedule by Id
  getScheduleById(id) {
    this.scheduleService.getScheduleById(+id).subscribe(data => {
      if (data.status === 'Success') {
        const selectedScheduledData = JSON.parse(data.resultData);
        if (selectedScheduledData.Status.length !== 0) {
          $('#name').html(this.empName);
          $('#empId').html(this.empId);
          this.empName = selectedScheduledData.Status[0].EmployeeName;
          this.empId = selectedScheduledData.Status[0].EmployeeId;
          this.startTime = selectedScheduledData.Status[0].StartTime;
          this.endTime = selectedScheduledData.Status[0].EndTime;
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
    this.employeeService.searchEmployee(this.search).subscribe(res => {
      if (res.status === 'Success') {
        this.empList = JSON.parse(res.resultData);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  getAll() {
    this.locationId = 0;
    this.getSchedule();
  }
  isAbsentChange(event) {
  }
  searchFocus() {
    this.search = this.search.trim();
  }

  loadLandingPage() {
    this.homeNavigation.loadLandingPage();
  }
}
