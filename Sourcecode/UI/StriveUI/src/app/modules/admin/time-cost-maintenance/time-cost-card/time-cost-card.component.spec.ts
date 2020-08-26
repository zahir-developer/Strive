import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeCostCardComponent } from './time-cost-card.component';

describe('TimeCostCardComponent', () => {
  let component: TimeCostCardComponent;
  let fixture: ComponentFixture<TimeCostCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeCostCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeCostCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
