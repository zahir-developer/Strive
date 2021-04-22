import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantSetupComponent } from './tenant-setup.component';

describe('TenantSetupComponent', () => {
  let component: TenantSetupComponent;
  let fixture: ComponentFixture<TenantSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenantSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenantSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
