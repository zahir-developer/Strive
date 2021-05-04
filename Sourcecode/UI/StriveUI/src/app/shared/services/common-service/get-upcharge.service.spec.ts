import { TestBed } from '@angular/core/testing';

import { GetUpchargeService } from './get-upcharge.service';

describe('GetUpchargeService', () => {
  let service: GetUpchargeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetUpchargeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
