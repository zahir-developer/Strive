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
  search = '';
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
  constructor(private empService: EmployeeService, private locationService: LocationService,
    private messageService: MessageServiceToastr, private scheduleService: ScheduleService, private employeeService: EmployeeService) { }
  ngAfterViewInit() {
    this.calendar = this.fc.getCalendar();
    console.log(this.fc.getCalendar(), 'current Date');

    this.fromDate = moment(this.fc.getCalendar().state.dateProfile.activeRange.start).format('YYYY-MM-DDTHH:mm');
    this.endDate = moment(this.fc.getCalendar().state.dateProfile.activeRange.end).format('YYYY-MM-DDTHH:mm');
    this.getSchedule();

    // tslint:disable-next-line:no-unused-expression
    new Draggable(this.draggablePeopleExternalElement?.nativeElement, {
      itemSelector: '.fc-event',
      eventData: function (eventEl) {
        return {
          id: 'dragged1',
          title: eventEl.innerText,
          backgroundColor: '#696969',
          textColor: '#FFFFFF',
          classNames: ['draggedEvent'],
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

        // const html = `<span class="float-right">`
        //   + `<img src="` + imgUrl + `" (onClick)="test()"/></a></span>`;
        // element.el.innerHTML = `<div class="fc-content"><div class="fc-title" title="` + element.event.title + `">` +
        //   element.event.title + html + `<br>` + `</div></div>`;
        // element.el.addEventListener('dblclick', () => {
        //   console.log(element.event.start + ' ' + element.event.end + 'double click');
        // });
      },
      eventClick: (event) => {
        if (!event.event.id.startsWith('click')) {
          this.empName = event.event.title;
          this.empId = event.event.extendedProps.employeeId;
          this.getScheduleById(+event.event.id);
        } else {
          this.splitEmpName(event);
          this.bindPopUp(event);
          this.selectedList.forEach(item => {
            if (item.EmployeeId === +this.empId) {
              item.clicked = true;
            }
          });
          console.log(this.selectedList);
        }
      },
      eventResize: (event) => {
        this.empName = event.event.title;
        this.empId = event.event.extendedProps.employeeId;
        this.buttonText = 'Save';
        this.bindPopUp(event);
      },
      eventReceive: (eventReceiveEvent) => {
        this.selectedList = this.empList.EmployeeList.filter(item => item.selected === true);
        this.splitEmpName(eventReceiveEvent);
        this.startTime = eventReceiveEvent.event.start;
        this.endTime = moment(eventReceiveEvent.event.start).add(60, 'minutes').toDate();
        if (this.selectedList.length === 0 || this.selectedList.length === 1) {
          $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
          $('#name').html(this.empName);
          $('#empId').html(this.empId);
          $('.modal').find('#location').val(0);
        } else {
          // const dubEvent = selectedList.map(item => item.FirstName + ' ' + item.LastName).indexOf(this.empName);
          // selectedList.splice(dubEvent, 1);
          this.removeDraggedEvent();
          let i = 0;
          this.selectedList.forEach(item => {
            i++;
            item.id = 'clicked' + i,
              item.title = item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
              item.start = this.startTime,
              item.end = moment(eventReceiveEvent.event.start).add(60, 'minutes'),
            this.events = [... this.events, {
              id: 'clicked' + i,
              title: item.FirstName + ' ' + item.LastName + '\n' + item.EmployeeId,
              start: this.startTime,
              end: moment(eventReceiveEvent.event.start).add(60, 'minutes'),
            }];
          });
        }
      },
      eventDragStop: (event) => {
        if ((200 <= event.jsEvent.pageX) && (event.jsEvent.pageX <= 500)) {
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
    const form = {
      scheduleId: this.scheduleId ? this.scheduleId : 0,
      employeeId: +this.empId,
      locationId: +this.empLocation,
      roleId: +localStorage.getItem('roleId'),
      scheduledDate: moment(this.startTime).format(),
      startTime: moment(this.startTime).format(),
      endTime: moment(this.endTime).format(),
      scheduleType: 1,
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
  // Get All Location
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
        if (empSchehdule.ScheduleDetail.length !== 0) {
          empSchehdule.ScheduleDetail.forEach(item => {
            const emp = {
              id: +item.ScheduleId,
              start: moment(item.StartTime).format('YYYY-MM-DDTHH:mm:ss'),
              end: moment(item.EndTime).format('YYYY-MM-DDTHH:mm:ss'),
              title: 'new test',
              textColor: 'white',
              backgroundColor: '#FF7900',
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
        this.removeDraggedEvent();
        this.retainUnclickedEvent();
      }
    });
  }
  retainUnclickedEvent(){
    if (this.selectedList.length !== 0) {
this.selectedList.forEach(item => {
  if (item.clicked === false) {
  this.events = [... this.events, {
    id: item.id,
start: item.start,
end: item.end,
title: item.title
  }];
}
});
    }
  }
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
      console.log(data);
      if (data.status === 'Success') {
        this.getSchedule();
      }
    });
  }
  getLocationId(event) {
    const loc = this.location.filter(item => item.LocationName === event.target.textContent);
    this.locationId = loc[0].LocationId;
    this.getSchedule();
  }
  getScheduleById(id) {
    this.scheduleService.getScheduleById(+id).subscribe(data => {
      if (data.status === 'Success') {
        const selectedScheduledData = JSON.parse(data.resultData);
        if (selectedScheduledData.Status.length !== 0) {
          $('#name').html(this.empName);
          $('#empId').html(this.empId);
          this.startTime = selectedScheduledData.Status[0].StartTime;
          this.endTime = selectedScheduledData.Status[0].EndTime;
          this.scheduleId = selectedScheduledData.Status[0].ScheduleId;
          $('.modal').find('#location').val(selectedScheduledData.Status[0].LocationId);
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
  bindPopUp(event) {
    this.startTime = event.event.start;
    this.endTime = event.event.end === null ? moment(event.event.start).add(30, 'minutes').toDate() :
      event.event.end;
    this.scheduleId = event?.event?.extendedProps?.scheduleId;
    $('#calendarModal').modal({ backdrop: 'static', keyboard: false });
    $('#name').html(this.empName);
    $('#empId').html(this.empId);
    if (event.event.extendedProps.locationId) {
      $('.modal').find('#location').val(event.event.extendedProps.locationId);
      this.empLocation = event.event.extendedProps.locationId;
    } else {
      $('.modal').find('#location').val(0);
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
  }
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
}
