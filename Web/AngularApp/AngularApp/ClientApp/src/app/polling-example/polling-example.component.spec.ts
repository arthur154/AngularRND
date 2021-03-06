import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PollingExampleComponent } from './polling-example.component';

describe('PollingExampleComponent', () => {
  let component: PollingExampleComponent;
  let fixture: ComponentFixture<PollingExampleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PollingExampleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PollingExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
