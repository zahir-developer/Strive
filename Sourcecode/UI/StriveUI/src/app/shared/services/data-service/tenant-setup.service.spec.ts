import { TestBed } from '@angular/core/testing';

import { TenantSetupService } from './tenant-setup.service';

describe('TenantSetupService', () => {
  let service: TenantSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenantSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
