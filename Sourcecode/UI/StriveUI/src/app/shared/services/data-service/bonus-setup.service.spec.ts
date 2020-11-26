import { TestBed } from '@angular/core/testing';

import { BonusSetupService } from './bonus-setup.service';

describe('BonusSetupService', () => {
  let service: BonusSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BonusSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
