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
  empName: any;
  empId: any;
  fromDate: any;
  endDate: any;
  today = moment(new Date());
  clickCnt = 0;
  events = [];
  options: any;
  searchEmp = '';
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
  constructor(private empService: EmployeeService, private locationService: LocationService,
    private messageService: MessageServiceToastr, private scheduleService: ScheduleService) { }
  ngAfterViewInit() {
    this.calendar = this.fc.getCalendar();
    console.log(this.fc.getCalendar(), 'current Date');

    this.fromDate = moment(this.fc.getCalendar().state.dateProfile.activeRange.start + 1).format('YYYY-MM-DDTHH:mm');
    this.endDate = moment(this.fc.getCalendar().state.dateProfile.activeRange.end).format('YYYY-MM-DDTHH:mm');
    this.getSchedule();

    // tslint:disable-next-line:no-unused-expression
    new Draggable(this.draggablePeopleExternalElement?.nativeElement, {
      itemSelector: '.fc-event',
      eventData: function (eventEl) {
        return {
          title: eventEl.innerText,
          backgroundColor: '#0000'
        };
      }
    });

  }
  ngOnInit(): void {
    this.getEmployeeList();

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
      minTime: '09:00:00',
      maxTime: '18:00:00',
      defaultView: 'timeGridWeek',
      slotEventOverlap: false,
      height: 'auto',
      contentHeight: 'auto',
      // displayEventTime: true,
      eventRender(element) {
        console.log(element,   'event Render');
        // const html = `<span class="float-right">`
        //   + `<img src="` + imgUrl + `" (onClick)="test()"/></a></span>`;
        // element.el.innerHTML = `<div class="fc-content"><div class="fc-title" title="` + element.event.title + `">` +
        //   element.event.title + html + `<br>` + `</div></div>`;
        // element.el.addEventListener('dblclick', () => {
        //   console.log(element.event.start + ' ' + element.event.end + 'double click');
        // });
      },
      eventClick: (event) => {
        this.splitEmpName(event);
        this.startTime = event.event.start;
        this.endTime = event.event.end === null ? moment(event.event.start).add(30, 'minutes').toDate() :
          event.event.end;
        $('#calendarModal').modal('show');
        $('#name').html(this.empName);
        $('#empId').html(this.empId);
        // $('.modal').find('#startTime').val(this.startTime);
        // $('.modal').find('#endTime').val(this.endTime);
      },
      eventResize: (event) => {
        this.splitEmpName(event);
        this.startTime = event.event.start;
        this.endTime = event.event.end;
        $('#calendarModal').modal('show');
        $('#name').html(this.empName);
        $('#empId').html(this.empId);
        // $('.modal').find('#startTime').val(this.startTime);
        // $('.modal').find('#endTime').val(this.endTime);
        $('.modal').find('#loation').val(1);
      },
      eventReceive: (eventReceiveEvent) => {
        const selectedList = this.empList.EmployeeList.filter(item => item.selected === true);
        this.splitEmpName(eventReceiveEvent);
        this.startTime = eventReceiveEvent.event.start;
        this.endTime = moment(eventReceiveEvent.event.start).add(30, 'minutes').toDate();
        if (selectedList.length === 0 || selectedList.length === 1) {
          $('#calendarModal').modal();
          $('#name').html(this.empName);
          $('#empId').html(this.empId);
          // $('#startTime').html(this.startTime);
          // $('#endTime').html(this.endTime);
        } else {
          const dubEvent = selectedList.map(item => item.FirstName + ' ' + item.LastName).indexOf(this.empName);
          selectedList.splice(dubEvent, 1);
          selectedList.forEach(item => {
            this.events = [... this.events, {
              id: this.guid(),
              title: item.FirstName + ' ' + item.LastName + '-' + item.EmployeeId,
              start: this.startTime,
              end: moment(eventReceiveEvent.event.start).add(30, 'minutes'),
            }];
          });
        }
      },
      eventDragStop: (event) => {
        // alert('Coordinates: ' + event.jsEvent.pageX + ',' + event.jsEvent.pageY);
        // && (130 <= event.jsEvent.pageY) && (event.jsEvent.pageY <= 170)
        if ((200 <= event.jsEvent.pageX) && (event.jsEvent.pageX <= 500)) {
          alert('delete: ' + event.event.id);
          this.deleteEvent(event);
        }
      },
      eventAdd(event) {
        console.log(event, 'eventAdded');
      },
      drop(event) {
        console.log(event, 'drop');
        // event.revert();
      },
      datesRender(event) {
        console.log(event, 'datesRender');
        // console.log( this.fc.getCalendar().getDate(), 'days Rendar');
      },
      eventDrop: (event) => {
        this.splitEmpName(event);
        this.startTime = event.event.start;
        this.endTime = event.event.end === null ? moment(event.event.start).add(30, 'minutes').toDate() :
          event.event.end;
        $('#calendarModal').modal('show');
        $('#name').html(this.empName);
        $('#empId').html(this.empId);
      },
      dblclick(event) {
        console.log(event, 'double Click');
      }
    };
  }

  submit() {
    console.log(this.events, 'allEvents');
    console.log(this.selectedEvent, 'selectedEvents');
  }

  // Get all the Employees details
  getEmployeeList() {
    this.empService.getEmployees().subscribe(data => {
      if (data.status === 'Success') {
        this.empList = JSON.parse(data.resultData);
        this.empList.EmployeeList.forEach(item => {
          item.selected = false;
        });
      }
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
    const form = {
      scheduleId: null,
      employeeId: +this.empId,
      locationId: +this.empLocation,
      roleId: localStorage.getItem('roleId'),
      scheduledDate: moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      startTime: moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      endTime: moment(this.endTime).format('YYYY-MM-DDTHH:mm:ss'),
      scheduleType: 1,
      comments: null,
      isActive: true
    };
    this.scheduleService.saveSchedule(form).subscribe(data => {
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Schedule Saved Successfully!!' });
        $('#calendarModal').modal('hide');
        this.getSchedule();
      }
    });
  }
  // Get All Location
  getLocation(event) {
    this.empLocation = event.target.value;
  }
  // Get all schedule based on date
  getSchedule() {
    this.events = [];
    // this.fc.getCalendar().destroy();
    this.scheduleService.getSchedule(this.fromDate, this.endDate).subscribe(data => {
      if (data.status === 'Success') {
        const empSchehdule = JSON.parse(data.resultData);
        if (empSchehdule.Status.length !== 0) {
          empSchehdule.Status.forEach(item => {
            const emp = {
              id: item.ScheduleId,
              start: moment(item.StartTime).format('YYYY-MM-DDTHH:mm:ss'),
              end: moment(item.StartTime).add(30, 'minutes').format('YYYY-MM-DDTHH:mm:ss'),
              title: 'new test',
              textColor: 'white',
              backgroundColor: '#FF7900',
              extendedProps: {
                employeeId: item.EmployeeId,
                roleId: item.RoleId,
                scheduleType: item.ScheduleType,
                locationId: item.LocationId
              }
            };
            this.events = [... this.events, emp];
          });
        }
        
        // const eventSources = this.fc.getCalendar().getEventSources();
        // console.log(eventSources, 'caleder Event');
        // const len = eventSources.length;
        // for (let i = 0; i < len; i++) {
        //   eventSources[i].remove();
        // }
        // this.fc.getCalendar().clientEvents((i, item) => {console.log(item, 'clientEvent'); });
        // this.fc.getCalendar().addEventSource(this.events);
        // this.fc.getCalendar().refetchEvents();
        this.fc.getCalendar().render();
      }
    });
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
    if (event.event.id !== '') {
      // this.scheduleService.deleteSchedule(event.event.id).subscribe(data => {
      //   console.log(data);
      //   if (data.Status === ' Success') {

      //   }
      // });
    }
  }
  getLocationId(event) {
    const loc = this.location.filter(item => item.LocationName === event.target.textContent);
    const locationId = loc[0].LocationId;
    this.getSchedule();
  }
  getScheduleById(id) {
    this.scheduleService.getScheduleById(id).subscribe(data => {

    });
  }
  splitEmpName(event) {
    const str = event.event.title.split('\n');
    this.empName = str[0];
    this.empId = str[1];
  }
}
