import { TestBed } from '@angular/core/testing';

import { WhiteLabelService } from './white-label.service';

describe('WhiteLabelService', () => {
  let service: WhiteLabelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WhiteLabelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
