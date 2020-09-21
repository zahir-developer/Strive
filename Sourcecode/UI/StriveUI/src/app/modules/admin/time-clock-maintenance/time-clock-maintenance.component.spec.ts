import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeClockMaintenanceComponent } from './time-clock-maintenance.component';

describe('TimeClockMaintenanceComponent', () => {
  let component: TimeClockMaintenanceComponent;
  let fixture: ComponentFixture<TimeClockMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeClockMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeClockMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
