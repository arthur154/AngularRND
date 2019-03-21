import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BsAlertExampleComponent } from './bs-alert-example.component';

describe('BsAlertExampleComponent', () => {
  let component: BsAlertExampleComponent;
  let fixture: ComponentFixture<BsAlertExampleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BsAlertExampleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BsAlertExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
