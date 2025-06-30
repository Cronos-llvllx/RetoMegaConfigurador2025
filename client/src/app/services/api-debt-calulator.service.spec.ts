import { TestBed } from '@angular/core/testing';

import { APIDebtCalulatorService } from './api-debt-calulator.service';

describe('APIDebtCalulatorService', () => {
  let service: APIDebtCalulatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APIDebtCalulatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
