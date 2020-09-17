import { TestBed } from '@angular/core/testing';

import { TimeClockMaintenanceService } from './time-clock-maintenance.service';

describe('TimeClockMaintenanceService', () => {
  let service: TimeClockMaintenanceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimeClockMaintenanceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
