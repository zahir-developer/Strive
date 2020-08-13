import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation, AfterViewInit } from '@angular/core';
// import { FullCalendarComponent } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin, { Draggable } from '@fullcalendar/interaction';
import timelinePlugin from '@fullcalendar/timeline';
import * as moment from 'moment';
// import { FullCalendarComponent } from '@fullcalendar/angular';
import { FullCalendar } from 'primeng';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

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
  today = moment(new Date());
  clickCnt = 0;
  events = [];
  options: any;
  searchEmp = '';
  headerData = '';
  mytime: any;
  selectedEvent = [];
  // @ViewChild('fullcalendar') fullcalendar: FullCalendarComponent;
  @ViewChild('fc') fc: FullCalendar;
  
  @ViewChild('draggable_people') draggablePeopleExternalElement: ElementRef;
  empList: any;
  showDialog: boolean;
  constructor(private empService: EmployeeService) { }
  ngAfterViewInit() {
    console.log("PEOPLE LIST ngAfterViewInit() START !!!")
    const self = this;

    new Draggable(this.draggablePeopleExternalElement?.nativeElement, {
      itemSelector: '.fc-event',
      eventData: function (eventEl) {
        console.log("DRAG !!!");
        // console.log("SELECTED SHIFT: " + self.selectedShift.value);
        console.log('DRAGGABLE OBJECT: ' + self.draggablePeopleExternalElement.nativeElement);

        // let returnedEvent = self.createEventObject(self.selectedShift.value, eventEl.innerText);

        // return returnedEvent;
      }
    });

  }
  ngOnInit(): void {
    this.getEmployeeList();
    console.log(this.today);
    this.empList = [{ id: 1, name: 'employee1' },
    { id: 2, name: 'employee2' },
    { id: 3, name: 'employee3' }];
    this.events = [
      {
        id: 2,
        title: 'WilFord 21412779 Main Street 2-5',
        start: '2020-06-28T14:00:00',
        end: '2020-06-28T17:00:00',
        color: '#ffcccb',
        borderColor: '#D3D3D3'
      },
      {
        id: 3,
        title: 'oxford 21412779 Old Street 3-5',
        start: '2020-06-28T11:00:00',
        end: '2020-06-28T16:30:00',
        color: '#ffcccb',
        borderColor: '#D3D3D3'
      },
      {
        id: 3,
        title: 'Repeating Event3',
        start: '2020-06-28T11:00:00',
        end: '2020-06-28T16:30:00',
        color: '#ffcccb',
        borderColor: '#D3D3D3'
      }];
    const imgUrl = 'assets/images/orange.png';
    this.options = {
      plugins: [dayGridPlugin, timelinePlugin, timeGridPlugin, interactionPlugin],

      header: {
        left: '',
        center: 'prev, title, next',
        right: 'timeGridDay,timeGridWeek'
      },
      allDaySlot: false,
      editable: true,
      droppable: true,
      slotDuration: '00:30:00',
      minTime: '09:00:00',
      maxTime: '18:00:00',
      // slotLabelInterval: '00:30:00',
      customButtons: {
        custom1: {
          text: 'Today event',
          click() {
            alert('clicked custom button 1!');
          },
        }, custom2: {
          text: 'Schedule',
          color: 'red',
          click() {
            alert('Schedule Clicked');
          },
        },
      },
      defaultView: 'timeGridWeek',
      defaultDate: new Date(),

      slotEventOverlap: false,
      // height: 400,
      // contentHeight: 400,
      // displayEventTime: true,
      eventRender(element) {
        const html = `<span class="float-right">`
          + `<img src="` + imgUrl + `" (onClick)="test()"/></a></span>`;
        element.el.innerHTML = `<div class="fc-content"><div class="fc-title" title="` + element.event.title + `">` +
          element.event.title + html + `<br>` + `</div></div>`;
        element.el.addEventListener('dblclick', () => {
          console.log(element.event.start + ' ' + element.event.end + 'double click');
        });
      },
      eventClick(event) {
        if (event.jsEvent.target.tagName === 'IMG') {
          console.log(event, 'image clicked');
        }
      },
      eventResize(event) {
        console.log(event, 'event resize');
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
  test() {
    console.log('event Clicked');
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
  tester() {
    console.log('image clicked');
  }
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
}
