import { TestBed } from '@angular/core/testing';

import { LoadingmaskService } from './loadingmask.service';

describe('LoadingmaskService', () => {
  let service: LoadingmaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoadingmaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
