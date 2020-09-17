import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintWashComponent } from './print-wash.component';

describe('PrintWashComponent', () => {
  let component: PrintWashComponent;
  let fixture: ComponentFixture<PrintWashComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrintWashComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintWashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
