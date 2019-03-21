import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DataTablesModule } from 'angular-datatables';
import { DataTableExampleComponent } from './data-table-example.component';

describe('DataTableExampleComponent', () => {
  let component: DataTableExampleComponent;
  let fixture: ComponentFixture<DataTableExampleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DataTableExampleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DataTableExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
