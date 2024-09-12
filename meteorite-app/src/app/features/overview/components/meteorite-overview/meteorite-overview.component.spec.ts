import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeteoriteOverviewComponent } from './meteorite-overview.component';

describe('MeteoriteOverviewComponent', () => {
  let component: MeteoriteOverviewComponent;
  let fixture: ComponentFixture<MeteoriteOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeteoriteOverviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MeteoriteOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
