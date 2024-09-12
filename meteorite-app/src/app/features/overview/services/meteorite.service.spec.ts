import { TestBed } from '@angular/core/testing';

import { MeteoriteService } from './meteorite.service';

describe('MeteoriteServiceService', () => {
  let service: MeteoriteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MeteoriteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
