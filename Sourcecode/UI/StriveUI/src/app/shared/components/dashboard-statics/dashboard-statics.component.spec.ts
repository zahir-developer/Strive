import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardStaticsComponent } from './dashboard-statics.component';

describe('DashboardStaticsComponent', () => {
  let component: DashboardStaticsComponent;
  let fixture: ComponentFixture<DashboardStaticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardStaticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardStaticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
