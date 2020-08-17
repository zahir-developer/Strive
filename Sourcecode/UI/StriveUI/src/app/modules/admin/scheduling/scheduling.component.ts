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
  empId: any
  today = moment(new Date());
  clickCnt = 0;
  events = [];
  options: any;
  searchEmp = '';
  headerData = '';
  mytime: any;
  location = [];
  empLocation = '';
  selectedEvent = [];
  startTime: Date;
  endTime: Date;
  @ViewChild('fc') fc: FullCalendar;
  @ViewChild('draggable_people') draggablePeopleExternalElement: ElementRef;
  empList: any;
  showDialog: boolean;
  constructor(private empService: EmployeeService, private locationService: LocationService,
    private messageService: MessageServiceToastr, private scheduleService: ScheduleService) { }
  ngAfterViewInit() {
    this.calendar = this.fc.getCalendar();
    const self = this;

    // tslint:disable-next-line:no-unused-expression
    new Draggable(this.draggablePeopleExternalElement?.nativeElement, {
      itemSelector: '.fc-event',
      eventData: function (eventEl) {
        console.log('DRAG !!!');
        return {
          title: eventEl.innerText,
          backgroundColor: '#ddddd'
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
      // height: 400,
      // contentHeight: 400,
      // displayEventTime: true,
      // eventRender(element) {
      //   const html = `<span class="float-right">`
      //     + `<img src="` + imgUrl + `" (onClick)="test()"/></a></span>`;
      //   element.el.innerHTML = `<div class="fc-content"><div class="fc-title" title="` + element.event.title + `">` +
      //     element.event.title + html + `<br>` + `</div></div>`;
      //   element.el.addEventListener('dblclick', () => {
      //     console.log(element.event.start + ' ' + element.event.end + 'double click');
      //   });
      // },
      eventClick(event) {
        const str = event.event.title.split('\n');
        this.empName = str[0];
        this.empId = str[1];
        this.startTime = moment(new Date(event.event.start)).format('HH:mm A');
        this.endTime = moment(event.event.start).add(30, 'minutes').format('HH:mm A');
        const startTime = new Date(event.event.start);
        const endTime = moment(new Date(event.event.start)).add(30, 'minutes').toDate();
        $('#name').html(this.empName);
        $('#empId').html(this.empId);
        $('#calendarModal').modal('show');
        $('.modal').find('#startTime').val(this.startTime);
        $('.modal').find('#endTime').val(this.endTime);
        // $('#timeStart').html(startTime);
        $('#timeStart').timepicker('setTime', startTime);
        // $('#timeEnd').html(this.endTime);

      },
      eventResize(event) {
        console.log(event, 'event resize');
      },
      eventReceive: (eventReceiveEvent) => {
        console.log(eventReceiveEvent);
        const str = eventReceiveEvent.event.title.split('\n');
        this.empName = str[0];
        this.empId = str[1];
        this.startTime = eventReceiveEvent.event.start;
        this.endTime = moment(eventReceiveEvent.event.start).add(30, 'minutes').toDate();
        $('#name').html(this.empName);
        $('#empId').html(this.empId);
        $('#startTime').html(this.startTime);
        $('#endTime').html(this.endTime);
        $('#calendarModal').modal();
      },

      datesRender(event) {
        console.log(event, 'datesRender');
        // console.log( this.fc.getCalendar().getDate(), 'days Rendar');
      },
      eventDrop(event) {
        console.log(event, 'eventDrop');
      },
      dblclick(event) {
        console.log(event, 'double Click');
      }
    };
  }

  DragStart(event) {
    // this.showDialog = true;
    this.selectedEvent.push({
      id: 23,
      title: 'my Event1',
      start: '2020-06-28T16:00:00',
      end: '2020-06-28T16:30:00',
    });
    this.events = [... this.events, {
      id: 23,
      title: 'my Event1',
      start: '2020-06-28T09:00:00',
      end: '2020-06-28T09:30:00',
    }];
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
        // this.empList = _.uniq(this.empList.EmployeeList);
        // this.isTableEmpty = false;
        // if (this.empList.EmployeeList.length > 0) {
        //   const employeeDetail = employees.EmployeeList; }
        console.log(this.empList.EmployeeList, 'employeeList');
      }
    });
  }
  getLocationList() {
    this.locationService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
        console.log(this.location, 'location');
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }
  addSchedule() {
    const form = {
      scheduleId: null,
      employeeId: +this.empId,
      locationId: +this.empLocation,
      roleId: localStorage.getItem('roleId'),
      scheduledDate: moment(this.startTime).format('MM-DD-YYYY'),
      startTime: moment(this.startTime).format('YYYY-MM-DDTHH:mm:ss'),
      endTime: moment(this.endTime).format('YYYY-MM-DDTHH:mm:ss'),
      scheduleType: null,
      comments: null,
      isActive: true
    };
    this.scheduleService.saveSchedule(form).subscribe(data => {
      console.log(data, 'saveschedule');
    })
    console.log(form);
  }
  getLocation(event) {
    this.empLocation = event.target.value;
    console.log(event);
  }
}
