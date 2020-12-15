import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectServicesComponent } from './select-services.component';

describe('SelectServicesComponent', () => {
  let component: SelectServicesComponent;
  let fixture: ComponentFixture<SelectServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
