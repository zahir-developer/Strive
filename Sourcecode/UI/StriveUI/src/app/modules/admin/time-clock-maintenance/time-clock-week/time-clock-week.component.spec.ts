import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeClockWeekComponent } from './time-clock-week.component';

describe('TimeClockWeekComponent', () => {
  let component: TimeClockWeekComponent;
  let fixture: ComponentFixture<TimeClockWeekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeClockWeekComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeClockWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
