import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportFiletypeComponent } from './export-filetype.component';

describe('ExportFiletypeComponent', () => {
  let component: ExportFiletypeComponent;
  let fixture: ComponentFixture<ExportFiletypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExportFiletypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExportFiletypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
