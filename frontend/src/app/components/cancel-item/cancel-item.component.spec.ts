import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelItemComponent } from './cancel-item.component';

describe('CancelItemComponent', () => {
  let component: CancelItemComponent;
  let fixture: ComponentFixture<CancelItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CancelItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CancelItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
