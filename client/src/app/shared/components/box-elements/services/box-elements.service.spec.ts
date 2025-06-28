import { TestBed } from '@angular/core/testing';

import { BoxElementsService } from './box-elements.service';

describe('BoxElementsService', () => {
  let service: BoxElementsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BoxElementsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
