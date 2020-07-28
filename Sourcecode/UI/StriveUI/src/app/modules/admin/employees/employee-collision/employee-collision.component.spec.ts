import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeCollisionComponent } from './employee-collision.component';

describe('EmployeeCollisionComponent', () => {
  let component: EmployeeCollisionComponent;
  let fixture: ComponentFixture<EmployeeCollisionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeCollisionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeCollisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
