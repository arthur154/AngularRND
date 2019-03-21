import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';

import { MemberService } from './member-service/member.service';

import { DataTableExampleComponent } from './data-table-example/data-table-example.component';
import { PollingExampleComponent } from './polling-example/polling-example.component';
import { BsAlertExampleComponent } from './bs-alert-example/bs-alert-example.component';
import { MemberComponent } from './member/member.component';
import { UserMessagesComponent } from './user-messages/user-messages.component';
import { UserMessageService } from './user-message-service/user-message.service';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        DataTableExampleComponent,
        PollingExampleComponent,
        BsAlertExampleComponent,
        MemberComponent,
        UserMessagesComponent
      ],
      imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        NgbModule,
        DataTablesModule
      ],
      providers: [
        MemberService,
        UserMessageService
      ],
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
  it(`should have as title 'ClientApp'`, async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('ClientApp');
  }));
});
