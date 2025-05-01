import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleCancelComponent } from './sale-cancel.component';

describe('SaleCancelComponent', () => {
  let component: SaleCancelComponent;
  let fixture: ComponentFixture<SaleCancelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleCancelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleCancelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
