import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeadsmanagermentDetailComponent } from './leadsmanagerment-detail.component';

describe('LeadsmanagermentDetailComponent', () => {
  let component: LeadsmanagermentDetailComponent;
  let fixture: ComponentFixture<LeadsmanagermentDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeadsmanagermentDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeadsmanagermentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
