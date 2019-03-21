import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';

import { MemberService } from './member-service/member.service';

import { AppComponent } from './app.component';
import { DataTableExampleComponent } from './data-table-example/data-table-example.component';
import { PollingExampleComponent } from './polling-example/polling-example.component';
import { BsAlertExampleComponent } from './bs-alert-example/bs-alert-example.component';
import { MemberComponent } from './member/member.component';
import { UserMessagesComponent } from './user-messages/user-messages.component';
import { UserMessageService } from './user-message-service/user-message.service';

@NgModule({
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
    NgbModule.forRoot(),
    DataTablesModule
  ],
  providers: [
    MemberService,
    UserMessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
