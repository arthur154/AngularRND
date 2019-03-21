import { Component, OnInit } from '@angular/core';
import { MemberService } from '../member-service/member.service';
import { throwError } from 'rxjs';

import { UserMessageService } from '../user-message-service/user-message.service';
import { IAlertType } from '../user-messages/user-messages.component';

class InsertStatus {
  correlationid: string;
}

@Component({
  selector: 'app-polling-example',
  templateUrl: './polling-example.component.html',
  styleUrls: ['./polling-example.component.scss']
})
export class PollingExampleComponent implements OnInit {
  status: string;
  memberJson: object;
  memberJsonString: string;
  insertStatus: InsertStatus;

  constructor(private _memberService: MemberService, private _userMessageService: UserMessageService) { }

  ngOnInit() {
    this.status = "(empty)";
    this.memberJson = {};
    this.memberJsonString = "{}";
    this.insertStatus = new InsertStatus();
    this.insertStatus.correlationid = "";
  }

  GetMemeber() {
    this.memberJsonString = "Loading...";

    this._memberService.getSingleMemeber().subscribe(
      data => {
        this.memberJson = data;
        this.memberJsonString = JSON.stringify(data);
        return true;
      },
      error => {
        this._userMessageService.addUserMessage("There was an error retrieving the Memeber.", IAlertType.danger);
        this.memberJsonString = "{}";
        return throwError(error);
      }
    );
  }

  SaveMemeber() {
    this.insertStatus.correlationid = "Loading...";

    this._memberService.insertMember(this.memberJson).subscribe(
      data => {
        this.insertStatus = JSON.parse(data);
        this.RefreshStatus();
        return true;
      },
      error => {
        this._userMessageService.addUserMessage("There was an error saving the Memeber.", IAlertType.danger);
        this.insertStatus.correlationid = "";
        return throwError(error);
      }
    );
  }

  RefreshStatus() {
    console.log("this._memberService.getInsertStatus(" + this.insertStatus.correlationid + ")");

    this.status = "Loading...";

    this._memberService.getInsertStatus(this.insertStatus.correlationid).subscribe(
      data => {
        this.status = JSON.stringify(data);
        return true;
      },
      error => {
        this._userMessageService.addUserMessage("There was an error retrieving the record status.", IAlertType.danger);
        this.status = "(empty)";
        return throwError(error);
      }
    );
  }
}
