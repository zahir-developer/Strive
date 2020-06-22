import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
// import { FullCalendarComponent } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import { FullCalendar } from 'primeng/fullcalendar/fullcalendar';

@Component({
  selector: 'app-scheduling',
  templateUrl: './scheduling.component.html',
  styleUrls: ['./scheduling.component.css']
})
export class SchedulingComponent implements OnInit {
  events = [];
  options: any;
  searchEmp = '';
  headerData = '';
  mytime: any;
  selectedEvent = [];
  // @ViewChild('fullcalendar') fullcalendar: FullCalendarComponent;
  @ViewChild('daySchedule') fc: FullCalendar;
  @ViewChild('external') external: ElementRef;
  empList: { id: number; name: string; }[];
  showDialog: boolean;
  constructor() { }

  ngOnInit(): void {
    this.empList = [{ id: 1, name: 'employee1' },
    { id: 2, name: 'employee2' },
    { id: 3, name: 'employee3' }];
    this.events = [
      {
        id: 2,
        title: 'Long Event',
        start: '2020-06-19T16:30:00',
        end: '2020-06-19T17:30:00',
        extendedProps: {
          imgId: 1
        }
      },
      {
        id: 3,
        title: 'Repeating Event',
        start: '2020-06-19T16:00:00',
        end: '2020-06-19T16:30:00',
        extendedProps: {
          imgId: 2
        }
      }];
    const imgUrl = 'assets/images/orange.png';
    this.options = {
      plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
      defaultDate: new Date(),
      header: {
        left: 'prev,next',
        center: 'title',
        // right: 'timeGridWeek,timeGridDay'
        right: 'timeGridWeek,timeGridDay'
      },
      defaultView: 'timeGridDay',
      allDaySlot: false,
      minTime: '09:00:00',
      maxTime: '17:00:00',
      editable: true,
      droppable: true,
      themeSystem: 'standard',
      slotEventOverlap: false,
      slotDuration: '00:15:00',
      slotLabelInterval: '01:00:00',
      height: 'auto',
      contentHeight: 'auto',
      displayEventTime: false,
      // slotLabelInterval: '00:15:00',
      eventRender(element) {
        const html = `<span class="float-right">`
          + `<img src="` + imgUrl + `" (click)="test()"/></a></span>`;
        element.el.innerHTML = `<div class="fc-content"><div class="fc-title" title="` + element.event.title + `">` +
          element.event.title + html + `</div></div>`;
        console.log(element);
      },
      eventClick(event) {
        if (event.jsEvent.target.tagName === 'IMG') {
          console.log('image clicked');
        }
      },
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
      start: '2020-06-20T16:00:00',
      end: '2020-06-20T16:30:00',
    });
    this.events = [... this.events, {
      id: 23,
      title: 'my Event1',
      start: '2020-06-22T09:00:00',
      end: '2020-06-22T09:30:00',
    }];
  }
  submit() {
    console.log(this.events, 'allEvents');
    console.log(this.selectedEvent, 'selectedEvents');
  }
  tester() {
    console.log('image clicked');
  }

}
