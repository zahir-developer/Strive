import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeCostMaintenanceComponent } from './time-cost-maintenance.component';

describe('TimeCostMaintenanceComponent', () => {
  let component: TimeCostMaintenanceComponent;
  let fixture: ComponentFixture<TimeCostMaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeCostMaintenanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeCostMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
