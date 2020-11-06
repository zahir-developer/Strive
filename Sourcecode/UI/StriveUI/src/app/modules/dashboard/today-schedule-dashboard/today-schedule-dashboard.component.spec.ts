import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TodayScheduleDashboardComponent } from './today-schedule-dashboard.component';

describe('TodayScheduleDashboardComponent', () => {
  let component: TodayScheduleDashboardComponent;
  let fixture: ComponentFixture<TodayScheduleDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TodayScheduleDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TodayScheduleDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
