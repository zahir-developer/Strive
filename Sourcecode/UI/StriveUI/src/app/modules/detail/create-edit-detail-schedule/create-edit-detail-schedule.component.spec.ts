import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditDetailScheduleComponent } from './create-edit-detail-schedule.component';

describe('CreateEditDetailScheduleComponent', () => {
  let component: CreateEditDetailScheduleComponent;
  let fixture: ComponentFixture<CreateEditDetailScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditDetailScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditDetailScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
